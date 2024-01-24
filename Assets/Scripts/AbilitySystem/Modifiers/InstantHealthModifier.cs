using UnityEngine;
using UnityEngine.Assertions;

public interface IInstantHealthReceiver
{
    public void InstantHeal(int amount);
}

[CreateAssetMenuAttribute(menuName = "Modifier/Instant Healing Modifier", fileName = "Instant Healing Modifier")]
public class InstantHealthModifier : Modifier
{
    [SerializeField]
    private int healing = 5;

    public override void Apply(UseModifierContext context)
    {
        Assert.IsNotNull(context.instantHealtReceiver, "Tried applying instant health modifier, but no receiver was passed!");
        context.instantHealtReceiver.InstantHeal(healing);
    }

    public override void Unapply(UseModifierContext context)
    {
        Assert.IsNotNull(context.instantHealtReceiver, "Tried un-applying instant health modifier, but no receiver was passed!");
        context.instantHealtReceiver.InstantHeal(-healing);
    }
}
