using UnityEngine;


public class RangedAttack : MonoBehaviour
{
    // Denne sørger for at attacket kun er på skermen i 1 sekundt
    // ændre AttackUpTime for at ændre hvor langt tid den er på skærmen
    // -Morgan
    [SerializeField] private float AttackUpTime;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] ParticleSystem onHitParticleSystem = default; // Assign your particle system in the inspector

    void Update()
    {
        AttackUpTime -= Time.deltaTime;

        if (AttackUpTime <= 0.0f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Ignore player for now
        if (collider.gameObject.CompareTag("Player")) return;

        // Check if the collision is with a GameObject that implements IDamageable
        IDamageable damageable = collider.gameObject.GetComponent<IDamageable>();

        // No damagable object was hit
        if (damageable == null) return;

        // Assuming you want to access the CombatPlayer's magical attack value, you need to reference it directly
        // For example, let's say CombatPlayer has a public property or field called 'MagicalAttack'
        float damage = CombatPlayer.combatPlayer.GetMagicalDamage();
        damageable.TakeDamage(damage);

        Debug.Log($"Hit {collider.gameObject.name} with {damage} magical damage");

        StartParticleSystem(onHitParticleSystem, collider.transform.position, duration: 3f);
        Destroy(gameObject); // Destroy the projectile or attacker GameObject
    }

    void StartParticleSystem(ParticleSystem particleSystem, Vector3 possitionOfParticalEffect, float duration)
    {
        // Instantiate the particle system with the same rotation as the original
        ParticleSystem ps = Instantiate(particleSystem, possitionOfParticalEffect, particleSystem.transform.rotation) as ParticleSystem;
        ps.Play();
        Destroy(ps.gameObject, duration);
    }
}
