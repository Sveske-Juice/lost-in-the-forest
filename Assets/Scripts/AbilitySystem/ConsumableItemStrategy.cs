using UnityEngine;

[CreateAssetMenuAttribute(menuName = "ItemStrategy/Consumable", fileName = "consumable")]
public class ConsumableItemStrategy : ItemStrategy
{
    [SerializeField] Modifier[] modifiers;

    public override void UnUseItem(UseModifierContext useItemContext)
    {
        for (int i = 0; i < modifiers.Length; i++)
        {
            modifiers[i].Unapply(useItemContext);
        }
    }

    public override void UseItem(UseModifierContext useItemContext)
    {
        for (int i = 0; i < modifiers.Length; i++)
        {
            modifiers[i].Apply(useItemContext);
        }
    }
}
