using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = "Modifier/health", fileName = "healthModifier")]
public class HealthItemModifier : Modifier
{
    [SerializeField] int healing = 0;
    [SerializeField] bool regenItem = false;
    [SerializeField] int regenRate = 0;
    [SerializeField] int regenAmount = 0;
    [SerializeField] int maxHeathIncrease = 0;

    public override void Apply()
    {
        CombatPlayer.combatPlayer.ModifyHealth(healing, maxHeathIncrease, regenItem, regenAmount, regenRate);         //ModifyHealth(int _healAmount, int _maxHealthInc, bool _regenItem,int _regenAmount, int _regenRate)
    }

    public override void Unapply()
    {
        regenItem = false;
        CombatPlayer.combatPlayer.ModifyHealth(-healing, -maxHeathIncrease, regenItem, regenAmount, regenRate);
    }
}
