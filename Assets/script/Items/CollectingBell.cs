using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectingBell : MonoBehaviour
{
    BoxCollider2D myBoxCollider;

    public GameObject Bell;

    void Start()
    {
        myBoxCollider = GetComponent<BoxCollider2D>();
    }

    public void Interacting()
    {
        if (myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            PlayerPrefs.SetInt("hasBell", 1);
            Bell.SetActive(false);
        }
    }
}
