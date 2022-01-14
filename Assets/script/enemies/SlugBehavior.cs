using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlugBehavior : MonoBehaviour
{
    Rigidbody2D myRigidbody;

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

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckGroundAndWall();
    }

    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        myRigidbody.velocity = transform.right * speed;
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
