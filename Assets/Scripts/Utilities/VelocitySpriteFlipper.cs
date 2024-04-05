using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocitySpriteFlipper : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float moveThreshold = 1.0f;
    public bool invert = false;

    private Vector3 prevPos = Vector3.zero;

    private void Update()
    {
        Vector3 velocity = transform.position - prevPos;
        if (velocity.magnitude < moveThreshold) return;
        spriteRenderer.flipX = velocity.x >= 0f ? false : true;
        if (invert)
            spriteRenderer.flipX = !spriteRenderer.flipX;

        prevPos = transform.position;
    }
}
