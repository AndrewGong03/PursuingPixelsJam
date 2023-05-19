using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    [Header("Field dimensions")]
    public float xRangeMax;
    public float xRangeMin;
    public float yRangeMax;
    public float yRangeMin;
    [Header("Movement settings")]
    public float moveSpeed;
    public float friction;
    public float wallBounciness;
    public Vector3 mvmtDirection;

    [Header("On-screen checks")]
    public GameObject chipCorner;
    public GameObject kennyKeeper;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3 (0,1.05f,0);
        moveSpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if Chip and Kenny are onscreen
        bool chipOnScreen = IsOnScreen(chipCorner);
        bool kennyOnScreen = IsOnScreen(kennyKeeper);

        if (chipOnScreen || kennyOnScreen)
        {
            moveSpeed = 0; // Stop ball if on screen
        } 

        // Bounce off the "walls" (xRange and yRange)
        if (transform.position.x < xRangeMin || transform.position.x > xRangeMax) {
            mvmtDirection.x *= -1 * wallBounciness;
        }
        if (transform.position.y < yRangeMin || transform.position.y > yRangeMax) {
            mvmtDirection.y *= -1 * wallBounciness;
        }

        // Move towards mvmtDirection
        transform.position += mvmtDirection * moveSpeed * Time.deltaTime;

        // Slow down due to friction
        var frictionDecay = 1 / (1 + friction * Time.deltaTime);
        moveSpeed *= frictionDecay;
    }

    bool IsOnScreen(GameObject gameObject)
    {
        Vector3 objectPosition = Camera.main.WorldToViewportPoint(gameObject.transform.position);

        // Check if the object is within the screen boundaries
        bool isOnScreen = (objectPosition.x > 0 && objectPosition.x < 1 && objectPosition.y > 0 && objectPosition.y < 1);

        return isOnScreen;
    }
}
