using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

// Denne s�rger for at attacket kun er p� skermen i 1 sekundt
// �ndre AttackUpTime for at �ndre hvor langt tid den er p� sk�rmen
// -Morgan

public class RangedAttack : MonoBehaviour
{
    [SerializeField] private float AttackUpTime;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] ParticleSystem particleSystem = default; // Assign your particle system in the inspector
    private bool hasHit;

    // Start is called before the first frame update
    void Start()
    {
        hasHit = false;
        //rb.velocity = transform.forward * projectileSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        AttackUpTime -= Time.deltaTime;

        if (AttackUpTime <= 0.0f)
        {
            Destroy(gameObject);
        }

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        // Check if the collision is with a GameObject that implements IDamageable
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        Debug.Log(damageable + " damageable");
    
        // Check if damageable is not null and it's not the CombatPlayer
        if (damageable != null && damageable != (CombatPlayer.combatPlayer as IDamageable))
        {
            // Assuming you want to access the CombatPlayer's magical attack value, you need to reference it directly
            // For example, let's say CombatPlayer has a public property or field called 'MagicalAttack'

            damageable.TakeDamage(1);
            Debug.Log(damageable.Health);
            Particle(particleSystem, collision.transform.position);
            Destroy(gameObject); // Destroy the projectile or attacker GameObject
        }
    }
    void Particle(ParticleSystem particleSystem,Vector3 possitionOfParticalEffect)
    {
        // Instantiate the particle system with the same rotation as the original
        ParticleSystem ps = Instantiate(particleSystem, possitionOfParticalEffect, particleSystem.transform.rotation) as ParticleSystem;
        ps.Play();
        Destroy(ps.gameObject, 2); // Destroy after 3 seconds
    }

}
