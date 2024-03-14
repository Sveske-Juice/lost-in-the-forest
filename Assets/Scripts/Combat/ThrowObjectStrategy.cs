using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/Throw Object Strategy", fileName = "throwobjectstrat")]
public class ThrowObjectStrategy : AttackStrategy
{
    [SerializeField] private float throwRange;
    [SerializeField] private AnimationCurve throwCurve;
    [SerializeField] private GameObject throwObjectPrefab;

    public override void Attack(AttackContext _context)
    {
        GameObject throwObj = Instantiate(throwObjectPrefab);
        Rigidbody2D throwRB = throwObj.GetOrAddComponent<Rigidbody2D>();
        CurveFollower curveFollower = throwObj.GetOrAddComponent<CurveFollower>();

        curveFollower.Init(throwRB, _context.attackDir, throwRange, throwCurve);
    }
}
