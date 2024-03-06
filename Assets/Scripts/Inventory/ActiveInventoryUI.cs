using UnityEngine;

public class ActiveInventoryUI : MonoBehaviour
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
        foreach (InventoryItem item in connectedInventory.Inventory)
        {
            AddInventorySlot(item);
        }
    }

    public void AddInventorySlot(InventoryItem item)
    {
        GameObject obj = Instantiate(m_slotPrefab);
        obj.transform.SetParent(transform, false);

        ItemSlot slot = obj.GetComponent<ItemSlot>();
        slot.Set(item);
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

        UseModifierContext modCtx = connectedInventory.ModifierCtx(slot.Item.data);

        // Activate item
        if (slot.Item.data.ActivationMethod == ItemActivationMethod.INSTANT)
        {
            slot.Item.data.UseAbility(modCtx);
            connectedInventory.Remove(slot.Item.data);
        }
        else
        {
            // TODO: handle target method
        }
    }

    // TODO:: Markus, brug følgende to funktioner til at vise en item hover menu
    // du kan bruge enter funktion til at spawne en prefab, og exit funktionen til at
    // destroy den.

    // kaldt når mus enter over et item slot
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
