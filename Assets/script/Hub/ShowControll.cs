using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowControll : MonoBehaviour
{
    [SerializeField]
    GameObject Controll;

    void Start()
    {
        if (PlayerPrefs.GetInt("hideControll") == 1)
        {
            Controll.SetActive(false);
        }
        else
        {
            Controll.SetActive(true);
        }
    }
}
