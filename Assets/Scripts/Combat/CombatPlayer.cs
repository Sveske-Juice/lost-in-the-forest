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

    //private List<Item> inventory = new List<Item> (); // dette er en arrayliste. Skal bruge en klasse kaldt Item
    [SerializeField]
    private int maxhealth;
    public int MaxHealth => health; //Her skal i lade som om at set er private. I MÅ IKKE SÆTTE health UDEN FOR TakeDamage() OG Heal().
    public int health { get; private set; } // Det er ikke meningen at maxHealth skal manipuleres så ofte. Kun med Items og/eller level up

    private void Awake()
    {
        this.health = this.MaxHealth;

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

    // FIXME: outdated
    public void ModifyHealth(
        int _healAmount,
        int _maxHealthInc,
        bool _regenItem,
        int _regenAmount,
        int _regenRate,
        int _regenDur
    )
    {
        health += _healAmount;
        maxhealth += _maxHealthInc;

        if (_regenItem)
            StartCoroutine(Regenerate(_regenAmount, _regenRate, _regenDur));

        IEnumerator Regenerate(int _regenAmount, int _regenRate, float _regenDur)
        {
            float timeHealed = 0f;
            while (timeHealed < _regenDur)
            {
                health += _regenAmount;
                yield return new WaitForSeconds(_regenRate);
                timeHealed += _regenRate;
            }
        }
    }

    public void TakeDamage(int _damage)
    {
        this.health -= _damage;
    }

    public bool Heal(int _heal)
    {
        this.health += _heal;

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
        throw new System.NotImplementedException();
    }

    public void Regeneration(int regenAmount, int regenRate, int regenDuration)
    {
        throw new System.NotImplementedException();
    }
}
