using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = "Modifier/health", fileName = "healthModifier")]
public class HealthItemModifier : Modifier
{
    [Header("Item Type")]
    [SerializeField] bool consumable = false;
    [SerializeField] bool passive = false;

    [Header("Instant Healing")]
    [SerializeField] int healing = 0;

    [Header("Regeneration")]
    [SerializeField] bool regenItem = false;
    [SerializeField] int regenRate = 0;
    [SerializeField] int regenAmount = 0;
    [SerializeField] int regenDur = 0;

    [Header("Permanent Buffs")]
    [SerializeField] int maxHeathIncrease = 0;


    public override void Apply()
    {
        CombatPlayer.combatPlayer.ModifyHealth(healing, maxHeathIncrease, regenItem, regenAmount, regenRate, regenDur);         //ModifyHealth(int _healAmount, int _maxHealthInc, bool _regenItem,int _regenAmount, int _regenRate)
    }

    public override void Unapply()
    {
        regenItem = false;
        CombatPlayer.combatPlayer.ModifyHealth(-healing, -maxHeathIncrease, regenItem, regenAmount, regenRate, regenDur);
    }
}
