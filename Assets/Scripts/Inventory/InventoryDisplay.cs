using System;
using System.Collections.Generic;
using UnityEngine;
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

    private GameObject ghostItem = null;

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
    }

    private void Update()
    {
        UpdateGhostItem();
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

        // Setup click listener
        ImageCickHandler clickHandler = obj.AddComponent<ImageCickHandler>();

        clickHandler.OnElementDown += ItemSlotClickDown;
        clickHandler.OnElementUp += ItemSlotClickUp;
    }

    private void ItemSlotClickDown(ImageCickHandler handler)
    {
        ItemSlot slot = handler.gameObject.GetComponent<ItemSlot>();
        ghostItem = new GameObject($"Ghost Item {slot.Item.data.DisplayName}");
        ghostItem.transform.SetParent(canvas);

        var img = ghostItem.AddComponent<RawImage>();
        img.texture = slot.Item.data.Icon;
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
            Sacrificer sacrificer = hit.gameObject.GetComponent<Sacrificer>();
            if (sacrificer == null) continue;

            sacrificer.Sacrifice(slot.Item.data);
            connectedInventory.Remove(slot.Item.data);
            break;
        }

        Destroy(ghostItem);
        ghostItem = null;
    }
}
