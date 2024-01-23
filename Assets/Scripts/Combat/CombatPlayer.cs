using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Skrevet af Morgan ud fra Snorres klasse diagram
// Disse to scripts er for at holde data p� Playeren og Enemyen

public class CombatPlayer : MonoBehaviour, IDamageable
{
    [SerializeField] private int speed;
    [SerializeField] private int intelligence;
    [SerializeField] private int strength;
    [SerializeField] private float attackSpeed;
    public float AttackSpeed => attackSpeed; // dette vil v�re hvor lang tid der er imellem vert angreb
    //private List<Item> inventory = new List<Item> (); // dette er en arrayliste. Skal bruge en klasse kaldt Item
    [SerializeField] private int maxhealth;
    public int MaxHealth => health; //Her skal i lade som om at set er private. I M� IKKE S�TTE health UDEN FOR TakeDamage() OG Heal().
    public int health { get; private set; } // Det er ikke meningen at maxHealth skal manipuleres s� ofte. Kun med Items og/eller level up 


    private void Awake()
    {
        this.health = this.MaxHealth;
    }
    /*
    public bool GiveItem(item _item)
    {
        //Her vil der nok v�re noget logic til at give items v�k
        //Jeg lader den forblive tom for nu
    }*/

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
}
