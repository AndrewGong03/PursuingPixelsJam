using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomepageScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed;

    public GameObject ball;
    public float kickSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get the mouse position in screen coordinates
        Vector3 mousePosition = Input.mousePosition;

        // Convert the mouse position to world coordinates
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0;
        // Set the object's position to the mouse position
        transform.position = Vector3.MoveTowards(transform.position, mousePosition, moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Ball")) {
            BallMenuScript ballScript = other.gameObject.GetComponent<BallMenuScript>();
            ballScript.moveSpeed = this.kickSpeed;
            ballScript.mvmtDirection = (other.transform.position - this.transform.position).normalized;
        }
    }


}
