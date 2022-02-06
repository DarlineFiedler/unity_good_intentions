using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicTrasition : MonoBehaviour
{
    public AudioSource audioSrc;

    public AudioClip menuClip;

    public AudioClip mainClip;

    public AudioClip bossClip;

    Scene scene;

    private static MusicTrasition backgroundMusic;

    private void Awake()
    {
        if (backgroundMusic == null)
        {
            backgroundMusic = this;
            DontDestroyOnLoad (backgroundMusic);

            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneLoaded += OnSceneLoaded;
            scene = SceneManager.GetActiveScene();
            /* if (SceneManager.GetActiveScene().buildIndex == 15)
            {
                SceneManager.sceneLoaded += OnSceneLoaded;
            } */
        }
        else
        {
            Destroy (gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0)
        {
            audioSrc.Stop();
            audioSrc.clip = menuClip;
            audioSrc.Play();
        }
        else if (scene.buildIndex == 15)
        {
            if (PlayerPrefs.GetInt("ForestSpiritIsDead") == 0)
            {
                audioSrc.Stop();
                audioSrc.clip = bossClip;
                audioSrc.Play();
            }
            else
            {
                audioSrc.Stop();
                audioSrc.clip = mainClip;
                audioSrc.Play();
            }
        }
        else if (
            scene.buildIndex == 1 ||
            scene.buildIndex == 14 ||
            scene.buildIndex == 18
        )
        {
            audioSrc.Stop();
            audioSrc.clip = mainClip;
            audioSrc.Play();
        }
    }
}
