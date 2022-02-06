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

    [SerializeField]
    float numberOfHealing = 2f;

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
            myAnimator.SetBool("hasLowHealth", true);
        }
        else
        {
            catHeadAnimator.SetBool("lowHealth", false);
            myAnimator.SetBool("hasLowHealth", false);
        }

        if (PlayerPrefs.GetFloat("currentHealth") <= 0)
        {
            myAnimator.SetBool("isDead", true);
            catHeadAnimator.SetBool("isDead", true);
            if (waitCountdown <= 0)
            {
                if (PlayerPrefs.GetInt("safeAfterBoss") == 0)
                {
                    PlayerPrefs.SetInt("ForestSpiritIsDead", 0);
                    PlayerPrefs.SetInt("hasFirstPower", 0);
                    PlayerPrefs.SetInt("isRankWallOpen", 0);
                    PlayerPrefs.SetInt("Shroom1", 1);
                    PlayerPrefs.SetInt("Shroom2", 1);
                    PlayerPrefs.SetInt("Shroom3", 0);
                    PlayerPrefs.SetInt("Shroom4", 0);
                    PlayerPrefs.SetInt("Spike2", 0);
                }
                myAnimator.SetBool("isDead", false);
                catHeadAnimator.SetBool("isDead", false);
                if (
                    PlayerPrefs.GetInt("safePoint") == 7 ||
                    PlayerPrefs.GetInt("safePoint") == 13 ||
                    PlayerPrefs.GetInt("safePoint") == 15 ||
                    PlayerPrefs.GetInt("safePoint") == 17
                )
                {
                    PlayerPrefs.SetInt("previosSceneIndex", 1000);
                }
                SceneManager.LoadSceneAsync(PlayerPrefs.GetInt("safePoint"));
                PlayerPrefs
                    .SetFloat("currentHealth",
                    PlayerPrefs.GetFloat("maxHealth"));
                PlayerPrefs.SetFloat("Healing", numberOfHealing);
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
            playerHasHorizontalSpeed ||
            PlayerPrefs.GetInt("canHeal") == 0 ||
            PlayerPrefs.GetFloat("Healing") <= 0
        )
        {
            return;
        }
        if (value.isPressed && !isHealing && !playerHasHorizontalSpeed)
        {
            if (
                health < PlayerPrefs.GetFloat("maxHealth") &&
                PlayerPrefs.GetFloat("currentHealth") > 0 &&
                PlayerPrefs.GetFloat("Healing") > 0
            )
            {
                myAnimator.SetTrigger("isHealing");
                isHealing = true;
                PlayerPrefs.SetInt("isHealing", 1);
                health += 2f;
                if (health > 3f)
                {
                    health = 3f;
                }
                PlayerPrefs
                    .SetFloat("Healing", (PlayerPrefs.GetFloat("Healing") - 1));
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

        if (other.gameObject.tag == "Boss")
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

        if (other.tag == "curruptionPuddle")
        {
            StartCoroutine(DamageOverTimeCoroutine(2f));
        }
    }

    IEnumerator DamageOverTimeCoroutine(float damageAmount)
    {
        float amountDamaged = 0f;
        float damagePerLoop = 1f;

        while (amountDamaged < damageAmount)
        {
            health -= damagePerLoop;
            amountDamaged += damagePerLoop;
            myAnimator.SetTrigger("getHurt");
            catHeadAnimator.SetTrigger("getHurt");
            PlayerPrefs.SetFloat("currentHealth", health);
            yield return new WaitForSecondsRealtime(1f);
        }
    }
}
