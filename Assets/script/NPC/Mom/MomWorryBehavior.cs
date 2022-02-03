using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MomWorryBehavior : MonoBehaviour
{
    BoxCollider2D myBoxCollider;

    Animator myAnimator;

    [SerializeField]
    Animator vAnimator;

    [SerializeField]
    GameObject Text;

    [SerializeField]
    string[] giveBB;

    [SerializeField]
    string[] talkAgainAfterBB;

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

    void Start()
    {
        myBoxCollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();

        PlayerPrefs.SetInt("isTalking", 0);
        if (PlayerPrefs.GetInt("ForestSpiritIsDead") == 0)
        {
            Destroy (gameObject);
        }

        if (PlayerPrefs.GetInt("momHasBB") == 1)
        {
            myAnimator.SetBool("hasBB", true);
            vAnimator.SetBool("hasBB", false);
            PlayerPrefs.SetInt("hasBB", 0);
        }
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

    public void Interacting()
    {
        if (myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            textComponent.text = string.Empty;
            textSpeaker.text = speaker;
            DialogeBox.SetActive(true);
            PlayerPrefs.SetFloat("currentHealth", 3f);
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
                if (PlayerPrefs.GetInt("momHasBB") == 0)
                {
                    vAnimator.SetBool("hasBB", false);
                    myAnimator.SetBool("hasBB", true);
                    PlayerPrefs.SetInt("momHasBB", 1);
                    PlayerPrefs.SetInt("hasBB", 0);
                }

                isTalking = false;
                PlayerPrefs.SetInt("isTalking", 0);
                myAnimator.SetBool("isTalking", false);
            }
        }
        else
        {
            DialogeBox.SetActive(false);

            if (PlayerPrefs.GetInt("momHasBB") == 0)
            {
                vAnimator.SetBool("hasBB", false);
                myAnimator.SetBool("hasBB", true);
                PlayerPrefs.SetInt("momHasBB", 1);
                PlayerPrefs.SetInt("hasBB", 0);
            }

            isTalking = false;
            PlayerPrefs.SetInt("isTalking", 0);
            myAnimator.SetBool("isTalking", false);
        }
    }

    void getText()
    {
        if (PlayerPrefs.GetInt("momHasBB") == 0)
        {
            giveBB.CopyTo(lines, 0);
        }
        if (PlayerPrefs.GetInt("momHasBB") == 1)
        {
            talkAgainAfterBB.CopyTo(lines, 0);
        }
    }
}
