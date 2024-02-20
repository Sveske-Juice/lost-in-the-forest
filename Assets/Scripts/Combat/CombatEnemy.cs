using UnityEngine;

// Dette vil v�re klassen som holder enemystats n�r spillet k�re
// Scriptet som Gabriel har lavet vil holde de enemysne som ikke er blevet intansiatet i nu
// -Morgan

public class CombatEnemy : MonoBehaviour, IDamageable
{

    [SerializeField]
    public float moveSpeed = 0;

    [SerializeField]
    public float attackRange = 0;

    [SerializeField]
    public float attackDelay = 0;

    [SerializeField]
    public bool canMoveWhileAttacking = false;
    [SerializeField]
    public int strength = 1;

    [SerializeField]
    public int health = 10;
    [SerializeField]
    public int maxHealth = 10;
    public int MaxHealth => maxHealth;

    public int Health
    {
        get { return health; }
        private set { health = (int)Mathf.Clamp(value, 0f, MaxHealth); }
    }

    public void TakeDamage(float _damage)
    {
        this.Health -= (int)_damage;
    }

    public bool Heal(int _heal)
    {
        this.Health += _heal;

        return true; //temp
    }

    void Update()
    {
        if (this.Health <= 0) //virker ikke helt
        {
            Destroy(this);
        }
    }
}
