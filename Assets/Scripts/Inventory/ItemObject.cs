using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public ItemScriptableObject referenceItem;

    // TODO: add to collided inventory
    public void OnHandlePickupItem()
    {
        InventorySystem.instance.Add(referenceItem);
        Destroy(gameObject);
    }
}
