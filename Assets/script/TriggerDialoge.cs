using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TriggerDialoge : MonoBehaviour
{
    /*  [SerializeField]
    Animator speakerAnimator;

    [SerializeField]
    TextMeshProUGUI textComponent;

    [SerializeField]
    TextMeshProUGUI textSpeaker; */
    [SerializeField]
    GameObject Mom;

    [SerializeField]
    /*   GameObject DialogeBox;

    [SerializeField]
    string speaker;

    [SerializeField]
    string[] lines;

    [SerializeField]
    float textSpeed;

    bool isTalking = false;

    private int index;
 */
    // Start is called before the first frame update
    /*   void Start()
    {
         textComponent.text = string.Empty;
        textSpeaker.text = speaker;
        StartDialogue(); 
     PlayerPrefs.SetInt("isTalking", 0);
    } */
    // Update is called once per frame
    /*  void Update()
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
    } */
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("trigger collison ");
        if (other.gameObject.tag == "Player")
        {
            Mom.GetComponent<MomBehavior>().Interacting("leftBarrier");

            Debug.Log("trigger collison with player");
            /* textComponent.text = string.Empty;
            textSpeaker.text = speaker;
            DialogeBox.SetActive(true);
            StartDialogue(); */
        }
    }

    /*     void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
        isTalking = true;
        PlayerPrefs.SetInt("isTalking", 1);
        speakerAnimator.SetBool("isTalking", true);
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
            speakerAnimator.SetBool("isTalking", false);
            if (gameObject.tag == "leftBarrier")
            {
                PlayerPrefs.SetInt("ignoreMom", 1);
            }
        }
    } */
}
