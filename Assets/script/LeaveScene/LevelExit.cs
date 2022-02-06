using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField]
    int nextSceneIndex;

    /*     [SerializeField]
    float levelLoadDelay = 0.2f; */
    void OnTriggerEnter2D(Collider2D other)
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        //StartCoroutine(LoadNextLevel());
        SceneManager.LoadSceneAsync (nextSceneIndex);

        // set nextSceneInde
        PlayerPrefs.SetInt("previosSceneIndex", currentSceneIndex);
    }

    /* IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(0f);

        // int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //SceneManager.LoadScene (nextSceneIndex);
        SceneManager.LoadSceneAsync (nextSceneIndex);
    } */
}
