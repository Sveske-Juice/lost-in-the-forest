using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageReceiver{
    public void DamageIncrease(float physicalDamage, float magicDamage);
}
[CreateAssetMenuAttribute(menuName = "Modifier/Damage Modifier", fileName = "Damage Modifier")]
public class DamageModifier : Modifier
{
    [Header("Damage")]
    [SerializeField] AnimationCurve magicDmg;
    [SerializeField] AnimationCurve physicialDmg;


    public override void Apply(UseModifierContext context)
    {
        context.damageReceiver.DamageIncrease(physicialDmg.Evaluate(context.item.Level), magicDmg.Evaluate(context.item.Level));
    }

    public override void Unapply(UseModifierContext context)
    {
        context.damageReceiver.DamageIncrease(-physicialDmg.Evaluate(context.item.Level), -magicDmg.Evaluate(context.item.Level));
    }
}
