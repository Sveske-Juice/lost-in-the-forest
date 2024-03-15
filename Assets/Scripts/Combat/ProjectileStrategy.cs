using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/projectile", fileName = "ProjectileStrategy")]
public class ProjectileStrategy : AttackStrategy
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 1f;

    // TODO: burst functionality (ex. spawn 3 projectiles)

    public override void Attack(AttackContext _context)
    {
        Vector3 dir = _context.attackDir;
        dir.z = 0;
        dir.Normalize();

        GameObject projectile = Instantiate(projectilePrefab, _context.origin.position, Quaternion.identity);
        Debug.DrawRay(_context.origin.position, dir, Color.magenta, 5f);

        projectile.transform.up = dir;
        projectile.GetComponent<Rigidbody2D>().AddForce(dir * projectileSpeed);
        RangedAttack rangeAttack = projectile.GetComponent<RangedAttack>();
        rangeAttack.attackCtx = _context;
    }
}
