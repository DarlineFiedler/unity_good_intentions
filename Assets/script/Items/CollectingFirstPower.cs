using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectingFirstPower : MonoBehaviour
{
    BoxCollider2D myBoxCollider;

    public GameObject Power1;

    public GameObject Text;

    [SerializeField]
    TextMeshProUGUI textComponent;

    [SerializeField]
    TextMeshProUGUI textSpeaker;

    [SerializeField]
    GameObject DialogeBox;

    [SerializeField]
    GameObject InfoBox;

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

        PlayerPrefs.SetInt("isTalking", 0);
    }

    public void Interacting()
    {
        if (myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            textComponent.text = string.Empty;
            textSpeaker.text = speaker;
            DialogeBox.SetActive(true);
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
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Text.SetActive(false);
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
            PlayerPrefs.SetInt("hasFirstPower", 1);
            PlayerPrefs.SetInt("safePoint", 15);
            DialogeBox.SetActive(false);
            PlayerPrefs.SetInt("Shroom1", 0);
            PlayerPrefs.SetInt("Shroom2", 0);
            PlayerPrefs.SetInt("Shroom3", 0);
            PlayerPrefs.SetInt("Shroom4", 0);
            PlayerPrefs.SetInt("Spike2", 0);
            isTalking = false;
            PlayerPrefs.SetInt("isTalking", 0);
            Power1.SetActive(false);
            InfoBox.SetActive(true);
        }
    }
}
