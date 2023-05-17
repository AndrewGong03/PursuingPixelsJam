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
        // Run at ball if within range
        if (Vector3.Distance(transform.position, ball.transform.position) < ballChaseRange) {
            newTransformPosition = Vector3.MoveTowards(transform.position, ball.transform.position, ballChaseSpeed * Time.deltaTime);
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
            transform.localScale = localScaleTemp;
        }
        else {
            localScaleTemp.x = 1;
            transform.localScale = localScaleTemp;
        }

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
}
