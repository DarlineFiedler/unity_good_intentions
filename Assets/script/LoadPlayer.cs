using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPlayer : MonoBehaviour
{
    [SerializeField]
    private List<int> levelEntries;

    [SerializeField]
    private List<Transform> spawnPoints;

    [SerializeField]
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        // set Player Position
        if (PlayerPrefs.HasKey("previosSceneIndex"))
        {
            SetPlayerPosition();
        }
    }

    void SetPlayerPosition()
    {
        for (int i = 0; i < levelEntries.Count; i++)
        {
            if (levelEntries[i] == PlayerPrefs.GetInt("previosSceneIndex"))
            {
                player.localScale =
                    new Vector2(PlayerPrefs.GetFloat("playerFacing", 0.2f),
                        0.2f);
                player.position = spawnPoints[i].position;
            }
        }
    }
}
