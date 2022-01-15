using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    float health = 3f;

    Animator myAnimator;

    [SerializeField]
    Animator catHeadAnimator;

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
        if (PlayerPrefs.GetFloat("maxHealth") < 3f)
        {
            PlayerPrefs.SetFloat("maxHealth", 3f);
        }
        health = PlayerPrefs.GetFloat("currentHealth");
    }

    private void Update()
    {
        if (PlayerPrefs.GetFloat("currentHealth") <= 1f)
        {
            catHeadAnimator.SetBool("lowHealth", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Spikes")
        {
            myAnimator.SetTrigger("getHurt");
            catHeadAnimator.SetTrigger("getHurt");
            health -= 1;
            PlayerPrefs.SetFloat("currentHealth", health);

            if (PlayerPrefs.GetFloat("currentHealth") <= 0)
            {
                SceneManager.LoadScene(1);
                PlayerPrefs
                    .SetFloat("currentHealth",
                    PlayerPrefs.GetFloat("maxHealth"));
            }
        }
        if (other.tag == "Enemy")
        {
            myAnimator.SetTrigger("getHurt");
            catHeadAnimator.SetTrigger("getHurt");
            health -= 1;
            PlayerPrefs.SetFloat("currentHealth", health);

            if (PlayerPrefs.GetFloat("currentHealth") <= 0)
            {
                SceneManager.LoadScene(1);
                PlayerPrefs
                    .SetFloat("currentHealth",
                    PlayerPrefs.GetFloat("maxHealth"));
            }
        }
    }
}
