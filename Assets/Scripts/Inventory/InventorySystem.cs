using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Ændre InventoryTestItem til noget andet

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem instance { get; private set; }

    public event Action onInventoryChangedEvent;
    private UseModifierContext modifierCtx;

    private Dictionary<ItemScriptableObject, InventoryItem> m_itemDictionary;
    public List<InventoryItem> inventory { get; private set; }
    public void Awake()
    {
        inventory = new List<InventoryItem>();
        m_itemDictionary = new Dictionary<ItemScriptableObject, InventoryItem>();

        if(instance != null && instance != this)//Singleton
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        modifierCtx = new UseModifierContextBuilder()
            .WithInstantHealthReceiver(CombatPlayer.combatPlayer as IInstantHealthReceiver)
            .WithRegenerationHealthReceiver(CombatPlayer.combatPlayer as IRegenerationReceiver)
            .WithAttackSpeedReceiver(CombatPlayer.combatPlayer as IAttackSpeedReceiver)
            .WithDamageReceiver(CombatPlayer.combatPlayer as IDamageReceiver)
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


    public void Add(ItemScriptableObject refenceData) //Add items til inventory
    {
        if (m_itemDictionary.TryGetValue(refenceData, out InventoryItem value)) //Tjekker om item er i inventory
        {
            value.AddToStack(); //Adder til en stack
        }
        else // laver en ny item, og add til inventory
        {
            InventoryItem newItem = new InventoryItem(refenceData);
            inventory.Add(newItem);
            m_itemDictionary.Add(refenceData, newItem);
            print($"Pick up: ¨{refenceData.name}");

            // add passive
            if (newItem.data.IsPassive)
            {
                newItem.data.UseAbility(modifierCtx);
            }
        }
        onInventoryChangedEvent?.Invoke();
    }

    public void Remove(ItemScriptableObject refenceData) //Fjerner item fra inventory
    {
        if (m_itemDictionary.TryGetValue(refenceData, out InventoryItem value)) //Tjekker om item er i inventory
        {
            value.RemoveFromStack(); //Fjerne 1 item fra stack

            if (value.stackSize == 0) //Hvis stack er 0, fjern item fra inventory
            {
                inventory.Remove(value);
                m_itemDictionary.Remove(refenceData);

            }

            // remove passive
            if (value.data.IsPassive)
            {
                value.data.LoseItem(modifierCtx);
            }

            onInventoryChangedEvent?.Invoke();

        }

    }
}
