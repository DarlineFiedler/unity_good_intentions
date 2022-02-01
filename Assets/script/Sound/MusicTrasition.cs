using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicTrasition : MonoBehaviour
{
    private static MusicTrasition backgroundMusic;

    private void Awake()
    {
        if (backgroundMusic == null)
        {
            backgroundMusic = this;
            DontDestroyOnLoad (backgroundMusic);
            if (SceneManager.GetActiveScene().buildIndex == 15)
            {
                SceneManager.sceneLoaded += OnSceneLoaded;
            }
        }
        else
        {
            Destroy (gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 15)
        {
            Destroy (backgroundMusic);
        }
    }
}
