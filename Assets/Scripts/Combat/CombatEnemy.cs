using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Dette vil være klassen som holder enemystats når spillet køre
// Scriptet som Gabriel har lavet vil holde de enemysne som ikke er blevet intansiatet i nu
// -Morgan

public class CombatEnemy : MonoBehaviour, IDamageable
{
    public int health { get; private set; }
    private int maxHealth;
    private float speed;
    private int strenght;

    public EnemyStats Enemy;

    public void TakeDamage(int _damage)
    {
        this.health -= _damage;
    }

    public bool Heal(int _heal)
    {
        this.health += _heal;

        return true; //temp
    }

    void Start()
    {
        this.health = Enemy.health;
        this.maxHealth = Enemy.maxHealth;
        this.speed = Enemy.moveSpeed;
        this.strenght = Enemy.strenght;
    }

    void Update()
    {
        if (this.health <= 0) //virker ikke helt
        {
            //Destroy(this);
        }
    }
}
