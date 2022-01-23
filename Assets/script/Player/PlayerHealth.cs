using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    float health = 3f;

    Animator myAnimator;

    [SerializeField]
    Animator catHeadAnimator;

    Rigidbody2D myRigidbody;

    float waitCountdown = 1.2f;

    [SerializeField]
    float healingCountdown;

    [SerializeField]
    float waitingTime = 6f;

    BoxCollider2D myBoxCollider;

    bool isHealing = false;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
        if (PlayerPrefs.GetFloat("maxHealth") < 3f)
        {
            PlayerPrefs.SetFloat("maxHealth", 3f);
        }
        health = PlayerPrefs.GetFloat("currentHealth");
        healingCountdown = waitingTime;
    }

    private void Update()
    {
        if (PlayerPrefs.GetFloat("currentHealth") <= 1f)
        {
            catHeadAnimator.SetBool("lowHealth", true);
        }
        else
        {
            catHeadAnimator.SetBool("lowHealth", false);
        }

        if (PlayerPrefs.GetFloat("currentHealth") <= 0)
        {
            myAnimator.SetBool("isDead", true);
            catHeadAnimator.SetBool("isDead", true);
            if (waitCountdown <= 0)
            {
                myAnimator.SetBool("isDead", false);
                catHeadAnimator.SetBool("isDead", false);
                SceneManager.LoadScene(1);
                PlayerPrefs
                    .SetFloat("currentHealth",
                    PlayerPrefs.GetFloat("maxHealth"));
            }
            else
            {
                waitCountdown -= Time.deltaTime;
            }
        }
        if (isHealing)
        {
            if (healingCountdown <= 0)
            {
                healingCountdown = waitingTime;
                isHealing = false;
                PlayerPrefs.SetInt("isHealing", 0);
            }
            else
            {
                healingCountdown -= Time.deltaTime;
            }
        }
    }

    void OnHeal(InputValue value)
    {
        bool playerHasHorizontalSpeed =
            Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (
            !myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) ||
            playerHasHorizontalSpeed
        )
        {
            return;
        }
        if (value.isPressed && !isHealing && !playerHasHorizontalSpeed)
        {
            if (
                health < PlayerPrefs.GetFloat("maxHealth") &&
                PlayerPrefs.GetFloat("currentHealth") > 0
            )
            {
                myAnimator.SetTrigger("isHealing");
                isHealing = true;
                PlayerPrefs.SetInt("isHealing", 1);
                health += 1f;
                PlayerPrefs.SetFloat("currentHealth", health);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            myAnimator.SetTrigger("getHurt");
            catHeadAnimator.SetTrigger("getHurt");
            health -= 1;
            PlayerPrefs.SetFloat("currentHealth", health);
        }
        if (other.gameObject.tag == "Falling")
        {
            myAnimator.SetTrigger("getHurt");
            catHeadAnimator.SetTrigger("getHurt");
            health -= 1;
            PlayerPrefs.SetFloat("currentHealth", health);
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
        }
        if (other.tag == "Enemy")
        {
            myAnimator.SetTrigger("getHurt");
            catHeadAnimator.SetTrigger("getHurt");
            health -= 1;
            PlayerPrefs.SetFloat("currentHealth", health);
        }

        if (other.tag == "Boss")
        {
            myAnimator.SetTrigger("getHurt");
            catHeadAnimator.SetTrigger("getHurt");
            health -= 1;
            PlayerPrefs.SetFloat("currentHealth", health);
        }
    }
}