using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    void Start()
    {
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
}
