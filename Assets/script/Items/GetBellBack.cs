using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBellBack : MonoBehaviour
{
    public GameObject Bell;

    void Update()
    {
        if (PlayerPrefs.GetInt("hasBell") == 0)
        {
            Bell.SetActive(true);
        }
        if (PlayerPrefs.GetInt("hasBell") == 1)
        {
            Bell.SetActive(false);
        }
    }
}
