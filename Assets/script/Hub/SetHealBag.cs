using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHealBag : MonoBehaviour
{
    [SerializeField]
    GameObject twoHealingLeft;

    [SerializeField]
    GameObject oneHealingLeft;

    [SerializeField]
    GameObject noMoreHealingLeft;

    void Start()
    {
        if (!PlayerPrefs.HasKey("Healing"))
        {
            PlayerPrefs.SetFloat("Healing", 2f);
        }
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("canHeal") == 0)
        {
            twoHealingLeft.SetActive(false);
            oneHealingLeft.SetActive(false);
            noMoreHealingLeft.SetActive(false);
        }
        else
        {
            if (PlayerPrefs.GetFloat("Healing") >= 2f)
            {
                twoHealingLeft.SetActive(true);
                oneHealingLeft.SetActive(false);
                noMoreHealingLeft.SetActive(false);
            }
            if (PlayerPrefs.GetFloat("Healing") == 1f)
            {
                twoHealingLeft.SetActive(false);
                oneHealingLeft.SetActive(true);
                noMoreHealingLeft.SetActive(false);
            }
            if (PlayerPrefs.GetFloat("Healing") == 0f)
            {
                twoHealingLeft.SetActive(false);
                oneHealingLeft.SetActive(false);
                noMoreHealingLeft.SetActive(true);
            }
        }
    }
}
