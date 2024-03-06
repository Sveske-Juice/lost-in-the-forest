using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OousagiIntro : ScriptedEvent
{
    // Start is called before the first frame update
    void Start()
    {
        triggerField = GetComponent<BoxCollider2D>();
    }

     
    protected override void PlayEvent()
    {
        StartCoroutine(IntroAnimation());
    }

}
