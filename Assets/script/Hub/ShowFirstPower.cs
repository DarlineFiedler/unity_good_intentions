using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowFirstPower : MonoBehaviour
{
    [SerializeField]
    GameObject PowerSign;

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("hasFirstPower") == 1)
        {
            PowerSign.SetActive(true);
        }
        else
        {
            PowerSign.SetActive(false);
        }
    }
}
