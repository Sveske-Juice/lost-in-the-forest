using UnityEngine;
using UnityEngine.Assertions;

public interface IAttackSpeedReceiver
{
    public void AddAttackSpeed(float amount);
}

[CreateAssetMenuAttribute(menuName = "Modifier/Attack Speed Modifer", fileName = "Attack Speed Modifier")]
public class AttackSpeedModifier : Modifier
{
    [SerializeField]
    private AnimationCurve speedAmount;

    public override void Apply(UseModifierContext context)
    {
        Assert.IsNotNull(context?.attackSpeedReceiver, "No attack speed receiver when trying to apply attack speed modifier");
        context.attackSpeedReceiver.AddAttackSpeed(speedAmount.Evaluate(context.item.Level));
    }

    public override void Unapply(UseModifierContext context)
    {
        Assert.IsNotNull(context?.attackSpeedReceiver, "No attack speed receiver when trying to un-apply attack speed modifier");
        context.attackSpeedReceiver.AddAttackSpeed(-speedAmount.Evaluate(context.item.Level));
    }
}
