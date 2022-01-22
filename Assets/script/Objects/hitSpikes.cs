using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitSpikes : MonoBehaviour
{
    [SerializeField]
    private List<int> levelEntries;

    [SerializeField]
    private List<Transform> resetPoints;

    [SerializeField]
    private Transform player;

    [SerializeField]
    float levelLoadDelay = 0.2f;

    void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(SetPlayerPosition());
    }

    IEnumerator SetPlayerPosition()
    {
        for (int i = 0; i < levelEntries.Count; i++)
        {
            if (levelEntries[i] == PlayerPrefs.GetInt("previosSceneIndex"))
            {
                yield return new WaitForSecondsRealtime(levelLoadDelay);
                player.localScale =
                    new Vector2(PlayerPrefs.GetFloat("playerFacing", 0.2f),
                        0.2f);
                player.position = resetPoints[i].position;
            }
        }
    }
}
