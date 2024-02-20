using UnityEngine;

// Dette vil være klassen som holder enemystats når spillet køre
// Scriptet som Gabriel har lavet vil holde de enemysne som ikke er blevet intansiatet i nu
// -Morgan

public class CombatEnemy : MonoBehaviour, IDamageable
{
    private int health;
    private int maxHealth;
    private float speed;
    private int strength;
    [SerializeField] private EnemyStats Enemy;

    public int Health
    {
        get { return health; }
        private set { health = (int)Mathf.Clamp(value, 0f, maxHealth); }
    }

    public void TakeDamage(int _damage)
    {
        this.Health -= _damage;
    }

    public bool Heal(int _heal)
    {
        this.Health += _heal;

        return true; //temp
    }

    void Start()
    {
        this.Health = Enemy.health;
        this.maxHealth = Enemy.maxHealth;
        this.speed = Enemy.moveSpeed;
        this.strength = Enemy.strength;
    }

    void Update()
    {
        if (this.Health <= 0) //virker ikke helt
        {
            Destroy(this);
        }
    }
}
