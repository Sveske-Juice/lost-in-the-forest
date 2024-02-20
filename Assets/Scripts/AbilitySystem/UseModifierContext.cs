public class UseModifierContext
{
    public IInstantHealthReceiver instantHealtReceiver { get; private set; }
    public IRegenerationReceiver regenerationReceiver { get; private set; }
    public IAttackSpeedReceiver attackSpeedReceiver { get; private set; }

    public UseModifierContext(
        IInstantHealthReceiver instantHealtReceiver,
        IRegenerationReceiver regenerationReceiver,
        IAttackSpeedReceiver attackSpeedReceiver
    )
    {
        this.instantHealtReceiver = instantHealtReceiver;
        this.regenerationReceiver = regenerationReceiver;
        this.attackSpeedReceiver = attackSpeedReceiver;
    }
}

public class UseModifierContextBuilder
{
    private IInstantHealthReceiver instantHealtReceiver;
    private IRegenerationReceiver regenerationReceiver;
    private IAttackSpeedReceiver attackSpeedReceiver;

    public UseModifierContextBuilder WithInstantHealthReceiver(
        IInstantHealthReceiver instantHealtReceiver
    )
    {
        this.instantHealtReceiver = instantHealtReceiver;
        return this;
    }

    public UseModifierContextBuilder WithRegenerationHealthReceiver(
        IRegenerationReceiver regenerationReceiver
    )
    {
        this.regenerationReceiver = regenerationReceiver;
        return this;
    }

    public UseModifierContextBuilder WithAttackSpeedReceiver(
        IAttackSpeedReceiver attackSpeedReceiver
    )
    {
        this.attackSpeedReceiver = attackSpeedReceiver;
        return this;
    }

    public UseModifierContext Build()
    {
        return new UseModifierContext(
            instantHealtReceiver,
            regenerationReceiver,
            attackSpeedReceiver
        );
    }
}
