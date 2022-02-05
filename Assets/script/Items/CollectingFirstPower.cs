using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectingFirstPower : MonoBehaviour
{
    BoxCollider2D myBoxCollider;

    public GameObject Power1;

    public GameObject Text;

    public GameObject InfoBox;

    void Start()
    {
        myBoxCollider = GetComponent<BoxCollider2D>();
    }

    public void Interacting()
    {
        if (myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            PlayerPrefs.SetInt("hasFirstPower", 1);
            PlayerPrefs.SetInt("Shroom1", 0);
            PlayerPrefs.SetInt("Shroom2", 0);
            PlayerPrefs.SetInt("Shroom3", 0);
            PlayerPrefs.SetInt("Shroom4", 0);
            PlayerPrefs.SetInt("Spike2", 0);
            InfoBox.SetActive(true);
            Power1.SetActive(false);
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
