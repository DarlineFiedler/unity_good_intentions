using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

using Random = UnityEngine.Random;

public class showShroomText : MonoBehaviour
{
    [SerializeField]
    GameObject Text;

    [SerializeField]
    string shroomName;

    [SerializeField]
    Animator shroomAnimator;

    BoxCollider2D myBoxCollider;

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
    }

    public void Interacting()
    {
        if (myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            if (PlayerPrefs.GetInt(shroomName) == 0)
            {
                if (PlayerPrefs.GetInt("hasFirstPower") == 1)
                {
                    shroomAnimator.SetBool("isActive", true);
                    PlayerPrefs.SetInt(shroomName, 1);
                }
                else
                {
                    textComponent.text = string.Empty;
                    textSpeaker.text = speaker;
                    DialogeBox.SetActive(true);
                    StartDialogue();
                }
            }
        }
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt(shroomName) == 1)
        {
            // Destroy (Text);
            myBoxCollider.enabled = false;
        }
        else
        {
            myBoxCollider.enabled = true;
        }

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
            Text.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (PlayerPrefs.GetInt(shroomName) == 0)
            {
                Text.SetActive(true);
            }
            else
            {
                Text.SetActive(false);
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
    }
}
