using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    SpriteRenderer rend;

    [SerializeField]
    Sprite clickCursor;

    [SerializeField]
    Sprite normalCursor;

    void Start()
    {
        Cursor.visible = false;
        rend = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPos;
        if (Input.GetMouseButtonDown(0))
        {
            rend.sprite = clickCursor;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            rend.sprite = normalCursor;
        }
    }
}
