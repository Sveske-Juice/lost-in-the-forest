using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Skrevet af Morgan ud fra Snorres klasse diagram

public interface IDamageable
{
    public Transform Transform { get; }
    public int Health { get; }

    public void TakeDamage(float _damage, IDamageable initiator);
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
