using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Dette script bliver brugt til at spawne playerens attack
// -Morgan

public class PlayerAttack : MonoBehaviour
{
    private CombatPlayer player;
    private bool isAttacking = false;
    private float attackTime = 0;

    [SerializeField] private AttackStrategy leftAttack;

    void Start()
    {
        player = GetComponent<CombatPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isAttacking)
            {
                isAttacking = true;
                attackTime = 0;
                // Instantiate(physicalAttack, transform.position, transform.rotation);
                Attack();
            }
            
        }
        attackTime += Time.deltaTime;

        if (isAttacking && attackTime >= player.AttackSpeed)
        {
            isAttacking = false;
        }
    }

    void Attack()
    {
        AttackContext context = new AttackContext(transform, player);
        leftAttack.Attack(context);
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
