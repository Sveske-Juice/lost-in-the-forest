using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private float delayBetweenBulletSequences = 0.1f;

    public void RunBulletSequence(AttackContext attackCtx, IEnumerable<BulletPattern> bulletSequence)
    {
        StartCoroutine(RunBulletSequenceCourotine(attackCtx, bulletSequence));
    }

    private IEnumerator RunBulletSequenceCourotine(AttackContext attackCtx, IEnumerable<BulletPattern> bulletSequence)
    {
        Vector3 facingDir = attackCtx.attackDir;
        facingDir.z = 0;
        facingDir.Normalize();

        foreach (BulletPattern bulletPattern in bulletSequence)
        {
            float stepSize = bulletPattern.Arc / ((float)bulletPattern.Amount);
            for (int i = 0; i < bulletPattern.Amount; i++)
            {
                Vector2 bulletDir = rotate(facingDir, stepSize * i + stepSize / 2f - bulletPattern.Arc / 2f);

                GameObject projectile = Instantiate(bulletPattern.bulletPrefab, attackCtx.origin.position, Quaternion.identity);
                Debug.DrawRay(attackCtx.origin.position, bulletDir, Color.magenta, 5f);

                projectile.transform.up = bulletDir;
                projectile.GetComponent<Rigidbody2D>().AddForce(bulletDir * bulletPattern.Speed);
                RangedAttack rangeAttack = projectile.GetComponent<RangedAttack>();
                rangeAttack.attackCtx = attackCtx;

                if (bulletPattern.Burst)
                    yield return new WaitForSeconds(bulletPattern.BurstDelay);
            }
            yield return new WaitForSeconds(delayBetweenBulletSequences);
        }
    }

    public static Vector2 rotate(Vector2 v, float deg)
    {
        float delta = deg * (float)Mathf.Deg2Rad;
        return new Vector2(
            v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
            v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
        );
    }
}
