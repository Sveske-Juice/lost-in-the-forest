using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class InventorySystem : MonoBehaviour
{
    public event Action onInventoryChangedEvent;
    private UseModifierContext modifierCtx;

    private Dictionary<ItemScriptableObject, InventoryItem> m_itemDictionary = new();
    public List<InventoryItem> Inventory { get; private set; } = new();
    public GameObject tooltipPrefab;

    public int inventorySize = 3;
    static public int currentInvenotorySize = 0;

    private void Awake()
    {
    }

    private void Start()
    {
        modifierCtx = new UseModifierContextBuilder()
            .WithInstantHealthReceiver(CombatPlayer.combatPlayer)
            .WithRegenerationHealthReceiver(CombatPlayer.combatPlayer)
            .WithAttackSpeedReceiver(CombatPlayer.combatPlayer)
            .WithDamageReceiver(CombatPlayer.combatPlayer)
            .WithMoveSpeedReceiver(CombatPlayer.combatPlayer)
            .Build();
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
            value.AddToStack(); //Adder til en stack
            onInventoryChangedEvent?.Invoke();

            return true;
        }
        else if(inventorySize > currentInvenotorySize)// laver en ny item, og add til inventory
        {
            InventoryItem newItem = new InventoryItem(refenceData);
            Inventory.Add(newItem);
            m_itemDictionary.Add(refenceData, newItem);
            currentInvenotorySize++;

            // Notify item thats its been acquired - apply passives etc.
            newItem.data.ItemAcquired(modifierCtx);
            onInventoryChangedEvent?.Invoke();

            return true;
        }   
        return false;
        
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
                currentInvenotorySize--;
            }

            // remove passive
            value.data.LoseItem(modifierCtx);

            onInventoryChangedEvent?.Invoke();
        }
    }
}
