using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManagerScript : MonoBehaviour
{
    public Texture2D cursorTexture;
    public Texture2D cursorClickTexture;

    public Vector2 cursorOffset;

    public float animDuration;
    private float animTimer;
    

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursorTexture, cursorOffset, CursorMode.ForceSoftware);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            animTimer = 0;
            Cursor.SetCursor(cursorClickTexture, cursorOffset, CursorMode.ForceSoftware);
        }

        animTimer += Time.deltaTime;
        if (animTimer >= animDuration) {
            Cursor.SetCursor(cursorTexture, cursorOffset, CursorMode.ForceSoftware);
        }
    }
}
