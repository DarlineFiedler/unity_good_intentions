using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutroMovement : MonoBehaviour
{
    float runSpeed = 5f;

    Rigidbody2D myRigidbody;

    BoxCollider2D myBoxCollider;

    Animator myAnimator;

    [SerializeField]
    GameObject Orb;

    [SerializeField]
    GameObject BellOrb;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        myAnimator.SetBool("isRunning", true);
        myAnimator.SetBool("hasBell", true);
        Orb.SetActive(true);
        BellOrb.SetActive(true);
    }

    void Update()
    {
        Vector2 placerVelocity = new Vector2(runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = placerVelocity;
    }
}
