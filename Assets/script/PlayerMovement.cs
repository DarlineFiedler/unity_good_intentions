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

    BoxCollider2D myBoxCollider;

    Sprite sprite;

    Animator myAnimator;

    bool isJumping;

    GameObject myCollision;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        myBoxCollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        Run();
        FlipSprite();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
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
            PlayerPrefs
                .SetFloat("playerFacing",
                Mathf.Sign(myRigidbody.velocity.x) == 1 ? 0.2f : -0.2f);
            float turnPlayer =
                Mathf.Sign(myRigidbody.velocity.x) == 1 ? 0.2f : -0.2f;
            transform.localScale = new Vector2(turnPlayer, 0.2f);
            Debug.Log(PlayerPrefs.GetFloat("playerFacing"));
        }
    }

    void OnInteract(InputValue value)
    {
        if (myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Interactables")))
        {
            if (myCollision.tag == "Entrance")
            {
                myCollision.GetComponent<LevelEntrance>().Interacting();
            }
            if (myCollision.tag == "Mom")
            {
                myCollision.GetComponent<MomBehavior>().Interacting();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        myCollision = other.gameObject;
    }
}
