using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shroom : MonoBehaviour
{
    Animator myAnimator;

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
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
