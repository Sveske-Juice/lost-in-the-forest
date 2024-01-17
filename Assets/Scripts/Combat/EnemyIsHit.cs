using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Denne kode vil nok blive implementeret i enemysnes AI
// -Morgan

public class EnemyIsHit : MonoBehaviour
{
    public EnemyStats enemy;
    private bool isHit = false;

    private float iFrame;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isHit)
        {
            iFrame -= Time.deltaTime;
            Debug.Log(iFrame);
        }
        if (iFrame <= 0)
        {
            isHit = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isHit && other.tag == "PlayerAttack") 
        {
            isHit = true;
            enemy.TakeDamage(1);
            Debug.Log("hit");
            iFrame = 1.0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ( other.tag == "PlayerAttack")
        {
            isHit = false;
            Debug.Log("Done");
        }
    }
}
