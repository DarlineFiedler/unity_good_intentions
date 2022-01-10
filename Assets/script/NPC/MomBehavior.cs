using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomBehavior : MonoBehaviour
{
    BoxCollider2D myBoxCollider;

    void Start()
    {
        myBoxCollider = GetComponent<BoxCollider2D>();
    }

    public void Interacting()
    {
        if (myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            Debug.Log("Hello Honey! How are U?");
        }
    }
}
