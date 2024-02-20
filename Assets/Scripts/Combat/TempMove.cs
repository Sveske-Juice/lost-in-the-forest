using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Af Morgan
//Dette er en midlertidig movement script da det er næmmere at teste combat når man kan bevæge sig
//Dette script skal blive fjernet senere når der kommer et bedre movement script


public class TempMove : MonoBehaviour
{
    Rigidbody2D body;
    Transform transform;

    float horizontal;
    float vertical;

    public float runSpeed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down
    }

    private void FixedUpdate()
    {
        Vector2 velocity = new Vector2(horizontal, vertical);
        velocity.Normalize();
        body.velocity = velocity * runSpeed;

    }
}
