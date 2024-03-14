using System;
using UnityEngine;

public class CurveFollower : MonoBehaviour
{
    [SerializeField] private AnimationCurve altitudeToScale;

    private bool simulate = false;
    private Rigidbody2D body;
    private Vector2 v0;
    private float throwAngle;
    private float gravity;
    private IDamageable initiator;

    private float initialScale;
    private float t = 0f;
    private float z = 0f;

    public event Action<GameObject, Vector2> OnGroundImpact;
    public event Action<IDamageable, GameObject, Vector2> OnGroundImpactWithInitiator;

    public void Init(Rigidbody2D body, Vector2 initialVelocity, float throwAngle, float gravity, IDamageable initiator = null)
    {
        this.simulate = true;

        this.body = body;
        this.v0 = initialVelocity;
        this.throwAngle = throwAngle;
        this.gravity = gravity;
        this.initiator = initiator;

        this.initialScale = transform.localScale.x;
    }

    private void Update()
    {
        if (!this.simulate) return;
        t += Time.deltaTime;

        z = 0.5f * -gravity * t*t + v0.magnitude * t;

        body.velocity = new Vector2(v0.x, v0.y);
        transform.localScale = Vector3.one * initialScale * altitudeToScale.Evaluate(z);

        if (z <= 0)
        {
            body.velocity = Vector2.zero;
            this.simulate = false;

            OnGroundImpact?.Invoke(this.gameObject, transform.position);
            OnGroundImpactWithInitiator?.Invoke(this.initiator, this.gameObject, transform.position);
        }
    }

    public static float ThrowTime(float v0, float angle, float gravity)
    {
        return (2 * v0 * Mathf.Sin(angle))/(gravity);
    }

    public static float ThrowDistance(float v0, float angle, float gravity)
    {
        return (v0 * v0 * Mathf.Sin(2*angle))/(gravity);
    }
}
