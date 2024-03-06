using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OousagiIntro : ScriptedEvent
{
    [SerializeField] OousagiSpawner spawner;
    // Start is called before the first frame update
    void Start()
    {
        triggerField = GetComponent<BoxCollider2D>();
    }

     
    protected override IEnumerator PlayEvent()
    {
        yield return IntroAnimation(); //Runs Coroutine and waits for it to finish
        spawner.StartSpawn();
    }

}
