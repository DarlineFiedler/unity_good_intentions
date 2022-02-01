using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class SquidBehavior : MonoBehaviour
{
    public Transform target;

    float speed = 100f;

    public float nextWaypointDistance = 3f;

    Path path;

    int currentWaypoint = 0;

    // bool reachedEndOfPath = false;
    Seeker seeker;

    Rigidbody2D myRigidbody;

    float health = 2f;

    float playerDamage = 1f;

    CapsuleCollider2D squidCollider;

    Animator myAnimator;

    float waitCountdown = 3f;

    bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        playerDamage = PlayerPrefs.GetFloat("damage");
        squidCollider = GetComponent<CapsuleCollider2D>();
        seeker = GetComponent<Seeker>();
        myRigidbody = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);

        if (PlayerPrefs.GetInt("ForestSpiritIsDead") == 1)
        {
            myAnimator.SetBool("isAngry", true);
            speed = 150f;
            health = 4f;
        }
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker
                .StartPath(myRigidbody.position,
                target.position,
                OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null) return;
        if (currentWaypoint >= path.vectorPath.Count)
        {
            //reachedEndOfPath = true;
            return;
        }
        else
        {
            //reachedEndOfPath = false;
        }

        Vector2 direction =
            ((Vector2) path.vectorPath[currentWaypoint] - myRigidbody.position)
                .normalized;

        Vector2 force = direction * speed * Time.deltaTime;

        myRigidbody.AddForce (force);

        float distance =
            Vector2
                .Distance(myRigidbody.position,
                path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    void Update()
    {
        if (isDead)
        {
            if (waitCountdown <= 0)
            {
                Destroy (gameObject);
            }
            else
            {
                waitCountdown -= Time.deltaTime;
            }
        }
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
                myRigidbody.gravityScale = 0.3f;
                squidCollider.enabled = false;
                speed = 0f;
                isDead = true;
            }
        }
    }
}
