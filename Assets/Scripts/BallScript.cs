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
        if (CutsceneManagerScript.isInCutscene) { // Stop moving during cutscenes
            moveSpeed = 0;
            return;
        }
        /* original code, in case I fucked anything up. New code should try to prevent ball going out of bounds
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
        */        

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
}
