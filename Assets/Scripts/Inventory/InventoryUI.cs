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

        clickHandler.OnElementEnter -= OnItemSlotEnter;
        clickHandler.OnElementExit -= OnItemSlotExit;

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

        clickHandler.OnElementEnter += OnItemSlotEnter;
        clickHandler.OnElementExit += OnItemSlotExit;
    }

    public void ItemSlotClick(ImageCickHandler imageElement)
    {
        StackNumber slot = imageElement.gameObject.GetComponent<StackNumber>();

        UseModifierContext modifierContext = new UseModifierContextBuilder()
            .WithInstantHealthReceiver(CombatPlayer.combatPlayer as IInstantHealthReceiver)
            .WithRegenerationHealthReceiver(CombatPlayer.combatPlayer as IRegenerationReceiver)
            .WithAttackSpeedReceiver(CombatPlayer.combatPlayer as IAttackSpeedReceiver)
            .WithDamageReceiver(CombatPlayer.combatPlayer as IDamageReceiver)
            .Build();

        slot.Item.data.UseAbility(modifierContext);
    }

    // TODO:: Markus, brug følgende to funktioner til at vise en item hover menu
    // du kan bruge enter funktion til at spawne en prefab, og exit funktionen til at
    // destroy den.

    // kaldt når mus enter over et item slot
    public void OnItemSlotEnter(ImageCickHandler imageElement)
    {
        StackNumber slot = imageElement.gameObject.GetComponent<StackNumber>();

        // TODO: spawn item hover prefab
        // kald funktion på instantied prefab til at loade det item (brug slot variabel)
    }

    // kaldt når mus bliver fjernet fra item slot
    public void OnItemSlotExit(ImageCickHandler imageElement)
    {
        StackNumber slot = imageElement.gameObject.GetComponent<StackNumber>();

        // TODO: slet item hover menu
    }
}
