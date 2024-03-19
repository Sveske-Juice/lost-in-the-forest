using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class HappyOSIntro : ScriptedEvent
{
    public SplineAnimate splineAnim;
    public EnemyAttackAI ai;
    // Start is called before the first frame update
    void Awake()
    {
        ai.enabled = false;
        triggerField = GetComponent<BoxCollider2D>();
    }


    protected override IEnumerator PlayEvent()
    {
        splineAnim.Play();
        ai.enabled = true;
        yield return null;
    }
}
