using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKilledNotifier : MonoBehaviour
{
    public static event Action BossKilled;

    protected void RaiseBossKilled() => BossKilled?.Invoke();
}
