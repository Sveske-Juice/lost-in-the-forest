using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[System.Serializable]
public class BulletPattern
{
    [SerializeField]
    public float Arc = 15;
    [SerializeField]
    public int Amount = 3;
    [SerializeField]
    public float Speed = 1;
    [SerializeField]
    public GameObject bulletPrefab;

    [SerializeField]
    public bool Burst = true;
    [SerializeField]
    public float BurstDelay = 0.1f;
}


[CreateAssetMenu(menuName = "Attacks/projectile", fileName = "ProjectileStrategy")]
public class ProjectileStrategy : AttackStrategy
{
    [SerializeField] private BulletPattern[] bulletSequence;

    public override void Attack(AttackContext _context)
    {


        // TODO: move bullet spawner to AttackContext
        BulletSpawner bulletSpawner = _context.initiator.Transform.gameObject.GetComponent<BulletSpawner>();
        Assert.IsNotNull(bulletSpawner, $"Bullet spawner needs to be on initiatio {_context.initiator.Transform.name}");

        bulletSpawner.RunBulletSequence(_context, bulletSequence);

        //GameObject projectile = Instantiate(projectilePrefab, _context.origin.position, Quaternion.identity);
        //Debug.DrawRay(_context.origin.position, dir, Color.magenta, 5f);

        //projectile.transform.up = dir;
        //projectile.GetComponent<Rigidbody2D>().AddForce(dir * projectileSpeed);
        //RangedAttack rangeAttack = projectile.GetComponent<RangedAttack>();
        //rangeAttack.attackCtx = _context;
    }
}
