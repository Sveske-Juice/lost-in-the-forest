using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] ItemScriptableObject referenceItem;

    public ItemScriptableObject ReferenceItem => referenceItem;
}
