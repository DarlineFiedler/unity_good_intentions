using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MomBehavior : MonoBehaviour
{
    BoxCollider2D myBoxCollider;

    Animator myAnimator;

    [SerializeField]
    GameObject Text;

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

    bool talkingBecauseOfTrigger = false;

    void Start()
    {
        myBoxCollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();

        PlayerPrefs.SetInt("isTalking", 0);
    }

    private void Update()
    {
        if (!talkingBecauseOfTrigger)
        {
            getText();
        }

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

        if (PlayerPrefs.GetInt("everTalkToMom") == 1)
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
        }
    }

    public void Interacting(string trigger)
    {
        if (myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            textComponent.text = string.Empty;
            textSpeaker.text = speaker;
            DialogeBox.SetActive(true);
            StartDialogue();
        }

        if (trigger == "leftBarrier")
        {
            talkingBecauseOfTrigger = true;
            Array.Copy(triggerLeftBarrier, lines, 4);
            textComponent.text = string.Empty;
            textSpeaker.text = speaker;
            DialogeBox.SetActive(true);
            StartDialogue();
        }

        if (trigger == "rightBarrier")
        {
            talkingBecauseOfTrigger = true;
            Array.Copy(triggerRightBarrier, lines, 4);
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
                if (talkingBecauseOfTrigger)
                {
                    PlayerPrefs.SetInt("ignoreMom", 1);
                }

                if (
                    (
                    PlayerPrefs.GetInt("ignoreMom") == 1 ||
                    PlayerPrefs.GetInt("ignoreMom") == 0
                    ) &&
                    !talkingBecauseOfTrigger
                )
                {
                    PlayerPrefs.SetInt("everTalkToMom", 1);
                    leftBarrier.enabled = false;
                }
                if (
                    PlayerPrefs.GetInt("hasBell") == 1 &&
                    PlayerPrefs.GetInt("talkedToMomAfterBell") == 0 &&
                    !talkingBecauseOfTrigger
                )
                {
                    PlayerPrefs.SetInt("safePoint", 1);
                    PlayerPrefs.SetInt("talkedToMomAfterBell", 1);
                    PlayerPrefs.SetInt("canHeal", 1);
                    rightBarrier.enabled = false;
                }
                isTalking = false;
                PlayerPrefs.SetInt("isTalking", 0);
                myAnimator.SetBool("isTalking", false);
                talkingBecauseOfTrigger = false;
            }
        }
        else
        {
            DialogeBox.SetActive(false);
            if (talkingBecauseOfTrigger)
            {
                PlayerPrefs.SetInt("ignoreMom", 1);
            }
            if (
                (
                PlayerPrefs.GetInt("ignoreMom") == 1 ||
                PlayerPrefs.GetInt("ignoreMom") == 0
                ) &&
                !talkingBecauseOfTrigger
            )
            {
                PlayerPrefs.SetInt("everTalkToMom", 1);
                leftBarrier.enabled = false;
            }
            if (
                PlayerPrefs.GetInt("hasBell") == 1 &&
                PlayerPrefs.GetInt("talkedToMomAfterBell") == 0 &&
                !talkingBecauseOfTrigger
            )
            {
                PlayerPrefs.SetInt("safePoint", 1);
                PlayerPrefs.SetInt("talkedToMomAfterBell", 1);
                PlayerPrefs.SetInt("canHeal", 1);
                rightBarrier.enabled = false;
            }
            isTalking = false;
            PlayerPrefs.SetInt("isTalking", 0);
            myAnimator.SetBool("isTalking", false);
            talkingBecauseOfTrigger = false;
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
