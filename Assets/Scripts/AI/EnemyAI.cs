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

    Animator animator;
    int hopHash; // when hoppings is enabled
    int moveHash; // when moving normally

    public string hopId = "IsHopping";
    public string walkId = "IsWalking";

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

        animator = GetComponent<Animator>();
        hopHash = Animator.StringToHash(hopId);
        moveHash = Animator.StringToHash(walkId);
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
                        agent.isStopped = false;
                        agent.SetDestination(target.position);
                        hasHopTarget = true;
                        animator?.SetBool(hopHash, true);
                    }

                    if (hopSecondsDelayed >= hopDelay + 0.5)
                    {
                        agent.isStopped = true;
                        hopSecondsDelayed = 0;
                        hasHopTarget = false;
                        animator?.SetBool(hopHash, false);
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
        if (InAttackRange() == true)
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
                // Attack();
            }
            if (!InAttackRange())
            {
                StartMovement();
            }
        }
    }

    private bool InAttackRange()
    {
        return Vector2.Distance(target.position, this.transform.position) <= enemyStats.attackRange;
    }

    //Starter enemy bevægelse og skubbelighed
    private void StartMovement()
    {
        moving = true;
        if (hopMovement != true)
        {
            agent.isStopped = false;
        }
        animator?.SetBool(moveHash, true);
    }

    //Stopper enemy bevægelse og skubbelighed
    private void StopMovement()
    {
        moving = false;
        agent.isStopped = true;
        animator?.SetBool(moveHash, false);
    }


    public float GetDamage()
    {
        return damage;
    }
}
