using UnityEngine;

public class EnemyAttackAI : MonoBehaviour
{
    [SerializeField] private AttackStrategy[] attacks;
    [SerializeField] private CombatEnemy combatEnemy;

    public EnemyStats enemyStats;
    float attackSeconds;

    private void Attack()
    {
        AttackContextBuilder builder = new();
        AttackContext context = builder
            .WithOrigin(transform)
            .WithInitiator(combatEnemy)
            .WithAttackDir(CombatPlayer.combatPlayer.transform.position - transform.position)
            .WithPhysicalDamge(enemyStats.strength)
            .WithMagicalDamage(enemyStats.strength)
            .Build();

        attacks[(int)Random.Range(0, attacks.Length - 1)].Attack(context);
    }

    private void Update()
    {
        attackSeconds += Time.deltaTime;
        if (attackSeconds > enemyStats.attackDelay)
        {
            Attack();
            attackSeconds = 0;
        }
    }
}
