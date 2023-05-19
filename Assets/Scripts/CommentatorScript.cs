using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommentatorScript : MonoBehaviour
{
    public GameObject chipCorner;
    public GameObject kennyKeeper;
    public Vector3 targetChipPosition;
    public Vector3 targetKennyPosition;
    public float initialDelay = 5.0f;
    public float speechDelay = 2.0f;
    public float movementDuration = 2.0f; // the time it takes for the movement


    private CommentatorSpeechScript chipSpeechBubble;
    private CommentatorSpeechScript kennySpeechBubble;

    // Start is called before the first frame update
    void Start()
    {
        chipSpeechBubble = chipCorner.GetComponent<CommentatorSpeechScript>();
        kennySpeechBubble = kennyKeeper.GetComponent<CommentatorSpeechScript>();

        StartCoroutine(StartCommentary());
        
    }


    IEnumerator StartCommentary()
    {
        yield return new WaitForSeconds(initialDelay);

        yield return MoveToPositions();

        // Chip says something
        chipSpeechBubble.Show("Hello, hello, HELLO! Buckle up, folks, we're about to dive headfirst into a brand new season of football so tantalizing it'll make your taste buds tingle! I'm Chip Corner, your trusty, hyped-up host for this rollercoaster ride, and next to me, the man whose face could stop a clock, Kenny Keeper!");

        // TODO: add the actual opening script 
        // Wait for player to press any button and Kenny to respond, repeat 5 times
        for(int i = 0; i < 5; i++)
        {
            yield return new WaitUntil(() => Input.anyKeyDown);
            chipSpeechBubble.Hide();
            kennySpeechBubble.Show("Kenny's response " + (i+1));
            // yield return new WaitForSeconds(speechDelay);
        }

        // Wait for player to press any button and Chip to respond, repeat 5 times
        for(int i = 0; i < 5; i++)
        {
            kennySpeechBubble.Hide();
            yield return new WaitUntil(() => Input.anyKeyDown);
            chipSpeechBubble.Show("Chip's response " + (i+1));
            // yield return new WaitForSeconds(speechDelay);
        }

        // Chip and Kenny leave the screen, you can modify the target positions for exit
        yield return MoveToPositions(targetChipPosition + new Vector3((float)-5.6,(float)1.19,0), targetKennyPosition + new Vector3((float)6.5,(float)1.19,0));
    }

    IEnumerator MoveToPositions(Vector3? targetChipPos = null, Vector3? targetKennyPos = null)
    {
        float elapsedTime = 0;

        kennySpeechBubble.Hide();
        chipSpeechBubble.Hide();

        Vector3 chipStartPosition = chipCorner.transform.position;
        Vector3 kennyStartPosition = kennyKeeper.transform.position;

        Vector3 finalChipPos = targetChipPos ?? targetChipPosition;
        Vector3 finalKennyPos = targetKennyPos ?? targetKennyPosition;

        while (elapsedTime < movementDuration)
        {
            float journeyFraction = elapsedTime / movementDuration;

            chipCorner.transform.position = Vector3.Lerp(chipStartPosition, finalChipPos, journeyFraction);
            kennyKeeper.transform.position = Vector3.Lerp(kennyStartPosition, finalKennyPos, journeyFraction);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        chipCorner.transform.position = finalChipPos;
        kennyKeeper.transform.position = finalKennyPos;
    }
}
