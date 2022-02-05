using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectingBell : MonoBehaviour
{
    BoxCollider2D myBoxCollider;

    [SerializeField]
    GameObject Bell;

    [SerializeField]
    GameObject Text;

    [SerializeField]
    GameObject Rune;

    [SerializeField]
    GameObject GlowRune;

    [SerializeField]
    GameObject InfoBox;

    [SerializeField]
    TextMeshProUGUI textComponent;

    [SerializeField]
    TextMeshProUGUI textSpeaker;

    [SerializeField]
    GameObject DialogeBox;

    [SerializeField]
    string speaker;

    [SerializeField]
    string[] lines;

    [SerializeField]
    float textSpeed;

    bool isTalking = false;

    private int index;

    void Start()
    {
        myBoxCollider = GetComponent<BoxCollider2D>();

        /*  textComponent.text = string.Empty;
        textSpeaker.text = speaker;
        StartDialogue(); */
        PlayerPrefs.SetInt("isTalking", 0);
    }

    public void Interacting()
    {
        if (myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            textComponent.text = string.Empty;
            textSpeaker.text = speaker;
            DialogeBox.SetActive(true);
            PlayerPrefs.SetInt("safePoint", 7);
            StartDialogue();
        }
    }

    void Update()
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Text.SetActive(true);
            Rune.SetActive(false);
            GlowRune.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Text.SetActive(false);
            Rune.SetActive(true);
            GlowRune.SetActive(false);
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
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
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            DialogeBox.SetActive(false);
            isTalking = false;
            PlayerPrefs.SetInt("isTalking", 0);
            PlayerPrefs.SetInt("hasBell", 1);
            Bell.SetActive(false);
            InfoBox.SetActive(true);
        }
    }
}
