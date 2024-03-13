using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptedEvent : MonoBehaviour
{
    protected BoxCollider2D triggerField;
    [SerializeField] protected Animation introAnimation;
    protected abstract IEnumerator PlayEvent();

    protected virtual IEnumerator IntroAnimation()
    {
        //introAnimation.Play();
        yield return new WaitForSeconds(/*introAnimation.clip.length*/ 2);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print("event");
            StartCoroutine(PlayEvent());
        }
    }
}
