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

    float health = 1f;

    float playerDamage = 1f;

    Animator myAnimator;

    BoxCollider2D collider2D;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        playerDamage = PlayerPrefs.GetFloat("damage");
        myAnimator = GetComponent<Animator>();
        collider2D = GetComponent<BoxCollider2D>();
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
        if (health > 0)
        {
            myRigidbody.velocity = transform.right * speed;
        }
        else
        {
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            health -= playerDamage;
            myAnimator.SetTrigger("getHurt");
            if (health <= 0)
            {
                myAnimator.SetBool("isDead", true);
                collider2D.enabled = false;
            }
        }
    }
}
