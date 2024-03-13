using UnityEngine;

// Dette vil v�re klassen som holder enemystats n�r spillet k�re
// Scriptet som Gabriel har lavet vil holde de enemysne som ikke er blevet intansiatet i nu
// -Morgan

[RequireComponent(typeof(HealthComponent))]
public class CombatEnemy : MonoBehaviour, IDamageable, IThornsReceiver
{
    public Transform Transform => transform;

    public int Health => (int) this.healthComponent.CurrentHealth;

    [Header("Modifier values")]
    [SerializeField]
    private float thornsScale = 0;

    private HealthComponent healthComponent;

    private void Awake()
    {
        healthComponent = GetComponent<HealthComponent>();
    }

    public void TakeDamage(float _damage, IDamageable initiator)
    {
        this.healthComponent.Modify(_damage);

        // reason for initiator is null is to avoid cyclic thorns being applied
        if (initiator != null)
            initiator.TakeDamage(_damage * thornsScale, initiator: null);
    }

    public bool Heal(int _heal)
    {
        this.healthComponent.Modify(-_heal);

        return true; //temp
    }

    public void ThornsIncrease(float thornsScale)
    {
        this.thornsScale += thornsScale;
    }
}
