using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingTrigger : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D fallingObject;

    [SerializeField]
    float gravity = 1.5f;

    private void Update()
    {
        if (fallingObject == null)
        {
            Destroy (gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            fallingObject.gravityScale = gravity;
        }
    }
}
