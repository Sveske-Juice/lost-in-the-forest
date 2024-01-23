using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Denne s�rger for at attacket kun er p� skermen i 1 sekundt
// �ndre AttackUpTime for at �ndre hvor langt tid den er p� sk�rmen
// -Morgan

public class RangedAttack : MonoBehaviour
{
    private float AttackUpTime;
    public string Name;

    private Transform transform;

    private bool hasHit;
    // Start is called before the first frame update
    void Start()
    {
        AttackUpTime = 0.25f;
        transform = GetComponent<Transform>();
        hasHit = false;
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
            MeleeAttack();
        }
    }

    void MeleeAttack()
    {
        Collider2D collider = Physics2D.OverlapCapsule(transform.position, transform.lossyScale, CapsuleDirection2D.Horizontal, transform.rotation.y);
        if (collider == null) return;
        IDamageable damageable = collider.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(1);
            hasHit = true;
            Debug.Log(damageable.health);
        }
    }
}
