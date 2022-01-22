using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBBBack : MonoBehaviour
{
    public GameObject BB;

    void Update()
    {
        if (PlayerPrefs.GetInt("hasBB") == 0)
        {
            BB.SetActive(true);
        }
        if (PlayerPrefs.GetInt("hasBB") == 1)
        {
            BB.SetActive(false);
        }
    }
}
