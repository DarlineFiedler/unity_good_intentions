using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float runSpeed = 10f;

    [SerializeField]
    float jumpSpeed = 5f;

    Vector2 moveInput;

    Rigidbody2D myRigidbody;

    // PolygonCollider2D polygonCollider;
    // PolygonCollider2D myPolygonCollider;
    CapsuleCollider2D myCapsuleCollider;

    Sprite sprite;

    Animator myAnimator;

    bool isJumping;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        //myPolygonCollider = GetComponent<PolygonCollider2D>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        Run();
        FlipSprite();

        //polygonCollider = GetComponent<PolygonCollider2D>();
        //sprite = GetComponent<SpriteRenderer>().sprite;
        //polygonCollider.pathCount = sprite.GetPhysicsShapeCount();

        /* List<Vector2> path = new List<Vector2>();
        for (int i = 0; i < polygonCollider.pathCount; i++)
        {
            path.Clear();
            sprite.GetPhysicsShape (i, path);
            polygonCollider.SetPath(i, path.ToArray());
        } */
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
            myAnimator.SetTrigger("isJumping");
            myRigidbody.velocity +=
                new Vector2(myRigidbody.velocity.x, jumpSpeed);
        }
    }

    void Run()
    {
        Vector2 placerVelocity =
            new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = placerVelocity;

        bool playerHasHorizontalSpeed =
            Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed =
            Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            float turnPlayer =
                Mathf.Sign(myRigidbody.velocity.x) == 1 ? 0.25f : -0.25f;
            transform.localScale = new Vector2(turnPlayer, 0.25f);
        }
    }
}
