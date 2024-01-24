public class UseModifierContext
{
    public IInstantHealthReceiver instantHealtReceiver { get; private set; }
    public IRegenerationReceiver regenerationReceiver { get; private set; }

    public UseModifierContext(
        IInstantHealthReceiver instantHealtReceiver,
        IRegenerationReceiver regenerationReceiver
    )
    {
        this.instantHealtReceiver = instantHealtReceiver;
        this.regenerationReceiver = regenerationReceiver;
    }
}

public class UseModifierContextBuilder
{
    private IInstantHealthReceiver instantHealtReceiver;
    private IRegenerationReceiver regenerationReceiver;

    public UseModifierContextBuilder WithInstantHealthReceiver(
        IInstantHealthReceiver instantHealtReceiver
    )
    {
        this.instantHealtReceiver = instantHealtReceiver;
        return this;
    }

    public UseModifierContextBuilder WithRegenerationHealthReceiver(IRegenerationReceiver regenerationReceiver)
    {
        this.regenerationReceiver = regenerationReceiver;
        return this;
    }

    public UseModifierContext Build()
    {
        return new UseModifierContext(instantHealtReceiver, regenerationReceiver);
    }
}
