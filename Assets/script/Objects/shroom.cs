using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shroom : MonoBehaviour
{
    Animator myAnimator;

    BoxCollider2D myBoxCollider;

    [SerializeField]
    BoxCollider2D shroomBodyCollider;

    [SerializeField]
    string shroomName;

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        myBoxCollider.enabled = false;
        shroomBodyCollider.enabled = false;
        if (PlayerPrefs.GetInt(shroomName) == 1)
        {
            myAnimator.SetBool("isActive", true);
        }
        else
        {
            myAnimator.SetBool("isActive", false);
        }
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt(shroomName) == 1)
        {
            myBoxCollider.enabled = true;
            shroomBodyCollider.enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            myAnimator.SetBool("isBounching", true);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            myAnimator.SetBool("isBounching", false);
        }
    }
}
