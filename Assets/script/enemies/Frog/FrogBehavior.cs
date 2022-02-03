using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogBehavior : MonoBehaviour
{
    Rigidbody2D myRigidbody;

    Animator myAnimator;

    float speed = 10f;

    [SerializeField]
    float speedAfter = 12f;

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

    float health = 2f;

    float playerDamage = 1f;

    BoxCollider2D frogCollider;

    void Start()
    {
        waitCountdown = timewaiting;
        runCountdown = timeRunning;
        speedy = speed;
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        playerDamage = PlayerPrefs.GetFloat("damage");
        frogCollider = GetComponent<BoxCollider2D>();

        //myRigidbody.isKinematic = true;
        if (PlayerPrefs.GetInt("ForestSpiritIsDead") == 1)
        {
            myAnimator.SetBool("isAngry", true);
            speed = speedAfter;
            health = 4f;
        }
        else
        {
            myAnimator.SetBool("isAngry", false);
            speed = 10f;
            health = 2f;
        }
    }

    void Update()
    {
        CheckGroundAndWall();
        if (health > 0)
        {
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
                frogCollider.enabled = false;
            }
        }
    }
}
