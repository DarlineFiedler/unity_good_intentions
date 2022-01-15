using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogHealth : MonoBehaviour
{
    float health = 2f;

    float playerDamage = 1f;

    private void Start()
    {
        playerDamage = PlayerPrefs.GetFloat("damage");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            health -= playerDamage;
            if (health <= 0)
            {
                Destroy (gameObject);
            }
        }
    }
}
