using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpenWallblock : MonoBehaviour
{
    BoxCollider2D myBoxCollider;

    Animator myAnimator;

    public GameObject Text;

    [SerializeField]
    GameObject Wallblock;

    [SerializeField]
    string side;

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

        if (PlayerPrefs.GetInt("isRankWallOpen") == 1)
        {
            Destroy (Wallblock);
        }
    }

    private void Update()
    {
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
            if (
                side == "right" ||
                (side == "left" && PlayerPrefs.GetInt("hasFirstPower") == 0)
            )
            {
                textComponent.text = string.Empty;
                textSpeaker.text = speaker;
                DialogeBox.SetActive(true);
                StartDialogue();
            }
            if (side == "left" && PlayerPrefs.GetInt("hasFirstPower") == 1)
            {
                PlayerPrefs.SetInt("isRankWallOpen", 1);
                Destroy (Wallblock);
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
            Text.SetActive(true);
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

                isTalking = false;
                PlayerPrefs.SetInt("isTalking", 0);
            }
        }
        else
        {
            DialogeBox.SetActive(false);

            isTalking = false;
            PlayerPrefs.SetInt("isTalking", 0);
        }
    }
}
