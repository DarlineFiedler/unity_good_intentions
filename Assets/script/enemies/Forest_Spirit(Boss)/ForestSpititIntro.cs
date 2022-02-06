using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestSpititIntro : MonoBehaviour
{
    [SerializeField]
    BoxCollider2D bossBoxCollider;
    [SerializeField]
    BoxCollider2D wall;

    [SerializeField]
    Rigidbody2D bossRigidbody;

    BoxCollider2D myBoxCollider;

    [SerializeField]
    Animator bossAnimator;

    // Start is called before the first frame update
    void Start()
    {
        bossBoxCollider.enabled = false;
        myBoxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            bossAnimator.SetBool("isLanding", true);
            bossBoxCollider.enabled = true;
            bossRigidbody.bodyType = RigidbodyType2D.Kinematic;
            myBoxCollider.enabled = false;
            wall.enabled = false;
        }
    }
}
