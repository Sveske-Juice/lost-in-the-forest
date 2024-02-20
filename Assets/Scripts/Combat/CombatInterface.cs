using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Skrevet af Morgan ud fra Snorres klasse diagram

public interface IDamageable
{
    public int Health { get; }

    public void TakeDamage(int _damage);
    public bool Heal(int _heal);
}
/*
public interface IEnemy : IDamageable
{
    private int speed;
    private int strength;
}
*/
/*
public interface IHurtTrigger
{
    private int damage { set; }
    public int damage { get; }
}*/
