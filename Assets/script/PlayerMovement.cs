using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float runSpeed = 10f;

    [SerializeField]
    float jumpSpeed = 5f;

    Vector2 moveInput;

    Rigidbody2D myRigidbody;

    CapsuleCollider2D myCapsuleCollider;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        Run();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if (value.isPressed)
        {
            myRigidbody.velocity +=
                new Vector2(myRigidbody.velocity.x, jumpSpeed);
        }
    }

    void Run()
    {
        Vector2 placerVelocity =
            new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = placerVelocity;
    }
}
