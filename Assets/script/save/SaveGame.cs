using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    BoxCollider2D myBoxCollider;

    [SerializeField]
    Animator VAnimator;

    [SerializeField]
    GameObject Text;

    bool isSaveGame = false;

    [SerializeField]
    int seceneNumber;

    // Start is called before the first frame update
    void Start()
    {
        myBoxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Interacting()
    {
        if (myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            VAnimator.SetBool("save", !isSaveGame);
            if (isSaveGame)
            {
                PlayerPrefs.SetInt("save", 0);
            }
            else
            {
                PlayerPrefs.SetInt("save", 1);
            }
            isSaveGame = !isSaveGame;
            PlayerPrefs.SetInt("safePoint", seceneNumber);
            PlayerPrefs.SetFloat("Healing", 2f);
            PlayerPrefs.SetFloat("currentHealth", 3f);
            if (PlayerPrefs.GetInt("ForestSpiritIsDead") == 1)
            {
                PlayerPrefs.SetInt("safeAfterBoss", 1);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Text.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Text.SetActive(true);
        }
    }
}
