using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMusic : MonoBehaviour
{
    private void Start()
    {
        Destroy(GameObject.Find("backgroundMusic"));
    }
}
