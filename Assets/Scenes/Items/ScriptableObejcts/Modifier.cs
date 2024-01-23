using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Modifier : ScriptableObject
{
    public abstract void Apply();
    public abstract void Unapply();
}
