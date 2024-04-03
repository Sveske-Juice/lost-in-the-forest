using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentHazard : MonoBehaviour
{
    BoxCollider2D coll;
    public GameObject steamCloud;

    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        Destroy(Instantiate(steamCloud, transform.position, Quaternion.identity), 10f);
    }
}
