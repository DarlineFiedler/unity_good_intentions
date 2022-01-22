using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public float waitTime;

    public Animator musicAnim;

    public GameObject menu;

    public GameObject loadingInterface;

    public Image loadingProgressBar;

    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();

    private void Start()
    {
        PlayerPrefs.SetFloat("maxHealth", 3f);
        PlayerPrefs.SetFloat("currentHealth", 3f);
        PlayerPrefs.SetFloat("damage", 1f);
        PlayerPrefs.SetInt("hasBell", 0);
        PlayerPrefs.SetInt("hasBB", 0);
    }

    public void HideMenu()
    {
        menu.SetActive(false);
    }

    public void ShowLoadingScreen()
    {
        loadingInterface.SetActive(true);
    }

    public void PlayGame()
    {
        HideMenu();
        ShowLoadingScreen();
        scenesToLoad.Add(SceneManager.LoadSceneAsync("House"));
        musicAnim.SetTrigger("fadeOut");
        StartCoroutine(LoadingScreen());
        //StartCoroutine(ChangeScene());
    }

    IEnumerator LoadingScreen()
    {
        float totalProgress = 0;
        for (int i = 0; i < scenesToLoad.Count; i++)
        {
            while (!scenesToLoad[i].isDone)
            {
                totalProgress += scenesToLoad[i].progress;
                loadingProgressBar.fillAmount =
                    totalProgress / scenesToLoad.Count;
                yield return null;
            }
        }
    }

    /*  IEnumerator ChangeScene()
    {
        musicAnim.SetTrigger("fadeOut");
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    } */
    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
