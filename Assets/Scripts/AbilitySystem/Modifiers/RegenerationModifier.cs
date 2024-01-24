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
    private int regenAmount = 5;

    [SerializeField]
    private float regenRate = 5;

    [SerializeField]
    private int regenDuration = 5;

    public override void Apply(UseModifierContext context)
    {
        Assert.IsNotNull(context.regenerationReceiver, "Tried applying regeneration health modifier, but no receiver was passed!");
        context
            .regenerationReceiver
            .Regeneration(
                regenAmount: regenAmount,
                regenRate: regenRate,
                regenDuration: regenDuration
            );
    }

    public override void Unapply(UseModifierContext context)
    {
        // Der er ikke brug for at unapply fordi at regeneration er stopper efter regenDuration er færdig
    }
}
