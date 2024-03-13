// Script for enemy bevægelses- og angrebsadfærd
// Gør brug af et 2D NavMesh addon hentet fra https://github.com/h8man/NavMeshPlus
// -Gabriel

using UnityEngine;
using UnityEngine.AI;

public class HappyOSAttacks : MonoBehaviour
{
    [SerializeField] private AttackStrategy[] attacks;
    public EnemyStats enemyStats;
    float attackSeconds;
    private void Attack()
    {
        AttackContextBuilder builder = new();
        AttackContext context = builder
            .WithOrigin(transform)
            .WithInitiator(GetComponent<CombatEnemy>())
            .WithAttackDir(CombatPlayer.combatPlayer.transform.position - transform.position)
            .WithPhysicalDamge(GetComponent<EnemyAI>().enemyStats.strength)
            .WithMagicalDamage(GetComponent<EnemyAI>().enemyStats.strength)
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
