using UnityEngine;

public abstract class ItemStrategy : ScriptableObject
{
    public abstract void UseItem(UseModifierContext useItemContext);
    public abstract void UnUseItem(UseModifierContext useItemContext);
}
