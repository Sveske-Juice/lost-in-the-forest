using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Sacrificer : MonoBehaviour
{
    public UnityEvent OnSacrifice;

    public void Sacrifice(ItemScriptableObject item)
    {
        int level = item.Level+1;
        item.SetLevel(level);
        Debug.Log($"{item.DisplayName} lvl: {item.Level}");

        OnSacrifice?.Invoke();
    }
}
