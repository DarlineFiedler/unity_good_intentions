using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaveForest : MonoBehaviour
{
    [SerializeField]
    Animator VAnimator;

    [SerializeField]
    string[] bevorBB;

    [SerializeField]
    string[] withBB;

    [SerializeField]
    string[] afterBB;

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

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        VAnimator.SetBool("isRunning", false);
        if (other.gameObject.tag == "Player")
        {
            textComponent.text = string.Empty;
            textSpeaker.text = speaker;
            DialogeBox.SetActive(true);
            StartDialogue();
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
                VAnimator.SetBool("isRunning", false);

                isTalking = false;
                PlayerPrefs.SetInt("isTalking", 0);
            }
        }
        else
        {
            DialogeBox.SetActive(false);
            VAnimator.SetBool("isRunning", false);

            isTalking = false;
            PlayerPrefs.SetInt("isTalking", 0);
        }
    }

    void getText()
    {
        if (PlayerPrefs.GetInt("hasBB") == 1)
        {
            //Array.Copy(talkFirstTime, lines, 3);
            withBB.CopyTo(lines, 0);
        }
        else if (PlayerPrefs.GetInt("momHasBB") == 1)
        {
            afterBB.CopyTo(lines, 0);
        }
        else
        {
            bevorBB.CopyTo(lines, 0);
        }
    }
}
