using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest_Spirit : MonoBehaviour
{
    public Transform player;

    public bool isFlipped = false;

    [SerializeField]
    BoxCollider2D attackBox;

    float health = 30f;

    float playerDamage = 1f;

    BoxCollider2D forestSpiritCollider;

    public GameObject fallingSpikes;

    Animator myAnimator;

    [SerializeField]
    GameObject barrier;

    [SerializeField]
    GameObject barrierTrigger;

    private void Start()
    {
        playerDamage = PlayerPrefs.GetFloat("damage");
        myAnimator = GetComponent<Animator>();
    }

    public void LookAtPlayer()
    {
        if (PlayerPrefs.GetInt("bossIsAttaking") == 0)
        {
            Vector3 flipped = transform.localScale;
            flipped.z *= -1f;

            if (transform.position.x > player.position.x && isFlipped)
            {
                transform.localScale = flipped;
                transform.Rotate(0f, 180f, 0f);
                isFlipped = false;
            }
            else if (transform.position.x < player.position.x && !isFlipped)
            {
                transform.localScale = flipped;
                transform.Rotate(0f, 180f, 0f);
                isFlipped = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            health -= playerDamage;

            if (health <= 10f)
            {
                myAnimator.SetBool("isEnraged", true);
            }

            // myAnimator.SetTrigger("getHurt");
            if (health <= 0)
            {
                // myAnimator.SetBool("isDead", true);
                //forestSpiritCollider.enabled = false;
                Destroy (gameObject);
                barrier.SetActive(false);
                barrierTrigger.SetActive(false);
                PlayerPrefs.SetInt("ForestSpiritIsDead", 1);
            }
        }
    }

    public void AttackPlayer()
    {
        attackBox.enabled = true;
        Instantiate(fallingSpikes, new Vector3(0, 0, 0), Quaternion.identity);
    }

    public void AttackOver()
    {
        attackBox.enabled = false;
        PlayerPrefs.SetInt("bossIsAttaking", 0);
    }
}
