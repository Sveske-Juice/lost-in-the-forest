using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Skrevet af Morgan ud fra Snorres klasse diagram
// Disse to scripts er for at holde data på Playeren og Enemyen

public class CombatPlayer : IDamageable
{
    private int speed;
    private int intelligence;
    private int strength;
    public float attackSpeed { get; private set; } // dette vil være hvor lang tid der er imellem vert angreb
    //private List<Item> inventory = new List<Item> (); // dette er en arrayliste. Skal bruge en klasse kaldt Item

    public int health { get; private set; } //Her skal i lade som om at set er private. I MÅ IKKE SÆTTE health UDEN FOR TakeDamage() OG Heal().
    public int maxHealth { get; private set; } // Det er ikke meningen at maxHealth skal manipuleres så ofte. Kun med Items og/eller level up 

    public CombatPlayer(int _speed, int _intelligence, int _strenght,int _health, float _attackSpeed)
    {
        this.speed = _speed;
        this.intelligence = _intelligence;
        this.strength = _strenght;
        this.health = _health;
        this.maxHealth = _health;
        this.attackSpeed = _attackSpeed;
    }
    public CombatPlayer() //statsne her er for at teste
    {
        speed = 5;
        intelligence = 1;
        strength = 1;
        health = 3;
        maxHealth = 3;
        attackSpeed = 1.0f;
    }

    /*
    public bool GiveItem(item _item)
    {
        //Her vil der nok være noget logic til at give items væk
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
/*
public class CombatEnemy : IDamageable
{
    public int health { get; private set; }

    public void TakeDamage(int _damage)
    {

    }
    public bool Heal(int _heal);
}*/