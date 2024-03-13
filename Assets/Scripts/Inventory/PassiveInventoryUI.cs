using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveInventoryUI : MonoBehaviour
{
    [SerializeField] InventorySystem connectedInventory;
    public GameObject m_slotPrefab;

    void Start()
    {
        connectedInventory.onInventoryChangedEvent += OnUpdateInventory;
    }

    private void OnUpdateInventory()
    {
        foreach (Transform t in transform)
        {
            // Remove click handlers
            ImageCickHandler clickHandler = t.gameObject.GetComponent<ImageCickHandler>();

            clickHandler.OnElementUp -= ItemSlotClick;

            clickHandler.OnElementEnter -= OnItemSlotEnter;
            clickHandler.OnElementExit -= OnItemSlotExit;

            Destroy(t.gameObject);
        }

        DrawInventory();
    }

    public void DrawInventory()
    {
        foreach (InventoryItem passive in connectedInventory.Inventory)
        {
            AddInventorySlot(passive);
        }
    }

    public void AddInventorySlot(InventoryItem passive)
    {
        GameObject obj = Instantiate(m_slotPrefab);
        obj.transform.SetParent(transform, false);

        ItemSlot slot = obj.GetComponent<ItemSlot>();
        slot.Set(passive);
        slot.SetTooltip(connectedInventory.tooltipPrefab);

        // Setup click listener
        ImageCickHandler clickHandler = obj.AddComponent<ImageCickHandler>();

        clickHandler.OnElementUp += ItemSlotClick;

        clickHandler.OnElementEnter += OnItemSlotEnter;
        clickHandler.OnElementExit += OnItemSlotExit;
    }

    public void ItemSlotClick(ImageCickHandler imageElement)
    {
        ItemSlot slot = imageElement.gameObject.GetComponent<ItemSlot>();

        UseModifierContext modifierContext = new UseModifierContextBuilder()
            .WithInstantHealthReceiver(CombatPlayer.combatPlayer as IInstantHealthReceiver)
            .WithRegenerationHealthReceiver(CombatPlayer.combatPlayer as IRegenerationReceiver)
            .WithAttackSpeedReceiver(CombatPlayer.combatPlayer as IAttackSpeedReceiver)
            .WithDamageReceiver(CombatPlayer.combatPlayer as IDamageReceiver)
            .Build();

        slot.Item.data.UseAbility(modifierContext);
    }

    public void OnItemSlotEnter(ImageCickHandler imageElement)
    {
        ItemSlot slot = imageElement.gameObject.GetComponent<ItemSlot>();

        // TODO: spawn item hover prefab
        // kald funktion på instantied prefab til at loade det item (brug slot variabel)
    }

    // kaldt når mus bliver fjernet fra item slot
    public void OnItemSlotExit(ImageCickHandler imageElement)
    {
        ItemSlot slot = imageElement.gameObject.GetComponent<ItemSlot>();

        // TODO: slet item hover menu
    }
}
