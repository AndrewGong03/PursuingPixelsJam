using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManagerScript : MonoBehaviour
{
    [Header("Rise animation")]
    public float startingY;
    public float targetY;
    public bool rising;
    public float riseSpeed;
    public float riseHeight;

    // Start is called before the first frame update
    void Start()
    {
        startingY = transform.position.y;
        targetY = startingY;
        rising = false;
        riseHeight += transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
    }

    void OnMouseEnter() 
    {
        rising = true;
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
    }
}
