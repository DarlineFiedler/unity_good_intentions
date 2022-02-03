using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TriggerDialoge : MonoBehaviour
{
    [SerializeField]
    GameObject Mom;

    [SerializeField]
    string kind;

    [SerializeField]
    Animator VAnimator;

    private void Update()
    {
        if (Mom == null)
        {
            Destroy (gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            VAnimator.SetBool("isRunning", false);
            Mom.GetComponent<MomBehavior>().Interacting(kind);
        }
    }
}
