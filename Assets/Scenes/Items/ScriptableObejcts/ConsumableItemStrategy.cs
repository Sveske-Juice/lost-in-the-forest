using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = "ItemStrategy/consumableStrategy", fileName = "consumable")]
public class ConsumableItemStrategy : ItemStrategy
{
    [SerializeField] Modifier[] modifiers;
    public override void UseItem()
    {
        for (int i = 0; i < modifiers.Length; i++)
        {
            modifiers[i].Apply();
        }
        //TODO: Unapply 
    }
}
