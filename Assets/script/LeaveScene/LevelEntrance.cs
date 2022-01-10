using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelEntrance : MonoBehaviour
{
    BoxCollider2D myBoxCollider;

    [SerializeField]
    int nextSceneIndex;

    [SerializeField]
    float levelLoadDelay = 0.2f;

    void Start()
    {
        myBoxCollider = GetComponent<BoxCollider2D>();
    }

    public void Interacting()
    {
        if (myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            StartCoroutine(LoadNextLevel());

            // set nextSceneInde
            PlayerPrefs.SetInt("previosSceneIndex", currentSceneIndex);
        }
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);

        // int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene (nextSceneIndex);
    }
}
