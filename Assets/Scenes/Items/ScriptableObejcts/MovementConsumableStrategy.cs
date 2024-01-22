using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementConsumableStrategy : ItemStrategy
{
    [SerializeField] float moveSpeedMultiplier = 1;

    public override void UseItem()
    {
        throw new System.NotImplementedException();
        //Logik
    }
}
