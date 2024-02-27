using UnityEngine;
using UnityEngine.Events;

public class ItemObject : MonoBehaviour
{
    // TODO: implement this
    // [SerializeField, Range(1, 64)] int quantity = 1;
    [SerializeField] ItemScriptableObject referenceItem;

    public ItemScriptableObject ReferenceItem => referenceItem;

    public UnityEvent OnPickup;

    public void PickedUp()
    {
        OnPickup?.Invoke();
    }
}
