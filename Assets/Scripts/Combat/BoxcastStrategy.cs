using UnityEngine;

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
        Vector3 dir = _context.attackDir;
        dir.z = 0f;
        dir.Normalize();

        SpawnVisual(dir, _context);

        //attackLayers |= LayerMask.NameToLayer(_context.player != null ? "Player" : "Enemy");
        RaycastHit2D[] hits = Physics2D.BoxCastAll(_context.origin.position, attackSize, 0f, dir, attackDistance, attackLayers);
        Debug.DrawRay(_context.origin.position, dir * attackDistance + dir * attackSize.magnitude, Color.red, 5f);
        foreach (var hit in hits)
        {
            if (hit.transform == _context.initiator.Transform) continue;

            Debug.Log($"hit: {hit.collider.name}");
            IDamageable damageable = hit.collider.gameObject.GetComponent<IDamageable>();

            // The hit object can be damaged
            if (damageable != null)
            {
                damageable.TakeDamage(_context.physicalDamage, _context.initiator);
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
