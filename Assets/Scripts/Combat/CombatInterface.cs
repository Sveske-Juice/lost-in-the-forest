using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Skrevet af Morgan ud fra Snorres klasse diagram

public interface IDamageable
{
    public int health { get; set; } //Her skal i lade som om at set er private. I MÅ IKKE SÆTTE health UDEN FOR TakeDamage() OG Heal().

    public void TakeDamage(int _damage);
    public bool Heal(int _heal);
}
/*
public interface IEnemy : IDamageable
{
    private int speed;
    private int strenght;
}
*/
/*
public interface IHurtTrigger
{
    private int damage { set; }
    public int damage { get; }
}*/