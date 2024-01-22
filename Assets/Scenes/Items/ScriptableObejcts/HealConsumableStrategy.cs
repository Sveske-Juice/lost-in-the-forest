using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealConsumableStrategy : ItemStrategy
{
    [SerializeField] int healValue = 0;

    public override void UseItem()
    {
        throw new System.NotImplementedException();
        //Logik
    }
}
