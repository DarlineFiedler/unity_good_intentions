using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{
    AudioSource audioSrc;

    public AudioClip menuClip;

    public AudioClip mainClip;

    public AudioClip bossClip;

    Scene scene;

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();

        // check if Volume was set in previous game session
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 0.25f);
            AudioListener.volume = 0.25f;
        }
        else
        {
            AudioListener.volume = PlayerPrefs.GetFloat("musicVolume");
        }

        // check if Music was muted in previous game session
        if (!PlayerPrefs.HasKey("musicMuted"))
        {
            PlayerPrefs.SetInt("musicMuted", 0);
            AudioListener.pause = false;
        }
        else
        {
            AudioListener.pause = PlayerPrefs.GetInt("musicMuted") == 1;
        }
    }

    /*  private void Update()
    {
        scene = SceneManager.GetActiveScene();
        Debug.Log(scene.buildIndex);
        if (scene.buildIndex == 0)
        {
            audioSrc.clip = menuClip;
            audioSrc.Play();
        }
        else if (scene.buildIndex == 15)
        {
            audioSrc.clip = bossClip;
            audioSrc.Play();
        }
        else if (
            scene.buildIndex == 1 ||
            scene.buildIndex == 14 ||
            scene.buildIndex == 18
        )
        {
            audioSrc.clip = mainClip;
            audioSrc.Play();
        }
    } */
}
