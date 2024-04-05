using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System;


public class InventorySystem : MonoBehaviour
{
    [SerializeField] bool isShop = false;

    public event Action onInventoryChangedEvent;

    private Dictionary<ItemScriptableObject, InventoryItem> m_itemDictionary = new();
    public ItemScriptableObject[] startingItems;
    public List<InventoryItem> Inventory = new();
    public GameObject tooltipPrefab;

    public int inventorySize = 3;
    public int currentInvenotorySize => Inventory.Count;

    public bool IsShop => isShop;

    public bool stacking = true;

    private void OnValidate()
    {
        if (startingItems != null)
            Assert.IsTrue(inventorySize >= startingItems.Length, $"Can not hold that many starting items!");
    }

    private void Awake()
    {
        foreach (ItemScriptableObject item in startingItems)
        {
            if (item != null) 
                Add(item);
        }
    }

    public InventoryItem Get(ItemScriptableObject refenceData)
    {
        if (m_itemDictionary.TryGetValue(refenceData, out InventoryItem value))
        {
            return value;
        }
        return null;

    }

    public bool Add(ItemScriptableObject refenceData) //Add items til inventory
    {
        if (m_itemDictionary.TryGetValue(refenceData, out InventoryItem value)) //Tjekker om item er i inventory
        {
            if (stacking)
            {
                value.AddToStack(); //Adder til en stack
                onInventoryChangedEvent?.Invoke();

                return true;
            }
        }
        else if(inventorySize > currentInvenotorySize)// laver en ny item, og add til inventory
        {
            InventoryItem newItem = new InventoryItem(refenceData);
            Inventory.Add(newItem);
            m_itemDictionary.Add(refenceData, newItem);

            // Shop can contain conflicting items, others can not
            if (!isShop)
            {
                HandleConflictingItems(refenceData);
                // Notify item thats its been acquired - apply passives etc.
                newItem.data.ItemAcquired(ModifierCtx(refenceData).Build());
            }

            onInventoryChangedEvent?.Invoke();

            return true;
        }
        return false;
    }

    // Throws any items that are in conflict
    private void HandleConflictingItems(ItemScriptableObject item)
    {
        foreach (ItemScriptableObject conflictingItem in item.ConflictingItems)
        {
            InventoryItem itemInInventory;
            if (!m_itemDictionary.TryGetValue(conflictingItem, out itemInInventory))
                continue; // Conflicting item not in inventory

            // Trow conflicting item
            ThrowItem(itemInInventory);
        }
    }

    private void ThrowItem(InventoryItem item)
    {
        Assert.IsNotNull(item.data.PickupPrefab, $"No Pickup prefab set when trying to throw {item.data.DisplayName}");
        Debug.Log($"Throwing {item.data.DisplayName}");

        var pickup = Instantiate(item.data.PickupPrefab);
        pickup.transform.position = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);

        Remove(item.data);
    }

    public UseModifierContextBuilder ModifierCtx(ItemScriptableObject item)
    {
        return new UseModifierContextBuilder()
            .WithItem(item)
            .WithInstantHealthReceiver(CombatPlayer.combatPlayer)
            .WithRegenerationHealthReceiver(CombatPlayer.combatPlayer)
            .WithAttackSpeedReceiver(CombatPlayer.combatPlayer)
            .WithDamageReceiver(CombatPlayer.combatPlayer)
            .WithMoveSpeedReceiver(CombatPlayer.combatPlayer)
            .WithThornsReceiver(CombatPlayer.combatPlayer)
            .WithAttackStrategyReceiver(CombatPlayer.combatPlayer.gameObject.GetComponent<PlayerAttack>());
    }



    public void Remove(ItemScriptableObject refenceData) //Fjerner item fra inventory
    {
        if (m_itemDictionary.TryGetValue(refenceData, out InventoryItem value)) //Tjekker om item er i inventory
        {
            value.RemoveFromStack(); //Fjerne 1 item fra stack

            if (value.stackSize == 0) //Hvis stack er 0, fjern item fra inventory
            {
                Inventory.Remove(value);
                m_itemDictionary.Remove(refenceData);
            }

            // remove passive
            value.data.LoseItem(ModifierCtx(refenceData).Build());

            onInventoryChangedEvent?.Invoke();
        }
    }
}
