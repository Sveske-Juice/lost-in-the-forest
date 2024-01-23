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
        GameObject projectile = Instantiate(prefabAttack, _context.origin.position, Quaternion.identity);
        projectile.transform.forward = dir.normalized;
    }
}
