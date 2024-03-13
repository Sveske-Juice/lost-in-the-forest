using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Assertions;

public class ActiveInventoryUI : MonoBehaviour
{
    [SerializeField] InventorySystem connectedInventory;
    public GameObject m_slotPrefab;

    private bool waitingForTargetSelction = false;
    private ItemSlot targetSelectionItem = null;


    void Start()
    {
        connectedInventory.onInventoryChangedEvent += OnUpdateInventory;
    }

    private void Update()
    {
        HandleTargetSelection();
    }

    private void HandleTargetSelection()
    {
        if (!waitingForTargetSelction) return;

        if (Input.GetMouseButtonUp(0))
        {
            waitingForTargetSelction = false;
            Assert.IsTrue(targetSelectionItem.Item.data.ActivationMethod == ItemActivationMethod.TARGET_SELECTION);
            Vector2 mouseWS = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            AttackContext attackCtx = new AttackContextBuilder()
                .WithInitiator(CombatPlayer.combatPlayer)
                .WithOrigin(CombatPlayer.combatPlayer.transform)
                .WithPhysicalDamge(CombatPlayer.combatPlayer.GetPhysicalDamage())
                .WithMagicalDamage(CombatPlayer.combatPlayer.GetMagicalDamage())
                .WithAttackDir(new Vector3(mouseWS.x, mouseWS.y) - CombatPlayer.combatPlayer.transform.position)
                .WithTarget(mouseWS)
                .Build();


            UseModifierContext modCtx = connectedInventory.ModifierCtx(targetSelectionItem.Item.data)
                .WithAttackContext(attackCtx)
                .Build();

            targetSelectionItem.Item.data.UseAbility(modCtx);

            if (targetSelectionItem.Item.data.Uses <= 0)
                connectedInventory.Remove(targetSelectionItem.Item.data);

            waitingForTargetSelction = false;
            targetSelectionItem = null;
        }

        // Cancel item activiation
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            waitingForTargetSelction = false;
            targetSelectionItem = null;
        }
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

        UseModifierContext modCtx = connectedInventory.ModifierCtx(slot.Item.data).Build();

        // Activate item
        if (slot.Item.data.ActivationMethod == ItemActivationMethod.INSTANT)
        {
            slot.Item.data.UseAbility(modCtx);

            if (slot.Item.data.Uses <= 0)
                connectedInventory.Remove(slot.Item.data);
        }
        else
        {
            waitingForTargetSelction = true;
            targetSelectionItem = slot;
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
