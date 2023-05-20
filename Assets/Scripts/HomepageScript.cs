using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomepageScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed;
    public Vector3 mousePosition;
    public float kickSpeed;
    public float startDelay;
    private float timer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > startDelay) {
            // Get the mouse position in screen coordinates
            mousePosition = Input.mousePosition;

            // Convert the mouse position to world coordinates
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            mousePosition.z = 0;
            // Set the object's position to the mouse position
            transform.position = Vector3.MoveTowards(transform.position, mousePosition, moveSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Ball")) {
            BallMenuScript ballScript = other.gameObject.GetComponent<BallMenuScript>();
            this.kickSpeed = Mathf.Clamp(Vector3.Distance(transform.position, this.mousePosition)*5, 0.5f, 5f);
            ballScript.moveSpeed = this.kickSpeed;
            ballScript.mvmtDirection = (other.transform.position - this.transform.position).normalized;
        }
    }


}
