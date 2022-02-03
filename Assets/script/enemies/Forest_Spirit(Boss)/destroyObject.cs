using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyObjectToCleanSecen());
    }

    IEnumerator DestroyObjectToCleanSecen()
    {
        yield return new WaitForSecondsRealtime(5f);
        Destroy (gameObject);
    }
}
