using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocitySpriteFlipper : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    private Vector3 prevPos = Vector3.zero;

    private void Update()
    {
        Vector3 velocity = transform.position - prevPos;
        spriteRenderer.flipX = velocity.x >= 0f ? false : true;

        prevPos = transform.position;
    }
}