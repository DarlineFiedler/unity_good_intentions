using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBackground : MonoBehaviour
{
    [SerializeField]
    GameObject oldPlayground;

    [SerializeField]
    GameObject newPlayground;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("ForestSpiritIsDead") == 1)
        {
            oldPlayground.SetActive(false);
            newPlayground.SetActive(true);
        }
        else
        {
            oldPlayground.SetActive(true);
            newPlayground.SetActive(false);
        }
    }
}
