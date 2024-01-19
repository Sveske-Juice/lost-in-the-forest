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

    public float attackDistance = 5f;
    public int attackDamage = 5;
    public Vector2 attackSize = new Vector2(1, 1);

    void Start()
    {
        player = GetComponent<CombatPlayer>();
        attackDamage = player.GetPhysicalDamage(0);
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

        if (isAttacking && attackTime >= player.attackSpeed)
        {
            isAttacking = false;
        }
    }

    void Attack()
    {
        Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, attackSize, 0f, dir.normalized, attackDistance);
        if (hit.collider == null) return;

        Debug.Log($"hit: {hit.collider.name}");
        IDamageable damageable = hit.collider.gameObject.GetComponent<IDamageable>();

        // The hit object can be damaged
        if (damageable != null)
        {
            damageable.TakeDamage(attackDamage);
        }
    }

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
    }
}
