public class UseModifierContext
{
    public IInstantHealthReceiver instantHealtReceiver { get; private set; }
    public IRegenerationReceiver regenerationReceiver { get; private set; }
    public IAttackSpeedReceiver attackSpeedReceiver { get; private set; }
    public IDamageReceiver damageReceiver { get; private set; }
    public IMoveSpeedReceiver moveSpeedReceiver { get; private set; }
    public IThornsReceiver thornsReceiver { get; private set; }

    public UseModifierContext(
        IInstantHealthReceiver instantHealtReceiver,
        IRegenerationReceiver regenerationReceiver,
        IAttackSpeedReceiver attackSpeedReceiver,
        IDamageReceiver damageReceiver,
        IMoveSpeedReceiver moveSpeedReceiver,
        IThornsReceiver thornsReceiver
    )
    {
        this.instantHealtReceiver = instantHealtReceiver;
        this.regenerationReceiver = regenerationReceiver;
        this.attackSpeedReceiver = attackSpeedReceiver;
        this.damageReceiver = damageReceiver;
        this.moveSpeedReceiver = moveSpeedReceiver;
        this.thornsReceiver = thornsReceiver;
    }

    public override string ToString()
    {
        return $"{instantHealtReceiver}, {regenerationReceiver}, {attackSpeedReceiver},{damageReceiver}, {moveSpeedReceiver}, {thornsReceiver}";
    }
}

public class UseModifierContextBuilder
{
    private IInstantHealthReceiver instantHealtReceiver;
    private IRegenerationReceiver regenerationReceiver;
    private IAttackSpeedReceiver attackSpeedReceiver;
    private IDamageReceiver damageReceiver;
    private IMoveSpeedReceiver moveSpeedReceiver;
    private IThornsReceiver thornsReceiver;

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
    public UseModifierContextBuilder WithDamageReceiver(
        IDamageReceiver damageReceiver
    )
    {
        this.damageReceiver = damageReceiver;
        return this;
    }
    public UseModifierContextBuilder WithMoveSpeedReceiver(
        IMoveSpeedReceiver moveSpeedReceiver
    )
    {
        this.moveSpeedReceiver = moveSpeedReceiver;
        return this;
    }

    public UseModifierContextBuilder WithThornsReceiver(
        IThornsReceiver thornsReceiver
    )
    {
        this.thornsReceiver = thornsReceiver;
        return this;
    }

    public UseModifierContext Build()
    {
        return new UseModifierContext(
            instantHealtReceiver,
            regenerationReceiver,
            attackSpeedReceiver,
            damageReceiver,
            moveSpeedReceiver,
            thornsReceiver
        );
    }
}
