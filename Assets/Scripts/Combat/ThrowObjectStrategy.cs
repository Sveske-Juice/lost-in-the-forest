using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/Throw Object Strategy", fileName = "throwobjectstrat")]
public class ThrowObjectStrategy : AttackStrategy
{
    [SerializeField] private bool matchThrowDistToTarget = true;

    [SerializeField] private float throwForce = 1f;
    [SerializeField] private float throwAngle = 45f;
    [SerializeField] private float gravity = 9f;
    [SerializeField] private float explosionRadius = 2f;
    [SerializeField] private float damage = 5f;

    [SerializeField] private GameObject impactPrefab;

    [SerializeField] private GameObject throwObjectPrefab;

    public float ThrowForce => throwForce;
    public float ThrowAngle => throwAngle;
    public float Gravity => gravity;

    public override void Attack(AttackContext _context)
    {
        GameObject throwObj = Instantiate(throwObjectPrefab);
        throwObj.transform.position = _context.origin.position;

        Rigidbody2D throwRB = throwObj.GetComponent<Rigidbody2D>();
        if (throwRB == null)
            Debug.LogWarning($"{throwObjectPrefab.name} does not have a rigidbody!");

        Vector2 throwVecForce = new Vector2(_context.attackDir.x, _context.attackDir.y).normalized * throwForce;


        CurveFollower curveFollower = throwObj.GetComponent<CurveFollower>();

        if (matchThrowDistToTarget)
        {
            float throwDist = Vector2.Distance(_context.origin.position, _context.target);
            Debug.DrawLine(_context.origin.position, _context.target, Color.red, 5f);
            float v0Force = CurveFollower.FindInitialVelocityFromThrowDist(throwDist, throwAngle * Mathf.Deg2Rad, gravity);
            throwVecForce = throwVecForce.normalized * v0Force;
        }

        // Match velocity to initator's if it can find it
        Rigidbody2D initiatorBody = _context.origin.gameObject.GetComponent<Rigidbody2D>();
        if (initiatorBody != null)
            throwVecForce = throwVecForce + initiatorBody.velocity;

        curveFollower.Init(throwRB, throwVecForce, throwAngle * Mathf.Deg2Rad, gravity, _context.initiator);

        curveFollower.OnGroundImpactWithInitiator += OnImpact;
    }

    private void OnImpact(IDamageable initator, GameObject throwObj, Vector2 impactPosition)
    {
        Destroy(throwObj);

        if (impactPrefab != null)
        {
            var impGo = Instantiate(impactPrefab);
            impGo.transform.position = impactPosition;
            HandleExplosionDamage(initator, impactPosition);
        }
    }

    private void HandleExplosionDamage(IDamageable initator, Vector2 explosionPosition)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPosition, explosionRadius);
        foreach (var collider in colliders)
        {
            IDamageable damagable = collider.GetComponent<IDamageable>();
            if (damagable == null) continue;

            damagable.TakeDamage(damage, initator);
        }
    }
}
