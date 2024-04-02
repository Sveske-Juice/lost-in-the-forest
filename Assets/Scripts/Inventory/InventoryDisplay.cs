using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField]
    private Transform itemSlotsParent;
    [SerializeField]
    private Transform canvas;

    [SerializeField]
    private GameObject slotPrefab;

    [SerializeField]
    private InventorySystem connectedInventory;

    [Header("Drag")]
    [SerializeField]
    private bool dragAndDrop = true;

    [SerializeField]
    private Color dragColor = new Color(1f, 1f, 1f, 0.75f);

    private GameObject ghostItem = null;

    public bool acceptActives = true;

    [SerializeField] private bool invertElevation = false;

    private void OnEnable()
    {
        connectedInventory.onInventoryChangedEvent += UpdateUI;

        // Initial draw
        UpdateUI();
    }

    private void OnDisable()
    {
        connectedInventory.onInventoryChangedEvent -= UpdateUI;

        // Somehow menu was disabled while dragging item
        if (ghostItem != null)
            Destroy(ghostItem);

        for (int i = 0; i < itemSlotsParent.childCount; i++)
        {
            Destroy(itemSlotsParent.GetChild(i).gameObject);
        }
    }

    private void Update()
    {
        UpdateGhostItem();
    }

    public bool Add(ItemScriptableObject item)
    {
        // WARNING: god damn this code is shit
        if (item.IsActive && !acceptActives)
            return false;

        if (!item.IsActive && acceptActives)
            return false;

        return connectedInventory.Add(item);
    }

    private void UpdateGhostItem()
    {
        if (ghostItem == null) return;

        // Follow mouse
        ghostItem.transform.position = Input.mousePosition;
    }

    private void UpdateUI()
    {
        // Clean up from last UI build
        foreach (Transform itemSlot in itemSlotsParent)
        {
            ImageCickHandler clickHandler = itemSlot.gameObject.GetComponent<ImageCickHandler>();

            clickHandler.OnElementDown -= ItemSlotClickDown;
            clickHandler.OnElementUp -= ItemSlotClickUp;

            Destroy(itemSlot.gameObject);
        }

        foreach (InventoryItem item in connectedInventory.Inventory)
        {
            AddInventorySlot(item);
        }
    }

    private void AddInventorySlot(InventoryItem item)
    {
        GameObject obj = Instantiate(slotPrefab);
        obj.transform.SetParent(itemSlotsParent, false);

        ItemSlot slot = obj.GetComponent<ItemSlot>();
        slot.Set(item);
        slot.SetTooltip(connectedInventory.tooltipPrefab);

        if (invertElevation)
            slot.elevation *= -1;

        // Setup click listener
        ImageCickHandler clickHandler = obj.AddComponent<ImageCickHandler>();

        clickHandler.OnElementDown += ItemSlotClickDown;
        clickHandler.OnElementUp += ItemSlotClickUp;
    }

    private void ItemSlotClickDown(ImageCickHandler handler)
    {
        if (!dragAndDrop) return;

        ItemSlot slot = handler.gameObject.GetComponent<ItemSlot>();
        ghostItem = new GameObject($"Ghost Item {slot.Item.data.DisplayName}");
        ghostItem.transform.SetParent(canvas);

        var img = ghostItem.AddComponent<RawImage>();
        img.texture = slot.Item.data.Icon;
        img.color = dragColor;
    }

    private void ItemSlotClickUp(ImageCickHandler handler)
    {
        ItemSlot slot = handler.gameObject.GetComponent<ItemSlot>();
        GraphicRaycaster rayCaster = canvas.GetComponent<GraphicRaycaster>();
        var pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        pointerEventData.pointerId = -1;
        List<RaycastResult> raycastResults = new();

        rayCaster.ignoreReversedGraphics = false;
        rayCaster.Raycast(pointerEventData, raycastResults);

        foreach (var hit in raycastResults)
        {
            // Check for sacrification
            Sacrificer sacrificer = hit.gameObject.GetComponent<Sacrificer>();
            if (sacrificer != null && !connectedInventory.IsShop && slot.Item.data.Level < slot.Item.data.MaxLevel)
            {
                sacrificer.Sacrifice(slot.Item.data);
                connectedInventory.Remove(slot.Item.data);
                break;
            }

            // Check if item dropped in other inventory
            InventoryDisplay inventory = hit.gameObject.GetComponent<InventoryDisplay>();
            if (inventory != null)
            {
                // Buy from shop
                if (connectedInventory.IsShop)
                {
                    if (CreditManager.Instance.CanAfford(slot.Item.data.Cost))
                    {
                        Debug.Log($"Buying {slot.Item.data.DisplayName} for {slot.Item.data.Cost} to {inventory.name}");
                        bool couldFit = inventory.Add(slot.Item.data);
                        if (couldFit)
                        {
                            Assert.IsNotNull(CreditManager.Instance, "No credit manager to charge player!");
                            CreditManager.Instance.Charge(slot.Item.data.Cost);
                            connectedInventory.Remove(slot.Item.data);
                        }
                    }

                }

                // TODO: handle inventory to inventory transfer
                // NOTE: check for item type; actives and passives can't be mixed in inventory
                break;
            }
        }

        Destroy(ghostItem);
        ghostItem = null;
    }
}
