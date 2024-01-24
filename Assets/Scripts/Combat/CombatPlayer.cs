using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Skrevet af Morgan ud fra Snorres klasse diagram
// Disse to scripts er for at holde data på Playeren og Enemyen

public class CombatPlayer
    : MonoBehaviour,
        IDamageable,
        IInstantHealthReceiver,
        IRegenerationReceiver
{
    public static CombatPlayer combatPlayer { get; private set; }

    [SerializeField]
    private int speed;

    [SerializeField]
    private int intelligence;

    [SerializeField]
    private int strength;

    [SerializeField]
    private float attackSpeed;
    public float AttackSpeed => attackSpeed; // dette vil være hvor lang tid der er imellem vert angreb

    [SerializeField]
    private int maxHealth;
    public int MaxHealth => maxHealth;

    private int health;

    public int Health
    {
        get { return health; }

        // Clamp to max health
        private set { health = (int) Mathf.Clamp(value, 0f, MaxHealth); }
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
        //Her vil der nok være noget logic til at give items væk
        //Jeg lader den forblive tom for nu
    }*/

    public void TakeDamage(int _damage)
    {
        this.Health -= _damage;
    }

    public bool Heal(int _heal)
    {
        this.Health += _heal;

        return true; //temp
    }

    public int GetPhysicalDamage(int _modifiers)
    {
        int damage;
        damage = this.strength + _modifiers;
        return damage;
    }

    public int GetMagicalDamage(int _modifiers)
    {
        int damage;
        damage = this.intelligence + _modifiers;
        return damage;
    }

    public void InstantHeal(int amount)
    {
        this.Health += amount;
    }

    public void Regeneration(int regenAmount, int regenRate, int regenDuration)
    {
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
}
