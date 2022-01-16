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

    Rigidbody2D rb;

    float health = 2f;

    float playerDamage = 1f;

    CapsuleCollider2D collider2D;

    Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        playerDamage = PlayerPrefs.GetFloat("damage");
        collider2D = GetComponent<CapsuleCollider2D>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
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
            ((Vector2) path.vectorPath[currentWaypoint] - rb.position)
                .normalized;

        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce (force);

        float distance =
            Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
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
                collider2D.enabled = false;
                speed = 0f;
            }
        }
    }
}
