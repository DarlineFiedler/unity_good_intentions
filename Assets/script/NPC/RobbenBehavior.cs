using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RobbenBehavior : MonoBehaviour
{
    BoxCollider2D myBoxCollider;

    Animator myAnimator;

    public GameObject Text;

    [SerializeField]
    TextMeshProUGUI textComponent;

    [SerializeField]
    TextMeshProUGUI textSpeaker;

    [SerializeField]
    GameObject DialogeBox;

    [SerializeField]
    string speaker;

    [SerializeField]
    string[] firstTalk;

    [SerializeField]
    string[] talkAgain;

    [SerializeField]
    string[] lines;

    [SerializeField]
    float textSpeed;

    bool isTalking = false;

    private int index;

    void Start()
    {
        myBoxCollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();

        PlayerPrefs.SetInt("isTalking", 0);
    }

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

    public void Interacting()
    {
        if (myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            myAnimator.SetBool("isTalking", true);
            textComponent.text = string.Empty;
            textSpeaker.text = speaker;
            DialogeBox.SetActive(true);
            StartDialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Text.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Text.SetActive(true);
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
        myAnimator.SetBool("isTalking", true);
        isTalking = true;
        PlayerPrefs.SetInt("isTalking", 1);
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
                PlayerPrefs.SetInt("everTalkedToRobben", 1);
                myAnimator.SetBool("isTalking", false);
                DialogeBox.SetActive(false);
                isTalking = false;
                PlayerPrefs.SetInt("isTalking", 0);
            }
        }
        else
        {
            PlayerPrefs.SetInt("everTalkedToRobben", 1);
            myAnimator.SetBool("isTalking", false);
            DialogeBox.SetActive(false);
            isTalking = false;
            PlayerPrefs.SetInt("isTalking", 0);
        }
    }

    void getText()
    {
        if (PlayerPrefs.GetInt("everTalkedToRobben") == 1)
        {
            talkAgain.CopyTo(lines, 0);
        }
        else
        {
            firstTalk.CopyTo(lines, 0);
        }
    }
}
