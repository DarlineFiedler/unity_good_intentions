using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHealthFish : MonoBehaviour
{
    [SerializeField]
    GameObject healthFish;

    [SerializeField]
    GameObject healthGoneFish;

    [SerializeField]
    float numberOfFish = 1f;

    void Update()
    {
        if (PlayerPrefs.GetFloat("currentHealth") >= numberOfFish)
        {
            healthFish.SetActive(true);
            healthGoneFish.SetActive(false);
        }
        if (PlayerPrefs.GetFloat("currentHealth") < numberOfFish)
        {
            healthFish.SetActive(false);
            healthGoneFish.SetActive(true);
        }
    }
}
