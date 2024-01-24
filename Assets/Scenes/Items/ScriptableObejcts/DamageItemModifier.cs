using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = "Modifier/damage", fileName = "damageModifier")]
public class DamageItemModifier : Modifier
{
    [Header("Item Type")]
    [SerializeField] bool consumable = false;
    [SerializeField] bool passive = false;

    [Header("Magic Damage")]
    [SerializeField] int poisonDmg = 0;
    [SerializeField] int fireDmg = 0;
    [SerializeField] int lightningDmg = 0;

    [Header("Physical Damage")]
    [SerializeField] int physicialDmg = 0;

    [Header("Temp Buffs")]
    [SerializeField] int buffDuration = 0;
    [SerializeField] int damagePctModifierTemp = 0;

    [Header("Permanent Buffs")]
    [SerializeField] int damagePctModifierPerm = 0;

    [Header("Projectile Attack (Fireball, Throwing Knife, etc.)")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] bool specialAttack = false;
    [SerializeField] int attackRange = 0;

    [Header("Magic Modifier")]
    [SerializeField] int projectileScale = 0;



    public override void Apply()
    {
        throw new System.NotImplementedException();
    }

    public override void Unapply()
    {
        throw new System.NotImplementedException();
    }
}
