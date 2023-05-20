using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuGoalScript : MonoBehaviour
{
    public LevelLoaderScript levelLoader;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Ball")) {
            BallMenuScript ballScript = other.gameObject.GetComponent<BallMenuScript>();
            ballScript.moveSpeed *= 0.1f; // Slow-down effect for ball on enter goal

            if (this.CompareTag("LeftGoal")) {
                levelLoader.LoadNextLevel();
            }
            else if (this.CompareTag("RightGoal")) {
                // Transition to tutorial
            }
        }
    }
}
