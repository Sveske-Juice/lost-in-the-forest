using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentHazard : MonoBehaviour
{
    BoxCollider2D coll;
    public GameObject steamCloud;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(steamCloud, transform.position, Quaternion.identity);
    }
}
