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
    [SerializeField] AnimationCurve thornsScale;

    public override void Apply(UseModifierContext context)
    {
        context.thornsReceiver.ThornsIncrease(thornsScale.Evaluate(context.item.Level));
    }

    public override void Unapply(UseModifierContext context)
    {
        context.thornsReceiver.ThornsIncrease(-thornsScale.Evaluate(context.item.Level));
    }
}
