using UnityEngine;

public class PickUpItemController : MonoBehaviour
{
    [SerializeField] InventorySystem passiveInventory;
    [SerializeField] InventorySystem activeInventory;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        TryPickItem(collider);
        TryPickCoin(collider);
    }

    private void TryPickItem(Collider2D collider)
    {
        ItemObject itemObject = collider.gameObject.GetComponent<ItemObject>();
        if (itemObject == null) return; // Did not collide with item object

        // Add to correct inventory based on item type
        bool canPickup;
        if (itemObject.ReferenceItem.IsActive)
        {
            canPickup = activeInventory.Add(itemObject.ReferenceItem);
        }
        else
        {
            canPickup = passiveInventory.Add(itemObject.ReferenceItem);
        }

        if (canPickup)
        {
            Debug.Log($"Acquired: {itemObject.ReferenceItem.DisplayName} (active: {itemObject.ReferenceItem.IsActive})");
            itemObject.PickedUp();
        }
    }

    private void TryPickCoin(Collider2D collider)
    {
        CreditObject creditObject = collider.gameObject.GetComponent<CreditObject>();
        if (creditObject == null) return; // Did not collide with a coin

        // Register coin to credit manager
        CreditManager.Instance.AddCoin(creditObject.Quantity);

        creditObject.PickedUp();
    }
}
