using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMenuScript : MonoBehaviour
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


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3 (0,-1.0f,0);
        moveSpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {

        // Calculate the new position
        Vector3 newPosition = transform.position + mvmtDirection * moveSpeed * Time.deltaTime;

        // Check if the new position is within the field dimensions
        if (newPosition.x < xRangeMin || newPosition.x > xRangeMax)
        {
            // Reverse the x-direction and apply wall bounciness
            mvmtDirection.x *= -1 * wallBounciness;
            newPosition.x = Mathf.Clamp(newPosition.x, xRangeMin, xRangeMax);
        }

        if (newPosition.y < yRangeMin || newPosition.y > yRangeMax)
        {
            // Reverse the y-direction and apply wall bounciness
            mvmtDirection.y *= -1 * wallBounciness;
            newPosition.y = Mathf.Clamp(newPosition.y, yRangeMin, yRangeMax);
        }

        // Update the position
        transform.position = newPosition;

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
