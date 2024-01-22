using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageConsumableStrategy : ItemStrategy
{
    [SerializeField] int damage = 0;

    public override void UseItem()
    {
        throw new System.NotImplementedException();
        //Logik
    }
}
