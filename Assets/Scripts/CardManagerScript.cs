using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManagerScript : MonoBehaviour
{
    [Header("Rise animation")]
    public float startingY;
    public float targetY;
    public bool rising;
    public bool clicked;
    public float riseSpeed;
    public float riseHeight;
    public BoxCollider2D hitbox;

    // Start is called before the first frame update
    void Start()
    {
        startingY = transform.position.y;
        targetY = startingY;
        rising = false;
        riseHeight += transform.position.y;
        hitbox = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
        hitbox.offset = new Vector2(hitbox.offset.x, (startingY-transform.position.y)/gameObject.transform.localScale.x);
    }

    void OnMouseEnter() 
    {
        rising = true;

        if (!IngameCutsceneManagerScript.isCheckingCards) { // Stop moving during cutscenes
            rising = false;
        }
    }

    void OnMouseOver() {
        if (rising && transform.position.y < riseHeight) {
            targetY = Mathf.Lerp(targetY, riseHeight, riseSpeed * Time.deltaTime);
        }
    }

    void OnMouseExit() 
    {
        rising = false;
        targetY = startingY;
        hitbox.offset = new Vector2(hitbox.offset.x, 0);
    }

    void OnMouseDown()
    {
        clicked = true;
    }
}
