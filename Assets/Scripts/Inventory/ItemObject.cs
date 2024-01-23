using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public InventoryTestItem referenceItem;

    public void OnHandlePickupItem()
    {
        InventorySystem.instance.Add(referenceItem);
        Destroy(gameObject);
    }
  
}
