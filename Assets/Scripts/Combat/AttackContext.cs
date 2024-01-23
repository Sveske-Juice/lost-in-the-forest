using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackContext 
{
    public Transform origin {  get; private set; }
    public CombatPlayer player { get; private set; }

    public AttackContext(Transform _origin, CombatPlayer player)
    {
        this.origin = _origin;
        this.player = player;
    }
}
