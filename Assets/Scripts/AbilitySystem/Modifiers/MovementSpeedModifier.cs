using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveSpeedReceiver
{
    public void MoveSpeedIncrease(float moveSpeed);
}
[CreateAssetMenuAttribute(menuName = "Modifier/Move Speed Modifier", fileName = "Move Speed Modifier")]
public class MovementSpeedModifier : Modifier
{
    [Header("MoveSpeedModifier"), Range(-100, 100)]
    [SerializeField] AnimationCurve moveSpeed;
    public override void Apply(UseModifierContext context)
    {
        context.moveSpeedReceiver.MoveSpeedIncrease(moveSpeed.Evaluate(context.item.Level));
    }

    public override void Unapply(UseModifierContext context)
    {
        context.moveSpeedReceiver.MoveSpeedIncrease(-moveSpeed.Evaluate(context.item.Level));
    }
}
