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
        PlayerPrefs.SetInt("hasBB", 0);
        PlayerPrefs.SetInt("isHealing", 0);
        PlayerPrefs.SetInt("foundBB", 0);
        PlayerPrefs.SetInt("isTalking", 0);
        PlayerPrefs.SetFloat("Healing", 2f);
        PlayerPrefs.SetInt("safePoint", 1);

        // need for Talking
        PlayerPrefs.SetInt("everTalkToMom", 0);
        PlayerPrefs.SetInt("ignoreMom", 0);
        PlayerPrefs.SetInt("talkedToMomAfterBell", 0);
        PlayerPrefs.SetInt("everTalkToBro", 0);
        PlayerPrefs.SetInt("talkedToBroAfterBell", 0);
        PlayerPrefs.SetInt("leaveTreeOnceAfterTalkedToBro", 0);
        PlayerPrefs.SetInt("everTalkedToRobben", 0);

        // set skills
        PlayerPrefs.SetInt("hasBell", 0);
        PlayerPrefs.SetInt("canHeal", 0);
        PlayerPrefs.SetInt("hasFirstPower", 0);

        if (!PlayerPrefs.HasKey("safePoint"))
        {
            PlayerPrefs.SetInt("safePoint", 1);
        }

        //reset Shroomes and rankes
        PlayerPrefs.SetInt("isRankWallOpen", 0);
        PlayerPrefs.SetInt("Shroom1", 1);
        PlayerPrefs.SetInt("Shroom2", 1);
        PlayerPrefs.SetInt("Shroom3", 0);
        PlayerPrefs.SetInt("Shroom4", 0);
        PlayerPrefs.SetInt("Spike2", 0);

        //resetBoss
        PlayerPrefs.SetInt("ForestSpiritIsDead", 0);
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
