using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItemController : MonoBehaviour
{
    public InventorySystem Inventory;


    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Collectable"))
        {
            ItemObject item = collider.gameObject.GetComponent<ItemObject>();
            Inventory.Add(item.referenceItem);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        
    }

}
