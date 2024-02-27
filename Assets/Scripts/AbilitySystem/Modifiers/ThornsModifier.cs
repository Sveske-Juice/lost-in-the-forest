using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IThornsReceiver
{
    public void ThornsIncrease(float thornsScale);
}
[CreateAssetMenuAttribute(menuName = "Modifier/Thorns Modifier", fileName = "Thorns Modifier")]
public class ThornsModifier : Modifier
{
    [Header("Thorns")]
    [SerializeField, Range(0, 2)] float thornsScale = 0;

    public override void Apply(UseModifierContext context)
    {
        context.thornsReceiver.ThornsIncrease(thornsScale);
    }

    public override void Unapply(UseModifierContext context)
    {
        context.thornsReceiver.ThornsIncrease(-thornsScale);
    }
}
