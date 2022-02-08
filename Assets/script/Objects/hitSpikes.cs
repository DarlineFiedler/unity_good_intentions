using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (other.tag == "Player")
        {
            
            if((SceneManager.GetActiveScene().buildIndex == 7 && PlayerPrefs.GetInt("safePoint") == 7)||
               (SceneManager.GetActiveScene().buildIndex == 13 && PlayerPrefs.GetInt("safePoint") == 13 ) ||
               (SceneManager.GetActiveScene().buildIndex == 15&&PlayerPrefs.GetInt("safePoint") == 15 ) ||
               (SceneManager.GetActiveScene().buildIndex == 17&& PlayerPrefs.GetInt("safePoint") == 17)) {
                PlayerPrefs.SetInt("previosSceneIndex", 1000);
               }
            
            StartCoroutine(SetPlayerPosition());
        }
    }

    IEnumerator SetPlayerPosition()
    {
        for (int i = 0; i < levelEntries.Count; i++)
        {
            if (levelEntries[i] == PlayerPrefs.GetInt("previosSceneIndex"))
            {
                yield return new WaitForSecondsRealtime(levelLoadDelay);
                player.localScale =
                    new Vector2(PlayerPrefs.GetFloat("playerFacing", 0.6f),
                        0.6f);
                player.position = resetPoints[i].position;
            }
        }
    }
}
