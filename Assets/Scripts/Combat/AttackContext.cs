using UnityEngine;

public class AttackContext
{
    public Transform origin { get; private set; }
    public CombatPlayer player { get; private set; }
    public EnemyAI enemyAI { get; private set; }

    public AttackContext(Transform _origin, CombatPlayer player, EnemyAI enemyAI)
    {
        this.origin = _origin;
        this.player = player;
        this.enemyAI = enemyAI;
    }

}

public class AttackContextBuilder
{
    private Transform origin;
    private CombatPlayer player;
    private EnemyAI enemyAI;

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
    public AttackContextBuilder WithEnemy(EnemyAI enemy)
    {
        this.enemyAI = enemy;
        return this;
    }

    public AttackContext Build()
    {
        return new AttackContext(origin, player, enemyAI);
    }
}
