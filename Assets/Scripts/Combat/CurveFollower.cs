using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class CurveFollower : MonoBehaviour
{
    public void Init(Rigidbody2D body, Vector3 dir, float maxDist, AnimationCurve velocityCurve)
    {
        StartCoroutine(ThrowObject(body, dir, velocityCurve, maxDist));
    }

    private IEnumerator ThrowObject(Rigidbody2D body, Vector3 dir, AnimationCurve throwCurve, float dist)
    {
        float distTraveled = 0f;
        float t = 0f;
        do
        {
            Vector3 vel = dir.normalized * throwCurve.Evaluate(t);
            body.velocity = vel;
            t += Time.deltaTime;
            distTraveled += vel.magnitude;

            yield return new WaitForEndOfFrame();
        } while (distTraveled < dist);
    }
}
