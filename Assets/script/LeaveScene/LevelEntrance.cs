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

    public GameObject Text;

    void Start()
    {
        myBoxCollider = GetComponent<BoxCollider2D>();
    }

    public void Interacting()
    {
        if (myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            PlayerPrefs.SetInt("hideControll", 1);
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
        SceneManager.LoadSceneAsync (nextSceneIndex);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Text.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Text.SetActive(false);
        }
    }
}
