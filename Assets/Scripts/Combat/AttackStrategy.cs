using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackStrategy : ScriptableObject
{
    [SerializeField] private float attackTriggerRange = 5f;

    public float AttackTriggerRange => attackTriggerRange;
    public abstract void Attack(AttackContext _context);
}
