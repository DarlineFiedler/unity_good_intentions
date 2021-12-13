using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteMusic : MonoBehaviour
{
    [SerializeField]
    Image soundOnIcon;

    [SerializeField]
    Image soundOfIcon;

    private bool muted = false;

    // Start is called before the first frame update
    void Start()
    {
        // check if Music was muted in previous game session
        if (!PlayerPrefs.HasKey("musicMuted"))
        {
            PlayerPrefs.SetInt("musicMuted", 0);
            Load();
        }
        else
        {
            Load();
        }
        UpdateMuteButtonIcon();
        AudioListener.pause = muted;
    }

    // mute the music
    public void OnPressMuteButton()
    {
        if (muted == false)
        {
            muted = true;
            AudioListener.pause = true;
        }
        else
        {
            muted = false;
            AudioListener.pause = false;
        }
        Save();
        UpdateMuteButtonIcon();
    }

    // load the volume settings
    private void Load()
    {
        muted = PlayerPrefs.GetInt("musicMuted") == 1;
    }

    // save the volume settings
    private void Save()
    {
        PlayerPrefs.SetInt("musicMuted", muted ? 1 : 0);
    }

    // function that changes the icon when the music is muted.
    // Not used/needed at the moment
    private void UpdateMuteButtonIcon()
    {
        if (muted == false)
        {
            soundOnIcon.enabled = true;
            soundOfIcon.enabled = false;
        }
        else
        {
            soundOfIcon.enabled = true;
            soundOnIcon.enabled = false;
        }
    }
}

// PlayerPrefs can only store INT, FLOAT oder STRING
