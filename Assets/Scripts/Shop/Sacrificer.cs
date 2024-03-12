using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sacrificer : MonoBehaviour
{
    public void Sacrifice(ItemScriptableObject item)
    {
        // TODO : @morgan her kan du gøre hvad du skal gøre for level increase
        int level = item.Level+1;
        item.SetLevel(level);
        Debug.Log($"{item.DisplayName} lvl: {item.Level}");
    }
}
