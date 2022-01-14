using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogBehavior : MonoBehaviour
{
    Rigidbody2D myRigidbody;

    Animator myAnimator;

    [SerializeField]
    float speed = 0.2f;

    [SerializeField]
    bool groundDetected = true;

    [SerializeField]
    Transform groundPositionChecker;

    [SerializeField]
    float groundCheckDistance;

    [SerializeField]
    LayerMask whatIsGround;

    [SerializeField]
    bool wallDetected;

    [SerializeField]
    Transform wallPositionChecker;

    [SerializeField]
    float wallCheckDistance;

    [SerializeField]
    LayerMask whatIsWall;

    bool hasTurn;

    float ZAxisAdd;

    [SerializeField]
    float timewaiting = 0.19f;

    [SerializeField]
    float timeRunning = 0.26f;

    [SerializeField]
    float waitCountdown;

    [SerializeField]
    float runCountdown;

    float speedy;

    void Start()
    {
        waitCountdown = timewaiting;
        runCountdown = timeRunning;
        speedy = speed;
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myRigidbody.isKinematic = true;
    }

    void Update()
    {
        CheckGroundAndWall();

        if (waitCountdown <= 0)
        {
            if (runCountdown >= 0)
            {
                myAnimator.SetBool("isJumping", true);
                myRigidbody.velocity = transform.right * speed;
            }

            runCountdown -= Time.deltaTime;

            if (runCountdown <= 0)
            {
                speedy = 0;
                waitCountdown = timewaiting;
                runCountdown = timeRunning;
            }
        }
        else
        {
            myAnimator.SetBool("isJumping", false);
            waitCountdown -= Time.deltaTime;
            myRigidbody.velocity = Vector2.zero;
        }
    }

    void CheckGroundAndWall()
    {
        groundDetected =
            Physics2D
                .Raycast(groundPositionChecker.position,
                -transform.up,
                groundCheckDistance,
                whatIsGround);

        wallDetected =
            Physics2D
                .Raycast(wallPositionChecker.position,
                transform.right,
                wallCheckDistance,
                whatIsWall);

        if (!groundDetected || wallDetected)
        {
            if (!hasTurn)
            {
                ZAxisAdd -= 180;
                transform.eulerAngles = new Vector3(0, ZAxisAdd, 0);
                hasTurn = true;
            }
        }
        if (groundDetected || !wallDetected)
        {
            hasTurn = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos
            .DrawLine(groundPositionChecker.position,
            new Vector2(groundPositionChecker.position.x,
                groundPositionChecker.position.y - groundCheckDistance));
        Gizmos
            .DrawLine(wallPositionChecker.position,
            new Vector2(wallPositionChecker.position.x + wallCheckDistance,
                wallPositionChecker.position.y));
    }
}
