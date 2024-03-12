using UnityEngine;
using UnityEngine.Assertions;

public interface IAttackStrategyReceiver
{
    public void HotReloadRightHandStrategy(AttackStrategy strategy);
    public void HotReloadLeftHandStrategy(AttackStrategy strategy);
}

[CreateAssetMenuAttribute(menuName = "Modifier/Attack Strategy Modifer", fileName = "Attack Strategy Modifier")]
public class AttackStrategyModifier : Modifier
{
    [SerializeField]
    private AttackStrategy rightHandStrategy, leftHandStrategy;

    public override void Apply(UseModifierContext context)
    {
        Assert.IsNotNull(context?.attackStrategyReciever, "No attack strategy receiver when trying to apply attack strat modifier");

        if (rightHandStrategy != null)
            context.attackStrategyReciever.HotReloadRightHandStrategy(rightHandStrategy);
        if (leftHandStrategy != null)
            context.attackStrategyReciever.HotReloadLeftHandStrategy(leftHandStrategy);
    }

    public override void Unapply(UseModifierContext context)
    {
        Assert.IsNotNull(context?.attackStrategyReciever, "No attack attack receiver when trying to un-apply attack strat modifier");

        if (rightHandStrategy != null)
            context.attackStrategyReciever.HotReloadRightHandStrategy(null);
        if (leftHandStrategy != null)
            context.attackStrategyReciever.HotReloadLeftHandStrategy(null);
    }
}
