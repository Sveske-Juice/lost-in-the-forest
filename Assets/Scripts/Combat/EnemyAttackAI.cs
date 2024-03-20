using System.Linq;
using UnityEngine;

public class EnemyAttackAI : MonoBehaviour
{
    [SerializeField] private AttackStrategy[] attacks;
    [SerializeField] private CombatEnemy combatEnemy;

    public EnemyStats enemyStats;
    float attackSeconds;

    private void RandomAttack(AttackStrategy[] strategies)
    {
        if (strategies?.Length == 0) return;
        Attack(strategies[(int)Random.Range(0, strategies.Length)]);
    }

    private void Attack(AttackStrategy strategy)
    {
        AttackContextBuilder builder = new();
        AttackContext context = builder
            .WithOrigin(transform)
            .WithInitiator(combatEnemy)
            .WithAttackDir(CombatPlayer.combatPlayer.transform.position - transform.position)
            .WithPhysicalDamge(enemyStats.strength)
            .WithMagicalDamage(enemyStats.strength)
            .Build();

        strategy.Attack(context);
    }

    private void Update()
    {
        attackSeconds += Time.deltaTime;
        if (attackSeconds < enemyStats.attackDelay)
            return;

        float distToTarget = DistToTarget();
        AttackStrategy[] attacksInRange = attacks.Where(a => distToTarget <= a.AttackTriggerRange).ToArray();

        RandomAttack(attacksInRange);
        attackSeconds = 0;
    }

    public float DistToTarget() => Vector2.Distance(transform.position, CombatPlayer.combatPlayer.transform.position);
}
