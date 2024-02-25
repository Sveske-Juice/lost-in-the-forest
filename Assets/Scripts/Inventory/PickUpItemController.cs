using UnityEngine;

public class PickUpItemController : MonoBehaviour
{
    [SerializeField] InventorySystem passiveInventory;
    [SerializeField] InventorySystem activeInventory;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        ItemObject itemObject = collider.gameObject.GetComponent<ItemObject>();
        if (itemObject == null) return; // Did not collide with item object

        // Add to correct inventory based on item type
        Debug.Log($"Acquired: {itemObject.ReferenceItem.DisplayName} (active: {itemObject.ReferenceItem.IsActive})");
        if (itemObject.ReferenceItem.IsActive)
        {
            activeInventory.Add(itemObject.ReferenceItem);
        }
        else
        {
            passiveInventory.Add(itemObject.ReferenceItem);
        }
        itemObject.PickedUp();
    }
}
