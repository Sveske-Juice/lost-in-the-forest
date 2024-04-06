using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

// Skrevet af Morgan ud fra Snorres klasse diagram
// Disse to scripts er for at holde data på Playeren og Enemyen
[RequireComponent(typeof(HealthComponent))]
public class CombatPlayer
    : MonoBehaviour,
        IDamageable,
        IInstantHealthReceiver,
        IRegenerationReceiver,
        IAttackSpeedReceiver,
        IDamageReceiver,
        IMoveSpeedReceiver,
        IThornsReceiver
{
    public static CombatPlayer combatPlayer { get; private set; }

    [SerializeField]
    public float speed = 5f;

    [SerializeField]
    private float intelligence;

    [SerializeField]
    private float strength;

    [SerializeField]
    private float attackSpeed;

    [SerializeField]
    private float thornsScale;

    [SerializeField]
    private AnimationCurve attackSpeedToDelay;

    /// <summary>
    /// The delay in seconds before it's possible to attack again
    /// </summary>
    public float AttackDelay => attackSpeedToDelay.Evaluate(attackSpeed);

    private HealthComponent healthComponent;
  
    public Transform Transform => transform;

    public int Health => (int) healthComponent.CurrentHealth;

    private void Awake()
    {
        healthComponent = GetComponent<HealthComponent>();
        Assert.IsNotNull(healthComponent);

        if (combatPlayer != null && combatPlayer != this)
            Destroy(gameObject);
        else
            combatPlayer = this;
    }

    /*
    public bool GiveItem(item _item)
    {
        //Her vil der nok være noget logic til at give items væk
        //Jeg lader den forblive tom for nu
    }*/

    public bool Heal(int _heal)
    {
        this.healthComponent.Modify(-_heal);

        return true; //temp
    }

    public float GetPhysicalDamage()
    {
        float damage;
        damage = this.strength;
        return damage;
    }

    public float GetMagicalDamage()
    {
        float damage;
        damage = this.intelligence;
        return damage;
    }

    public void InstantHeal(int amount)
    {
        Heal(amount);
    }

    public void Regeneration(int regenAmount, float regenRate, int regenDuration)
    {
        Assert.IsTrue(regenDuration > 0, $"regen duration must be above 0");
        Assert.IsTrue(regenRate > 0, $"regen rate must be above 0");

        StartCoroutine(Regen());

        IEnumerator Regen()
        {
            float time = 0f;
            while (time < regenDuration)
            {
                this.healthComponent.Modify(-regenAmount);
                yield return new WaitForSeconds(regenRate);
                time += regenRate;
            }
        }
    }

    public void AddAttackSpeed(float amount)
    {
        attackSpeed += amount;
    }

    public void DamageIncrease(float physicalDamage, float magicDamage)
    {
        strength += physicalDamage;  // Apply physical damage increase
        intelligence += magicDamage; //Apply magic damage increase
    }

    public void MoveSpeedIncrease(float _moveSpeed)
    {
        speed += _moveSpeed;
    }

    public void ThornsIncrease(float _thornsScale)
    {
        thornsScale += _thornsScale;
    }

    public void TakeDamage(float _damage, IDamageable initiator)
    {
        healthComponent.Modify(_damage);

        // reason for initiator is null is to avoid cyclic thorns being applied
        if (initiator != null)
            initiator.TakeDamage(_damage * thornsScale, initiator: null);
    }
}
