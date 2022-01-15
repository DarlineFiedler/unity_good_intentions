using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellOrb : MonoBehaviour
{
    [SerializeField]
    Transform bellOrbTip;

    [SerializeField]
    GameObject bullet;

    Vector2 lookDirection;

    float lookAngle;

    void Start()
    {
        if (PlayerPrefs.GetFloat("damage") < 1f)
        {
            PlayerPrefs.SetFloat("damage", 1f);
        }
    }

    void Update()
    {
        // TODO get gamepad stuff to
        lookDirection =
            Camera.main.ScreenToWorldPoint(Input.mousePosition) -
            transform.position;
        lookAngle =
            Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, lookAngle - 90f);
    }

    public void FireBullet()
    {
        GameObject fireBullet =
            Instantiate(bullet, bellOrbTip.position, bellOrbTip.rotation);
        fireBullet.GetComponent<Rigidbody2D>().velocity = bellOrbTip.up * 10f;
    }
}
