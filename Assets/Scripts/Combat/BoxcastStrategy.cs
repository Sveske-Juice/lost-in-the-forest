using System;
using UnityEngine;
using UnityEngine.Analytics;

[CreateAssetMenu (menuName = "Attacks/Box Cast Attack", fileName = "boxcastStrategy")]
public class BoxcastStrategy : AttackStrategy
{
    [SerializeField] private Vector2 attackSize = new Vector2 (1, 1);
    [SerializeField] private float attackDistance = 5.0f;
    [SerializeField] private GameObject visualAttack;
    [SerializeField] private float offset = 1.0f;
    [SerializeField] private LayerMask attackLayers;

    public override void Attack(AttackContext _context)
    {
        Vector3 dir;
        if (_context.enemyAI != null)
        {
            dir = CombatPlayer.combatPlayer.transform.position-_context.enemyAI.transform.position;
        }
        else
        {
            dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - _context.origin.position;
        }
        dir.z = 0f;
        dir.Normalize();

        SpawnVisual(dir, _context);

        //attackLayers |= LayerMask.NameToLayer(_context.player != null ? "Player" : "Enemy");
        RaycastHit2D[] hits = Physics2D.BoxCastAll(_context.origin.position, attackSize, 0f, dir, attackDistance, attackLayers);
        Transform initiator = _context.player == null ? _context.enemyAI.transform : _context.player.transform;
        foreach (var hit in hits)
        {
            if (hit.transform == initiator) continue;

            Debug.Log($"hit: {hit.collider.name}");
            IDamageable damageable = hit.collider.gameObject.GetComponent<IDamageable>();

            // The hit object can be damaged
            if (damageable != null)
            {
                float damage = _context.player == null ? _context.enemyAI.GetDamage() : _context.player.GetPhysicalDamage();
                damageable.TakeDamage(damage);
            }
        }
    }

    private void SpawnVisual(Vector3 _dir, AttackContext _context)
    {
        // Is optional
        if (visualAttack == null) return;

        Vector3 attackPos = _context.origin.position + _dir * offset;
        GameObject projectile = Instantiate(visualAttack, attackPos, Quaternion.identity);
        projectile.transform.up = _dir;
    }
}
