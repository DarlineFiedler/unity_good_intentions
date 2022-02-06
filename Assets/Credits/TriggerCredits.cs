using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerCredits : MonoBehaviour
{
    [SerializeField]
    Animator CreditAnimator;

    [SerializeField]
    GameObject Background;

    [SerializeField]
    GameObject Text;

    void Start()
    {
        Background.SetActive(false);
        Text.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Background.SetActive(true);
        Text.SetActive(true);
        CreditAnimator.SetBool("startCredits", true);
        StartCoroutine(DestroyObjectToCleanSecen());
    }

    IEnumerator DestroyObjectToCleanSecen()
    {
        yield return new WaitForSecondsRealtime(15f);
        SceneManager.LoadSceneAsync(0);
        CreditAnimator.SetBool("startCredits", false);
        /*     Background.SetActive(false);
        Text.SetActive(false); */
    }
}
