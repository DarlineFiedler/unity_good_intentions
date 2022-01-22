using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectingBell : MonoBehaviour
{
    BoxCollider2D myBoxCollider;

    public GameObject Bell;

    public GameObject Text;

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Text.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Text.SetActive(false);
        }
    }
}
