using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu (menuName = "attackStrategy/boxcast", fileName = "boxcastStrategy")]
public class BoxcastStrategy : AttackStrategy
{
    [SerializeField] private Vector2 attackSize = new Vector2 (1, 1);
    [SerializeField] private float attackDistance = 5.0f;
    [SerializeField] private GameObject visualAttack;
    [SerializeField] private float offset = 1.0f;

    public override void Attack(AttackContext _context)
    {
        Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - _context.origin.position;
        SpawnVisual(dir.normalized, _context);
        RaycastHit2D hit = Physics2D.BoxCast(_context.origin.position, attackSize, 0f, dir.normalized, attackDistance);
        if (hit.collider == null) return;

        Debug.Log($"hit: {hit.collider.name}");
        IDamageable damageable = hit.collider.gameObject.GetComponent<IDamageable>();

        // The hit object can be damaged
        if (damageable != null && damageable != (CombatPlayer.combatPlayer as IDamageable))
        {
            damageable.TakeDamage(_context.player.GetPhysicalDamage(0));
        }
    }

    private void SpawnVisual(Vector3 _dir, AttackContext _context)
    {
        Vector3 attackPos = _context.origin.position + _dir * offset;
        GameObject projectile = Instantiate(visualAttack, attackPos, Quaternion.identity);
        projectile.transform.rotation = Quaternion.Euler(0, 0, AngleBetweenMouseAndPlayer(_context)+90);
    }

    private float AngleBetweenMouseAndPlayer(AttackContext _context)
    {
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10f);

        float angle = AngleBetweenPoints(_context.origin.transform.position, mouseWorldPosition);

        return angle;
    }

    private float AngleBetweenPoints(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
