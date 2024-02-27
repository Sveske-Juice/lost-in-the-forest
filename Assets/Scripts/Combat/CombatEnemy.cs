using UnityEngine;
using UnityEngine.Events;

// Dette vil v�re klassen som holder enemystats n�r spillet k�re
// Scriptet som Gabriel har lavet vil holde de enemysne som ikke er blevet intansiatet i nu
// -Morgan

public class CombatEnemy : MonoBehaviour, IDamageable, IHealthComponent
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

    UnityEvent<float, float> IHealthComponent.OnHealthChanged { get { return OnHealthChanged; } }

    public UnityEvent<float, float> OnHealthChanged;

    public int Health
    {
        get { return health; }
        private set { OnHealthChanged?.Invoke(health, value);  health = (int)Mathf.Clamp(value, 0f, MaxHealth); }
    }

    public Transform Transform => transform;

    public void TakeDamage(float _damage, IDamageable initiator)
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
            Destroy(gameObject);
        }
    }

    public float getMaxHealth()
    {
        return maxHealth;
    }
}
