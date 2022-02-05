using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

using Random = UnityEngine.Random;

public class GrowRanke : MonoBehaviour
{
    [SerializeField]
    GameObject GrowText;

    [SerializeField]
    GameObject BigRank;

    [SerializeField]
    GameObject SmallRank;

    [SerializeField]
    GameObject InteractText;

    [SerializeField]
    Animator rankAnimator;

    BoxCollider2D myBoxCollider;

    [SerializeField]
    string[] dontLeave;

    [SerializeField]
    string[] text1;

    [SerializeField]
    string[] text2;

    [SerializeField]
    string[] lines;

    [SerializeField]
    TextMeshProUGUI textComponent;

    [SerializeField]
    TextMeshProUGUI textSpeaker;

    [SerializeField]
    GameObject DialogeBox;

    [SerializeField]
    string speaker;

    [SerializeField]
    float textSpeed;

    private int index;

    bool isTalking = false;

    int textNumber = 0;

    private void Start()
    {
        myBoxCollider = GetComponent<BoxCollider2D>();
        PlayerPrefs.SetInt("isTalking", 0);

        if (PlayerPrefs.GetInt("isRankBig") == 1)
        {
            BigRank.SetActive(true);
            SmallRank.SetActive(false);
        }
    }

    public void Interacting()
    {
        if (myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            if (PlayerPrefs.GetInt("isRankBig") == 0)
            {
                if (
                    PlayerPrefs.GetInt("hasFirstPower") == 1 &&
                    PlayerPrefs.GetInt("hasBB") == 1
                )
                {
                    rankAnimator.SetBool("isGrowing", true);
                    PlayerPrefs.SetInt("isRankBig", 1);
                    BigRank.SetActive(true);
                    SmallRank.SetActive(false);
                }
                else if (PlayerPrefs.GetInt("hasFirstPower") == 0)
                {
                    textComponent.text = string.Empty;
                    textSpeaker.text = speaker;
                    DialogeBox.SetActive(true);
                    StartDialogue();
                }
            }
            else
            {
                int currentSceneIndex =
                    SceneManager.GetActiveScene().buildIndex;
                StartCoroutine(LoadNextLevel());

                // set nextSceneInde
                PlayerPrefs.SetInt("previosSceneIndex", currentSceneIndex);
            }
        }
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(0);

        // int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //SceneManager.LoadScene (nextSceneIndex);
        SceneManager.LoadSceneAsync(5);
    }

    private void Update()
    {
        getText();
        if (Input.GetKeyDown(KeyCode.Space) && isTalking)
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (PlayerPrefs.GetInt("isRankBig") == 1)
            {
                InteractText.SetActive(false);
                GrowText.SetActive(false);
            }
            else
            {
                GrowText.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (PlayerPrefs.GetInt("isRankBig") == 1)
            {
                InteractText.SetActive(true);
                GrowText.SetActive(false);
            }
            else
            {
                GrowText.SetActive(true);
            }
        }
    }

    void StartDialogue()
    {
        isTalking = true;
        PlayerPrefs.SetInt("isTalking", 1);
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        // Type each character 1 by 1
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            if (lines[index + 1] != "")
            {
                index++;
                textComponent.text = string.Empty;
                StartCoroutine(TypeLine());
            }
            else
            {
                DialogeBox.SetActive(false);
                textNumber = Random.Range(0, 2);
                isTalking = false;
                PlayerPrefs.SetInt("isTalking", 0);
            }
        }
        else
        {
            DialogeBox.SetActive(false);
            textNumber = Random.Range(0, 2);
            isTalking = false;
            PlayerPrefs.SetInt("isTalking", 0);
        }
    }

    void getText()
    {
        if (textNumber == 0)
        {
            text1.CopyTo(lines, 0);
        }

        if (textNumber == 1)
        {
            text2.CopyTo(lines, 0);
        }

        if (
            PlayerPrefs.GetInt("hasFirstPower") == 1 &&
            PlayerPrefs.GetInt("hasBB") == 1
        )
        {
        }
    }
}
