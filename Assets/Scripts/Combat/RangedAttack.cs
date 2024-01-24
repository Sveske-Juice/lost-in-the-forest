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
    [SerializeField] public float projectileSpeed;

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

        if (!hasHit)
        {
            AttackHit();
        }
    }

    void AttackHit()
    {
        Collider2D collider = Physics2D.OverlapCapsule(transform.position, transform.localScale, CapsuleDirection2D.Horizontal, transform.rotation.y);
        if (collider == null) return;
        IDamageable damageable = collider.gameObject.GetComponent<IDamageable>();
        if (damageable != null && damageable != (CombatPlayer.combatPlayer as IDamageable))
        {
            damageable.TakeDamage(1); // �ndre dette s� den tage combatPlayerens magical attack
            hasHit = true;
            Debug.Log(damageable.Health);
            Destroy(gameObject);
        }
    }
}
