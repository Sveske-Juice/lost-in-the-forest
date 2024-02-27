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
    [SerializeField] float moveSpeed = 0;
    public override void Apply(UseModifierContext context)
    {
        context.moveSpeedReceiver.MoveSpeedIncrease(moveSpeed);
    }

    public override void Unapply(UseModifierContext context)
    {
        context.moveSpeedReceiver.MoveSpeedIncrease(-moveSpeed);
    }
}
