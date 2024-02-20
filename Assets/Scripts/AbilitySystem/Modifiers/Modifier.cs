using UnityEngine;

public abstract class Modifier : ScriptableObject
{
    public abstract void Apply(UseModifierContext context);
    public abstract void Unapply(UseModifierContext context);
}
