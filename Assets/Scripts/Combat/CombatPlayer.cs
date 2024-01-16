using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Skrevet af Morgan ud fra Snorres klasse diagram

public class CombatPlayer : IDamageable
{
    private int speed;
    private int intelligence;
    private int strength;
    //private List<Item> inventory = new List<Item> (); // dette er en arrayliste. Skal bruge en klasse kaldt Item

    public int health { get; set; } //Her skal i lade som om at set er private. I MÅ IKKE SÆTTE health UDEN FOR TakeDamage() OG Heal().

    /*
    public bool GiveItem(item _item)
    {
        //Her vil der nok være noget logic til at give items væk
        //Jeg lader den forblive tom for nu
    }*/

    public void TakeDamage(int _damage)
    {
        health -= _damage;
    }
    public bool Heal(int _heal)
    {
        health += _heal;

        return true; //temp
    }
}
