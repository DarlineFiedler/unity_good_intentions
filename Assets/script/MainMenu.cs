using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public float waitTime;

    public Animator musicAnim;

    private void Start()
    {
        PlayerPrefs.SetFloat("maxHealth", 3f);
        PlayerPrefs.SetFloat("currentHealth", 3f);
        PlayerPrefs.SetFloat("damage", 1f);
    }

    public void PlayGame()
    {
        StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene()
    {
        musicAnim.SetTrigger("fadeOut");
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
