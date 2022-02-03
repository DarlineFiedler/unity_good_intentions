using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float timewaiting = 0.9f;

    float waitCountdown;

    // Start is called before the first frame update
    void Start()
    {
        waitCountdown = timewaiting;
    }

    // Update is called once per frame
    void Update()
    {
        if (waitCountdown <= 0)
        {
            Destroy (gameObject);
        }
        else
        {
            waitCountdown -= Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (
            other.tag != "Player" &&
            other.tag != "Enemy" &&
            other.tag != "FallingSpikes"
        )
        {
            Destroy (gameObject);
        }
    }
}
