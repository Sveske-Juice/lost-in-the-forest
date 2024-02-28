using UnityEngine;
using UnityEngine.Assertions;

public interface IRegenerationReceiver
{
    public void Regeneration(int regenAmount, float regenRate, int regenDuration);
}

[CreateAssetMenuAttribute(
    menuName = "Modifier/Regeneration Modifier",
    fileName = "regeneration modfier"
)]
public class RegenerationModifier : Modifier
{
    [SerializeField]
    private AnimationCurve regenAmount;

    [SerializeField]
    private AnimationCurve regenRate;

    [SerializeField]
    private AnimationCurve regenDuration;

    public override void Apply(UseModifierContext context)
    {
        Assert.IsNotNull(context.regenerationReceiver, "Tried applying regeneration health modifier, but no receiver was passed!");
        context
            .regenerationReceiver
            .Regeneration(
                regenAmount: (int)regenAmount.Evaluate(context.item.Level),
                regenRate: regenRate.Evaluate(context.item.Level),
                regenDuration: (int)regenDuration.Evaluate(context.item.Level)
            );
    }

    public override void Unapply(UseModifierContext context)
    {
        // Der er ikke brug for at unapply fordi at regeneration er stopper efter regenDuration er færdig
    }
}
