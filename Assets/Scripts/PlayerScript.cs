using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Movement Settings")]
    public float xRangeMax;
    public float xRangeMin;
    public float yRangeMax;
    public float yRangeMin;
    private Vector3 newTransformPosition;

    [Header("Wander settings")]
    public float wanderSpeed;
    public Vector3 wanderTarget;
    public float wanderRadius;
    private float wanderTimer;
    public float maxWanderTime;

    [Header("Ball chasing")]
    public float ballChaseRange;
    public float ballChaseSpeed;

    public GameObject ball;
    public float kickSpeed;

    [Header("Dirt particles")]
    public ParticleSystem dirtParticles;
    public float dirtParticleDelay;
    private float dirtParticleTimer;

    public GameObject chipCorner;
    public GameObject kennyKeeper;

    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.Find("Ball");
        wanderTarget = transform.position;
        maxWanderTime = 3f;
        wanderTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if Chip and Kenny are onscreen
        bool chipOnScreen = IsOnScreen(chipCorner);
        bool kennyOnScreen = IsOnScreen(kennyKeeper);

        if (chipOnScreen || kennyOnScreen)
        {
            return; // Skip movement if Chip or Kenny are onscreen
        }

        // Run at ball if within range
        if (Vector3.Distance(transform.position, ball.transform.position) < ballChaseRange) {
            newTransformPosition = Vector3.MoveTowards(transform.position, ball.transform.position, ballChaseSpeed * Time.deltaTime);

            // Dirt particles
            if (dirtParticleTimer >= dirtParticleDelay) {
                // Put the particles at Players' feet and make it point away from direction of movement
                Vector3 dirtParticleXY = new Vector3(transform.position.x, transform.position.y-0.1f, 0);
                if (newTransformPosition.x < transform.position.x) { 
                    dirtParticles.transform.localScale = new Vector2 (1, dirtParticles.transform.localScale.y);
                }
                else {
                    dirtParticles.transform.localScale = new Vector2 (-1, dirtParticles.transform.localScale.y);
                }

                Instantiate(dirtParticles, dirtParticleXY, Quaternion.identity);
                dirtParticles.Play();
                dirtParticleTimer = 0;
            }
            else {
                dirtParticleTimer += Time.deltaTime;
            }
        }
        else { // Otherwise, wander randomly
            if (Vector3.Distance(transform.position, wanderTarget) < 0.1f || wanderTimer >= maxWanderTime) {
                wanderTarget = RandomWanderPoint();
                wanderTimer = 0;
            }
            newTransformPosition = Vector3.MoveTowards(transform.position, wanderTarget, wanderSpeed * Time.deltaTime);
            wanderTimer += Time.deltaTime;
        }

        // Flip horizontally when moving left
        Vector2 localScaleTemp = transform.localScale;
        if (newTransformPosition.x < transform.position.x) {
            localScaleTemp.x = -1;  
        }
        else {
            localScaleTemp.x = 1;
        }
        transform.localScale = localScaleTemp;

        transform.position = newTransformPosition; // Move
    }

    Vector3 RandomWanderPoint()
    {
        Vector3 randomPoint = (Random.insideUnitSphere * wanderRadius) + transform.position;
        randomPoint.z = 0;
        if (randomPoint.x < xRangeMin || randomPoint.x > xRangeMax || randomPoint.y < yRangeMin || randomPoint.y > yRangeMax) {
            randomPoint = RandomWanderPoint(); // Make sure randomPoint is within x and y range of the field
        }
        return randomPoint;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Ball")) {
            BallScript ballScript = other.gameObject.GetComponent<BallScript>();
            ballScript.moveSpeed = this.kickSpeed;
            ballScript.mvmtDirection = (other.transform.position - this.transform.position).normalized;
        }
    }

    bool IsOnScreen(GameObject gameObject)
    {
        Vector3 objectPosition = Camera.main.WorldToViewportPoint(gameObject.transform.position);

        // Check if the object is within the screen boundaries
        bool isOnScreen = (objectPosition.x > 0 && objectPosition.x < 1 && objectPosition.y > 0 && objectPosition.y < 1);

        return isOnScreen;
    }

}
