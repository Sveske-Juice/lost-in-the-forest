using UnityEngine;

[CreateAssetMenu(menuName = "attackStrategy/projectile", fileName = "ProjectileStrategy")]
public class ProjectileStrategy : AttackStrategy
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 1f;

    // TODO: burst functionality (ex. spawn 3 projectiles)

    public override void Attack(AttackContext _context)
    {
        Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - _context.origin.position;
        dir.z = 0;
        dir.Normalize();

        GameObject projectile = Instantiate(projectilePrefab, _context.origin.position, Quaternion.identity);
        Debug.DrawRay(_context.origin.position, dir, Color.magenta, 5f);

        projectile.transform.up = dir;
        projectile.GetComponent<Rigidbody2D>().AddForce(dir * projectileSpeed);
    }
}
