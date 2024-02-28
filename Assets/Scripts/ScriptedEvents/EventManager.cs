using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager eventManager;



    private void Awake()
    {
        if (eventManager == null)
            eventManager = this;

        DontDestroyOnLoad(this);
    }

    void Update()
    {
        
    }

    /*public void PlayEvent(ScriptedEvent event)
    {
        eve
    }*/
}
