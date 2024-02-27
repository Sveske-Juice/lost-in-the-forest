using UnityEngine;

// Dette script bliver brugt til at spawne playerens attack
// -Morgan

public class PlayerAttack : MonoBehaviour
{
    private bool isAttacking = false;
    private float attackTime = 0;

    [SerializeField] private AttackStrategy leftAttack;
    [SerializeField] private AttackStrategy rightAttack;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isAttacking)
            {
                isAttacking = true;
                attackTime = 0;
                PhysicalAttack();
            }
        }
        if (Input.GetMouseButtonDown(1)) {  
            if (!isAttacking)
            {
                isAttacking = true;
                attackTime = 0;
                RangeAttack();
            }
        }
        attackTime += Time.deltaTime;

        if (isAttacking && attackTime >= CombatPlayer.combatPlayer.AttackDelay)
        {
            isAttacking = false;
        }
    }

    void PhysicalAttack()
    {
        AttackContextBuilder builder = new();
        AttackContext context = builder
            .WithOrigin(transform)
            .WithInitiator(CombatPlayer.combatPlayer)
            .WithAttackDir(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position)
            .WithPhysicalDamge(CombatPlayer.combatPlayer.GetPhysicalDamage())
            .WithMagicalDamage(CombatPlayer.combatPlayer.GetMagicalDamage())
            .Build();

        leftAttack.Attack(context);
    }

    void RangeAttack()
    {
        AttackContextBuilder builder = new();
        AttackContext context = builder
            .WithOrigin(transform)
            .WithInitiator(CombatPlayer.combatPlayer)
            .WithAttackDir(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position)
            .WithPhysicalDamge(CombatPlayer.combatPlayer.GetPhysicalDamage())
            .WithMagicalDamage(CombatPlayer.combatPlayer.GetMagicalDamage())
            .Build();

        rightAttack.Attack(context);
    }
    /*
    void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        Vector2 mousePos = Input.mousePosition;
        Camera mainCam = Camera.main;

        Vector3 dir = mainCam.ScreenToWorldPoint(mousePos) - transform.position;
        dir.Normalize();

        Gizmos.DrawWireCube(transform.position, attackSize);
        Gizmos.DrawLine(transform.position, transform.position + dir * attackDistance);

        Gizmos.DrawWireCube(transform.position + dir * attackDistance, attackSize);
    }*/
}
