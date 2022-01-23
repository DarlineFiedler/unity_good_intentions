using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveTree : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.GetInt("everTalkToBro") == 1)
        {
            PlayerPrefs.SetInt("leaveTreeOnceAfterTalkedToBro", 1);
        }
        else
        {
            PlayerPrefs.SetInt("leaveTreeOnceAfterTalkedToBro", 0);
        }
    }
}
