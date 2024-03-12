using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPickups : MonoBehaviour
{
    [SerializeField]
    private bool dropOnDestroy = false;

    [SerializeField]
    private ItemDrop[] drops;

    public void Drop()
    {
        foreach (var drop in drops)
        {
            float dropChance = UnityEngine.Random.Range(0f, 1f);
            if (dropChance <= drop.dropChance)
            {
                GameObject go = Instantiate(drop.drop);
                go.transform.position = transform.position;
            }
        }
    }

    private void OnDestroy()
    {
        if (dropOnDestroy)
        {
            Drop();
        }
    }
}

[Serializable]
public class ItemDrop
{
    public GameObject drop;

    [Range(0f, 1f)]
    public float dropChance = 1f;
}
