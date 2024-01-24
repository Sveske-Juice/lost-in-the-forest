using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject m_slotPrefab;

    void Start()
    {
        InventorySystem.instance.onInventoryChangedEvent += OnUpdateInventory;
    }

    private void OnUpdateInventory()
    {
        foreach (Transform t in transform)
        {
            // Remove click handlers
            ImageCickHandler clickHandler = t.gameObject.GetComponent<ImageCickHandler>();

            clickHandler.OnElementUp -= ItemSlotClick;

            Destroy(t.gameObject);
        }

        DrawInventory();
    }

    public void DrawInventory()
    {
        foreach (InventoryItem item in InventorySystem.instance.inventory)
        {
            AddInventorySlot(item);
        }
    }

    public void AddInventorySlot(InventoryItem item)
    {
        GameObject obj = Instantiate(m_slotPrefab);
        obj.transform.SetParent(transform, false);

        StackNumber slot = obj.GetComponent<StackNumber>();
        slot.Set(item);

        // Setup click listener
        ImageCickHandler clickHandler = obj.AddComponent<ImageCickHandler>();

        clickHandler.OnElementUp += ItemSlotClick;
    }

    public void ItemSlotClick(ImageCickHandler imageElement)
    {
        StackNumber slot = imageElement.gameObject.GetComponent<StackNumber>();

        UseModifierContext modifierContext = new UseModifierContextBuilder()
            .WithInstantHealthReceiver(CombatPlayer.combatPlayer as IInstantHealthReceiver)
            .WithRegenerationHealthReceiver(CombatPlayer.combatPlayer as IRegenerationReceiver)
            .Build();

        slot.Item.data.UseAbility(modifierContext);
    }
}
