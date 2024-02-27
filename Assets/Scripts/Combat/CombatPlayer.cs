using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

// Skrevet af Morgan ud fra Snorres klasse diagram
// Disse to scripts er for at holde data p� Playeren og Enemyen

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
    private float speed;

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

    [SerializeField]
    private int maxHealth;
    public int MaxHealth => maxHealth;

    private int health;

    public int Health
    {
        get { return health; }
        // Clamp to max health
        private set { health = (int)Mathf.Clamp(value, 0f, MaxHealth); }
    }

    private void Awake()
    {
        // Start health at max
        this.Health = this.MaxHealth;

        if (combatPlayer != null && combatPlayer != this)
            Destroy(this);
        else
            combatPlayer = this;
    }

    /*
    public bool GiveItem(item _item)
    {
        //Her vil der nok v�re noget logic til at give items v�k
        //Jeg lader den forblive tom for nu
    }*/

    public void TakeDamage(float _damage)
    {
        this.Health -= (int) _damage;
    }

    public bool Heal(int _heal)
    {
        this.Health += _heal;

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
        this.Health += amount;
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
                this.Health += regenAmount;
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
}
