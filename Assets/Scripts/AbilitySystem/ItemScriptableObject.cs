using UnityEngine;
using UnityEngine.Assertions;

[CreateAssetMenu(fileName = "ItemScriptableObject", menuName = "ScriptableObjects/Item")]
public class ItemScriptableObject : ScriptableObject
{
    [SerializeField] ItemActivationMethod activationMethod = ItemActivationMethod.INSTANT;
    [SerializeField] string id;
    [SerializeField] string displayName;
    [SerializeField]
    [TextArea]
    string description;
    [SerializeField] ItemStrategy[] itemStrategies;
    [SerializeField] int useCount = 1;
    [SerializeField] Texture2D icon;
    [SerializeField] Modifier[] passiveModifiers;
    [SerializeField] int level = 1;
    [SerializeField] int maxLevel = 5;
    [SerializeField] int cost = 5;
    [SerializeField] GameObject pickupPrefab;
    [SerializeField] ItemScriptableObject[] conflictingItems;
    [SerializeField] GameObject targetSelectionPrefab;

    public ItemActivationMethod ActivationMethod => activationMethod;
    public int Level => level;
    public int MaxLevel => maxLevel;
    public string Id => id;
    public string DisplayName => displayName;
    public string Description => description;
    public Texture2D Icon => icon;
    public bool IsActive => itemStrategies != null && itemStrategies.Length > 0;
    public int Uses => uses;
    public bool IsPassive => !IsActive;
    public int Cost => cost;
    public ItemScriptableObject[] ConflictingItems => conflictingItems;
    public GameObject PickupPrefab => pickupPrefab;
    public GameObject TargetSelectionPrefab => targetSelectionPrefab;

    private int uses;

    private void OnEnable()
    {
        uses = useCount;
    }

    private void OnValidate()
    {
        Assert.IsFalse(IsPassive && activationMethod == ItemActivationMethod.TARGET_SELECTION,
            "Can not have passive items with target activation method");

        if (string.IsNullOrEmpty(id))
            Debug.LogWarning($"No id for set {this}!");
    }

    public void SetLevel(int _level)
    {
        this.level = _level;
        if (_level > maxLevel)
            this.level = maxLevel;
    }

    public bool UseAbility(UseModifierContext useItemContext)
    {
        Debug.Log($"{DisplayName} {uses}");
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
        ApplyModifiers(context);
    }

    /// <summary>
    /// Called when the person/enemy loses this item from their inventory
    /// </summary>
    /// <param name="context"> The context for the person/enemy who lost the item </param>
    public void LoseItem(UseModifierContext context)
    {
        UnapplyModifiers(context);
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
}
