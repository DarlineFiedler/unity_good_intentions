using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class LevelExitOnInteract : MonoBehaviour
{
    BoxCollider2D myBoxCollider;

 [SerializeField]
    int nextSceneIndex;

    [SerializeField]
    float levelLoadDelay = 1f;

void Start()
    {
        myBoxCollider = GetComponent<BoxCollider2D>();

       
    }


  void OnInteract(InputValue value)
    {
        if(myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Player"))) {

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
