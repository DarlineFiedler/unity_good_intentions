using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BbBehavior : MonoBehaviour
{
    BoxCollider2D myBoxCollider;

    Animator myAnimator;

    public GameObject BB;

    public GameObject Text;

    void Start()
    {
        myBoxCollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
    }

    public void Interacting()
    {
        if (myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            Debug.Log("Welp");
            PlayerPrefs.SetInt("hasBB", 1);
            BB.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            myAnimator.SetBool("isHappy", false);
            Text.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            myAnimator.SetBool("isHappy", true);
            Text.SetActive(true);
        }
    }
}
