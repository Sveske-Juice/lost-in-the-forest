using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "attackStrategy/boxcast", fileName = "boxcastStrategy")]
public class BoxcastStrategy : AttackStrategy
{
    [SerializeField] private Vector2 attackSize = new Vector2 (1, 1);
    [SerializeField] private float attackDistance = 5.0f;


    public override void Attack(AttackContext _context)
    {
        Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - _context.origin.position;
        RaycastHit2D hit = Physics2D.BoxCast(_context.origin.position, attackSize, 0f, dir.normalized, attackDistance);
        if (hit.collider == null) return;

        Debug.Log($"hit: {hit.collider.name}");
        IDamageable damageable = hit.collider.gameObject.GetComponent<IDamageable>();

        // The hit object can be damaged
        if (damageable != null)
        {
            damageable.TakeDamage(_context.player.GetPhysicalDamage(0));
        }
    }
}
