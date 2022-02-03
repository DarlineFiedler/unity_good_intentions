using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeAfterBoss : MonoBehaviour
{
    [SerializeField]
    GameObject OldPlayground;

    [SerializeField]
    GameObject NewPlayground;

    [SerializeField]
    GameObject TreeGround;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("ForestSpiritIsDead") == 1)
        {
            OldPlayground.SetActive(false);
            NewPlayground.SetActive(true);
            Destroy (TreeGround);
        }
        else
        {
            OldPlayground.SetActive(true);
            NewPlayground.SetActive(false);
        }
    }
}
