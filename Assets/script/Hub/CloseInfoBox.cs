using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseInfoBox : MonoBehaviour
{
    [SerializeField]
    GameObject InfoBox;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InfoBox.SetActive(false);
        }
    }
}
