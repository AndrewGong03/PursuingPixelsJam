using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommentatorScript : MonoBehaviour
{
    public GameObject chipCorner;
    public GameObject kennyKeeper;
    public float initialDelay = 5.0f;
    public float speechDelay = 2.0f;
    public float speed = 5.0f;

    [SerializeField] private Rigidbody2D chipRb;
    [SerializeField] private Rigidbody2D kennyRb;
    [SerializeField] private CommentatorSpeechScript chipSpeechBubble; // assuming you have a SpeechBubble script or class
    [SerializeField] private CommentatorSpeechScript kennySpeechBubble;


    private bool isChipSpeaking = false;
    private bool isKennySpeaking = false;

    // Start is called before the first frame update
    void Start()
    {
        chipRb = chipCorner.GetComponent<Rigidbody2D>();
        kennyRb = kennyKeeper.GetComponent<Rigidbody2D>();

        chipSpeechBubble = chipCorner.GetComponent<SpeechBubble>();
        kennySpeechBubble = kennyKeeper.GetComponent<SpeechBubble>();

        StartCoroutine(StartCommentary());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartCommentary()
    {
        yield return new WaitForSeconds(initialDelay);

        // Moving Chip from left to right and Kenny from right to left
        chipRb.velocity = new Vector2(speed, 0);
        kennyRb.velocity = new Vector2(-speed, 0);

        // Assuming the objects stop at their positions in the frame
        yield return new WaitUntil(() => chipCorner.transform.position.x >= 0 && kennyKeeper.transform.position.x <= 0);
        chipRb.velocity = Vector2.zero;
        kennyRb.velocity = Vector2.zero;

        // Chip says something
        chipSpeechBubble.Show("Chip's first speech");

        // Wait for player to press any button and Kenny to respond, repeat 5 times
        for(int i = 0; i < 5; i++)
        {
            yield return new WaitUntil(() => Input.anyKeyDown);
            kennySpeechBubble.Show("Kenny's response " + (i+1));
            yield return new WaitForSeconds(speechDelay);
        }

        // Wait for player to press any button and Chip to respond, repeat 5 times
        for(int i = 0; i < 5; i++)
        {
            yield return new WaitUntil(() => Input.anyKeyDown);
            chipSpeechBubble.Show("Chip's response " + (i+1));
            yield return new WaitForSeconds(speechDelay);
        }

        // Chip and Kenny leave the screen
        chipRb.velocity = new Vector2(-speed, 0);
        kennyRb.velocity = new Vector2(speed, 0);
    }
}
