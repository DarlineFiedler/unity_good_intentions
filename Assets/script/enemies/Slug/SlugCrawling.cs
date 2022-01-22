using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlugCrawling : MonoBehaviour
{
    Rigidbody2D rb2d;

    [SerializeField]
    float speed = 0.2f;

    [SerializeField]
    bool groundDetected;

    [SerializeField]
    Transform groundPositionChecker;

    [SerializeField]
    float groundCheckDistance;

    [SerializeField]
    LayerMask whatIsGround;

    bool hasTurn;

    float ZAxisAdd;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckGround();
    }

    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        rb2d.velocity = transform.right * speed;
    }

    void CheckGround()
    {
        groundDetected =
            Physics2D
                .Raycast(groundPositionChecker.position,
                -transform.up,
                groundCheckDistance,
                whatIsGround);

        if (!groundDetected)
        {
            if (!hasTurn)
            {
                ZAxisAdd -= 90;
                transform.eulerAngles = new Vector3(0, 0, ZAxisAdd);
                hasTurn = true;
            }
        }
        if (groundDetected)
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
    }
}
