using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[CreateAssetMenu(menuName = "ItemStrategy/Item Attack Strategy", fileName = "New item attack strat")]
public class AttackItemStrategy : ItemStrategy
{
    [SerializeField] private AttackStrategy attack;

    public override void UseItem(UseModifierContext useItemContext)
    {
        Debug.Log($"Using {useItemContext.attackContext} {useItemContext.item.DisplayName}");
        Assert.IsNotNull(useItemContext.attackContext, $"No attack context set for attacking item! {useItemContext.item.DisplayName}");

        attack.Attack(useItemContext.attackContext);
    }
}
