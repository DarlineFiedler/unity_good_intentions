using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class falllingObjects : MonoBehaviour
{
    /*     Rigidbody2D myRigidbody;

    BoxCollider2D myBoxCollider;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
    } */
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (
            other.gameObject.tag == "Player" ||
            other.gameObject.tag == "Ground" ||
            other.gameObject.tag == "Boss"
        )
        {
            Destroy (gameObject);
        }
    }
}
