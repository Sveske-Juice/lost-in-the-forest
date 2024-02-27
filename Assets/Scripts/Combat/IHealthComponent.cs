using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using System;

public interface IHealthComponent
{
    public UnityEvent<float, float> OnHealthChanged { get; }
    public float getMaxHealth();
}
