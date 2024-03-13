using UnityEngine;

public class AttackContext
{
    public Transform origin { get; private set; }
    public Vector3 attackDir { get; private set; }
    public IDamageable initiator { get; private set; }
    public float physicalDamage { get; private set; }
    public float magicalDamage { get; private set; }
    public Vector3 target {  get; private set; }

    public AttackContext(Transform _origin, Vector3 _attackDir, IDamageable _initiator, float physicalDamage, float magicalDamage, Vector3 target)
    {
        this.origin = _origin;
        this.attackDir = _attackDir;
        this.initiator = _initiator;
        this.physicalDamage = physicalDamage;
        this.magicalDamage = magicalDamage;
        this.target = target;
    }

}

public class AttackContextBuilder
{
    private Transform origin;
    private Vector3 attackDir;
    private IDamageable initiator;
    private float physicalDamage, magicalDamage;
    private Vector3 target;

    public AttackContextBuilder WithOrigin(Transform origin)
    {
        this.origin = origin;
        return this;
    }

    public AttackContextBuilder WithAttackDir(Vector3 attackDir)
    {
        this.attackDir = attackDir;
        return this;
    }
    public AttackContextBuilder WithInitiator(IDamageable initiator)
    {
        this.initiator = initiator;
        return this;
    }

    public AttackContextBuilder WithPhysicalDamge(float physicalDamage)
    {
        this.physicalDamage = physicalDamage;
        return this;
    }

    public AttackContextBuilder WithMagicalDamage(float magicalDamage)
    {
        this.magicalDamage = magicalDamage;
        return this;
    }

    public AttackContextBuilder WithTarget(Vector3 target)
    {
        this.target = target;
        return this;
    }
    public AttackContext Build()
    {
        return new AttackContext(origin, attackDir, initiator, physicalDamage, magicalDamage, target);
    }
}
