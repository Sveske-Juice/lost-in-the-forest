using UnityEngine;

[CreateAssetMenu(fileName = "ItemScriptableObject", menuName = "ScriptableObjects/Item")]
public class ItemScriptableObject : ScriptableObject
{
    [SerializeField] string id;
    [SerializeField] string displayName;
    [SerializeField] ItemStrategy[] itemStrategies;
    [SerializeField] int uses = 1;
    [SerializeField] Texture2D icon;
    public bool IsActive => itemStrategies != null;

    public string Id => id;
    public string DisplayName => displayName;
    public Texture2D Icon => icon;

    public bool UseAbility(UseModifierContext useItemContext)
    {
        if (uses > 0)
        {
            for (int i = 0; i < itemStrategies.Length; i++)
            {
                itemStrategies[i].UseItem(useItemContext);
            }
            return true;
        }
        return false;
    }
}
