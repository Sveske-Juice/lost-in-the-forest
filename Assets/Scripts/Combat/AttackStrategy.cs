using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackStrategy : ScriptableObject
{
    public abstract void Attack(AttackContext _context);
}
