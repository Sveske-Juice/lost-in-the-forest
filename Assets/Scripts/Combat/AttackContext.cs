using UnityEngine;

public class AttackContext
{
    public Transform origin { get; private set; }
    public CombatPlayer player { get; private set; }

    public AttackContext(Transform _origin, CombatPlayer player)
    {
        this.origin = _origin;
        this.player = player;
    }
}

public class AttackContextBuilder
{
    private Transform origin;
    private CombatPlayer player;

    public AttackContextBuilder WithOrigin(Transform origin)
    {
        this.origin = origin;
        return this;
    }

    public AttackContextBuilder WithPlayer(CombatPlayer player)
    {
        this.player = player;
        return this;
    }

    public AttackContext Build()
    {
        return new AttackContext(origin, player);
    }
}
