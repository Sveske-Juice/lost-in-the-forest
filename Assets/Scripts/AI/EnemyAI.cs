// Script for enemy bevægelses- og angrebsadfærd
// Gør brug af et 2D NavMesh addon hentet fra https://github.com/h8man/NavMeshPlus
// -Gabriel

using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    float moveSpeed;
    bool moving;
 
    float attackRange;
    float attackDelay;
    float secondsDelayed;
    float hopDelay;
    float hopSecondsDelayed;
    bool attacking;
    bool canMoveWhileAttacking;
    bool hopMovement;
    bool hasHopTarget;

    public EnemyStats enemyStats;

    Transform target;
    NavMeshAgent agent;
    NavMeshObstacle obstacle;

    float damage;
    [SerializeField] private AttackStrategy[] attacks;

    private void Start()
    {
        //Sætter startværdier
        moving = true;
        secondsDelayed = 0;
        hopSecondsDelayed = 0;
        attacking = false;
        hasHopTarget = false;

        //Sætter værdier fra statblock
        moveSpeed = enemyStats.moveSpeed;
        attackRange = enemyStats.attackRange;
        attackDelay = enemyStats.attackDelay;
        hopDelay = enemyStats.hopDelay;
        canMoveWhileAttacking = enemyStats.canMoveWhileAttacking;
        hopMovement = enemyStats.hopMovement;
        damage = enemyStats.strength;

        //NavMesh
        target = CombatPlayer.combatPlayer.transform;
        agent = GetComponent<NavMeshAgent>();
        obstacle = GetComponent<NavMeshObstacle>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        //Moving state
        if (moving == true)
        {
            if (hopMovement == true)
            {
                hopSecondsDelayed += Time.deltaTime;
                if (hopSecondsDelayed >= hopDelay)
                {
                    if (hasHopTarget == false)
                    {
                        //obstacle.enabled = false;
                        agent.enabled = true;
                        agent.SetDestination(target.position);
                        hasHopTarget = true;
                    }

                    if (hopSecondsDelayed >= hopDelay + 0.5)
                    {
                        agent.enabled = false;
                        //obstacle.enabled = true;
                        hopSecondsDelayed = 0;
                        hasHopTarget = false;
                    }
                }
            }
            else
            {
                //Sætter bevægelses-målet til spillerens position
                agent.SetDestination(target.position);
            }
        }
        

        //Aktiverer attacking state hvis target er halvvejs indenfor angrebsrækkeviden
        if (canAttack() == true)
        {
            attacking = true;
        }

        //Attacking state
        if (attacking == true) {
            //Stopper bevægelse i løbet af attacking state hvis canMoveWhileAttacking ikke er sandt
            if (canMoveWhileAttacking != true)
            {
                StopMovement();
            }
            
            //Håndterer tiden brugt i attacking state, og angriber hvis det når attackDelay
            secondsDelayed += Time.deltaTime;
            if (secondsDelayed >= attackDelay)
            {
                Attack();
            }
        }
    }

    private bool canAttack()
    {
        float distanceToTarget = Vector2.Distance(target.position, this.transform.position);
        return (distanceToTarget <= attackRange / 2) ;
    }

    private void Attack()
    {
        AttackContextBuilder builder = new();
        AttackContext context = builder
            .WithOrigin(transform)
            .WithInitiator(GetComponent<CombatEnemy>())
            .WithAttackDir(CombatPlayer.combatPlayer.transform.position -  transform.position)
            .WithPhysicalDamge(enemyStats.strength)
            .WithMagicalDamage(enemyStats.strength)
            .Build();

        attacks[(int)Random.Range(0, attacks.Length-1)].Attack(context);

        attacking = false;
        secondsDelayed = 0;
        if (canAttack() != true)
        {
            StartMovement();
        }
    }

    //Starter enemy bevægelse og skubbelighed
    private void StartMovement()
    {
        moving = true;
        if (hopMovement != true)
        {
            //obstacle.enabled = false;
            agent.enabled = true;
        }
    }

    //Stopper enemy bevægelse og skubbelighed
    private void StopMovement()
    {
        moving = false;
        agent.enabled = false;
        //obstacle.enabled = true;
    }


    public float GetDamage()
    {
        return damage;
    }
}
