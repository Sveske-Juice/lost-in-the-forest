using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(menuName = "attackStrategy/projectile", fileName = "ProjectileStrategy")]
public class ProjectileStrategy : AttackStrategy
{
    [SerializeField] private GameObject prefabAttack;
    public override void Attack(AttackContext _context)
    {
        Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - _context.origin.position;
        dir.z = 0;
        GameObject projectile = Instantiate(prefabAttack, _context.origin.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().AddForce(new Vector3(dir.normalized.x, dir.normalized.y, 0)* projectile.GetComponent<RangedAttack>().projectileSpeed);
    }
}
