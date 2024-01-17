using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Denne s�rger for at attacket kun er p� skermen i 1 sekundt
// �ndre AttackUpTime for at �ndre hvor langt tid den er p� sk�rmen
// -Morgan

public class MeleAttack : MonoBehaviour
{
    private float AttackUpTime;
    public string Name;
    // Start is called before the first frame update
    void Start()
    {
        AttackUpTime = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        AttackUpTime -= Time.deltaTime;

        if (AttackUpTime <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}
