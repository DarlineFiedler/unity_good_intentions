using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class enterBossFight : MonoBehaviour
{
    public float zoomSpeed = 1;

    public float targetOrtho;

    public float smoothSpeed = 2.0f;

    public float minOrtho = 1.0f;

    public float maxOrtho = 7.0f;

    public CinemachineVirtualCamera vcam;

    private bool isTrigger = false;

    public GameObject Barrier;

    public GameObject Boss;

    public GameObject TriggerBoss;

    void Start()
    {
        if (PlayerPrefs.GetInt("ForestSpiritIsDead") == 1)
        {
            Destroy (Boss);
            Destroy (gameObject);
            Destroy (Barrier);
            Destroy (TriggerBoss);
        }
        else
        {
            targetOrtho = vcam.m_Lens.OrthographicSize;
            vcam.m_Lens.OrthographicSize = 2.56f;
        }
    }

    void Update()
    {
        if (isTrigger)
        {
            targetOrtho -= zoomSpeed;
            targetOrtho = Mathf.Clamp(targetOrtho, maxOrtho, maxOrtho);

            vcam.m_Lens.OrthographicSize =
                Mathf
                    .MoveTowards(vcam.m_Lens.OrthographicSize,
                    targetOrtho,
                    smoothSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isTrigger = true;
            Barrier.SetActive(true);
        }
    }
}
