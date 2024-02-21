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
    [SerializeField,Range(-100,100)] float magicDmg = 0;
    [SerializeField,Range(-100,100)] float physicialDmg = 0;


    public override void Apply(UseModifierContext context)
    {
        context.damageReceiver.DamageIncrease(physicialDmg, magicDmg);
    }

    public override void Unapply(UseModifierContext context)
    {
        context.damageReceiver.DamageIncrease(-physicialDmg, -magicDmg);
    }
}
