using UnityEngine;

public class CreditObject : MonoBehaviour
{
    [SerializeField, Range(1, 64)] int quantity = 1;

    public int Quantity => quantity;

    public void PickedUp()
    {
        // TODO: play animation?
        Destroy(gameObject);
    }
}
