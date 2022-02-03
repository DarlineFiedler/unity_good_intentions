using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class Forest_Spirit : MonoBehaviour
{
    public Transform player;

    public bool isFlipped = false;

    [SerializeField]
    BoxCollider2D attackBox;

    BoxCollider2D myBoxCollider;

    float health = 30f;

    float playerDamage = 1f;

    BoxCollider2D forestSpiritCollider;

    public GameObject[] fallingSpikes;

    Animator myAnimator;

    [SerializeField]
    GameObject barrier;

    [SerializeField]
    GameObject barrierTrigger;

    int objectNumber = 0;

    private void Start()
    {
        playerDamage = PlayerPrefs.GetFloat("damage");
        forestSpiritCollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
        myBoxCollider = GetComponent<BoxCollider2D>();
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

            myAnimator.SetTrigger("getHurt");
            if (health <= 0)
            {
                myAnimator.SetBool("isDead", true);
                forestSpiritCollider.enabled = false;

                //Destroy (gameObject);
                barrier.SetActive(false);
                barrierTrigger.SetActive(false);
                PlayerPrefs.SetInt("ForestSpiritIsDead", 1);
            }
        }
    }

    public void AttackPlayer()
    {
        attackBox.enabled = true;

        myBoxCollider.enabled = false;
        objectNumber = Random.Range(0, 10);
        Debug.Log (objectNumber);
        Instantiate(fallingSpikes[objectNumber],
        new Vector3(0, 0, 0),
        Quaternion.identity);
    }

    public void AttackOver()
    {
        attackBox.enabled = false;

        myBoxCollider.enabled = true;
        PlayerPrefs.SetInt("bossIsAttaking", 0);
    }
}
