using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float jumpHeight = 5f;

    Vector2 movementVector;
    Rigidbody2D rb;
    CapsuleCollider2D capsuleCollider;
    public BoxCollider2D boxColl;
    public SpriteRenderer spriteRend;
    public float timer_close_door = 0;
    private float waitTime = 1.0f;
    private float timer = 0.0f;
    bool timeStart;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    
    void Update()
    {
        Vector2 playerVelocity = new Vector2(movementVector.x * movementSpeed, rb.velocity.y);
        rb.velocity = playerVelocity;
        if (capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Door")))
        {
            timeStart = true;
            boxColl.enabled = false;
            spriteRend.enabled = false;     
        }

        if (timeStart)
        {
            timer += Time.deltaTime;
        }

        if (timer > waitTime && !capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Door")))
        {
            boxColl.enabled = true;
            spriteRend.enabled = true; 
            timer = timer - waitTime;
            timeStart = false;
        }
    }


    private void OnMove(InputValue value)
    {
        movementVector = value.Get<Vector2>();
        Debug.Log(movementVector);
    }

    private void OnJump(InputValue value)
    {
        if (!capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;
        if (value.isPressed)
        {
            rb.velocity += new Vector2(0f, jumpHeight);
        }
    }
}
