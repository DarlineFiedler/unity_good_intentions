using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MomBehavior : MonoBehaviour
{
    BoxCollider2D myBoxCollider;

    Animator myAnimator;

    public GameObject Text;

    [SerializeField]
    string[] ignoreMom;

    [SerializeField]
    string[] talkFirstToMom;

    [SerializeField]
    string[] talkAgainBeforBell;

    [SerializeField]
    string[] talkAfterBell;

    [SerializeField]
    string[] talkAgainAfterBell;

    [SerializeField]
    string[] triggerLeftBarrier;

    [SerializeField]
    string[] triggerRightBarrier;

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

    [SerializeField]
    BoxCollider2D leftBarrier;

    [SerializeField]
    BoxCollider2D rightBarrier;

    bool isTalking = false;

    void Start()
    {
        myBoxCollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();

        PlayerPrefs.SetInt("isTalking", 0);
        /*  if (PlayerPrefs.GetInt("everTalkToMom") == 1)
        {
            leftBarrier.enabled = false;
        }
        else
        {
            leftBarrier.enabled = true;
        }
        if (PlayerPrefs.GetInt("talkedToMomAfterBell") == 1)
        {
            rightBarrier.enabled = false;
        }
        else
        {
            rightBarrier.enabled = true;
        } */
    }

    private void Update()
    {
        Debug.Log("Update mum");
        Debug.Log (isTalking);
        getText();
        if (Input.GetKeyDown(KeyCode.Space) && isTalking)
        {
            Debug.Log("press space");
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

    public void Interacting(string trigger)
    {
        Debug.Log("fiere this function");
        if (myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            textComponent.text = string.Empty;
            textSpeaker.text = speaker;
            DialogeBox.SetActive(true);
            StartDialogue();

            // Debug.Log("Hello Honey! How are U?");
        }

        if (trigger == "leftBarrier")
        {
            Debug.Log("fiere from barrarier trigger");
            Array.Copy(triggerLeftBarrier, lines, 4);
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
        isTalking = true;
        PlayerPrefs.SetInt("isTalking", 1);
        myAnimator.SetBool("isTalking", true);
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

                if (
                    PlayerPrefs.GetInt("ignoreMom") == 1 ||
                    PlayerPrefs.GetInt("ignoreMom") == 0
                )
                {
                    PlayerPrefs.SetInt("everTalkToMom", 1);
                    leftBarrier.enabled = false;
                }
                if (
                    PlayerPrefs.GetInt("hasBell") == 1 &&
                    PlayerPrefs.GetInt("talkedToMomAfterBell") == 0
                )
                {
                    PlayerPrefs.SetInt("talkedToMomAfterBell", 1);
                    PlayerPrefs.SetInt("canHeal", 1);
                    rightBarrier.enabled = false;
                }
                isTalking = false;
                PlayerPrefs.SetInt("isTalking", 0);
                myAnimator.SetBool("isTalking", false);
            }
        }
        else
        {
            DialogeBox.SetActive(false);
            if (
                PlayerPrefs.GetInt("ignoreMom") == 1 ||
                PlayerPrefs.GetInt("ignoreMom") == 0
            )
            {
                PlayerPrefs.SetInt("everTalkToMom", 1);
                leftBarrier.enabled = false;
            }
            if (
                PlayerPrefs.GetInt("hasBell") == 1 &&
                PlayerPrefs.GetInt("talkedToMomAfterBell") == 0
            )
            {
                PlayerPrefs.SetInt("talkedToMomAfterBell", 1);
                PlayerPrefs.SetInt("canHeal", 1);
            }
            isTalking = false;
            PlayerPrefs.SetInt("isTalking", 0);
            myAnimator.SetBool("isTalking", false);
        }
    }

    void getText()
    {
        if (PlayerPrefs.GetInt("ignoreMom") == 1)
        {
            Array.Copy(ignoreMom, lines, 4);
        }

        if (PlayerPrefs.GetInt("ignoreMom") == 0)
        {
            Array.Copy(talkFirstToMom, lines, 4);
        }

        if (
            PlayerPrefs.GetInt("everTalkToMom") == 1 &&
            PlayerPrefs.GetInt("talkedToMomAfterBell") == 0
        )
        {
            Array.Copy(talkAgainBeforBell, lines, 4);
        }

        if (
            PlayerPrefs.GetInt("hasBell") == 1 &&
            PlayerPrefs.GetInt("talkedToMomAfterBell") == 0
        )
        {
            Array.Copy(talkAfterBell, lines, 4);
        }

        if (
            PlayerPrefs.GetInt("everTalkToMom") == 1 &&
            PlayerPrefs.GetInt("talkedToMomAfterBell") == 1
        )
        {
            Array.Copy(talkAgainAfterBell, lines, 4);
        }
    }
}
