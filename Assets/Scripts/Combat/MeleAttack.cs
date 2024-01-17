using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Denne sørger for at attacket kun er på skermen i 1 sekundt
// ændre AttackUpTime for at ændre hvor langt tid den er på skærmen
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
