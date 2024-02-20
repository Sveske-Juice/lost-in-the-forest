using UnityEngine;

[CreateAssetMenu(fileName = "ItemScriptableObject", menuName = "ScriptableObjects/Item")]
public class ItemScriptableObject : ScriptableObject
{
    [SerializeField] string id;
    [SerializeField] string displayName;
    [SerializeField] ItemStrategy[] itemStrategies;
    [SerializeField] int uses = 1;
    [SerializeField] Texture2D icon;
    [SerializeField] Modifier[] passiveModifiers;

    public string Id => id;
    public string DisplayName => displayName;
    public Texture2D Icon => icon;
    public bool IsActive => itemStrategies != null;
    public bool IsPassive => !IsActive;

    public bool UseAbility(UseModifierContext useItemContext)
    {
        if (uses > 0)
        {
            for (int i = 0; i < itemStrategies.Length; i++)
            {
                itemStrategies[i].UseItem(useItemContext);
            }
            uses--;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Called when this item is acquired into an inventory
    /// </summary>
    /// <param name="context"> The context for the person/enemy who acquired the item </param>
    public void ItemAcquired(UseModifierContext context)
    {
        Debug.Log($"Acquired: {displayName}");
        ApplyModifiers(context);
    }

    private void ApplyModifiers(UseModifierContext context)
    {
        foreach (var modifier in passiveModifiers)
        {
            Debug.Log($"Applying modifier: {modifier.ToString()}");
            modifier.Apply(context);
        }
    }

    private void UnapplyModifiers(UseModifierContext context)
    {
        foreach (var modifier in passiveModifiers)
        {
            modifier.Unapply(context);
        }
    }

    /// <summary>
    /// Called when the person/enemy loses this item from their inventory
    /// </summary>
    /// <param name="context"> The context for the person/enemy who lost the item </param>
    public void LoseItem(UseModifierContext context)
    {
        UnapplyModifiers(context);
    }
}
