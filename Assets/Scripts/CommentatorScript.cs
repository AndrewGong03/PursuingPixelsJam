using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommentatorScript : MonoBehaviour
{
    public GameObject chipCorner;
    public GameObject kennyKeeper;
    public Vector3 targetChipPosition;
    public Vector3 targetKennyPosition;

    public float initialDelay = 5.0f; // when to start the opening script 
    public float speechDelay = 2.0f;
    public float movementDuration = 2.0f; // the time it takes for the movement
    public float levelDelay = 3.0f; // when to start next level

    public CommentatorSpeechScript chipSpeechBubble;
    public CommentatorSpeechScript kennySpeechBubble;

    public CardManagerScript redCard;
    public CardManagerScript yellowCard;
    public CardManagerScript greenCard;

    public Text displayText; // text next to ref

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
            kennySpeechBubble.Show("Opening Script. Kenny's response " + (i+1));
            // yield return new WaitForSeconds(speechDelay);
        }

        // Wait for player to press any button and Chip to respond, repeat 5 times
        for(int i = 0; i < 5; i++)
        {
            kennySpeechBubble.Hide();
            yield return new WaitUntil(() => Input.anyKeyDown);
            chipSpeechBubble.Show("Opening Script. Chip's response " + (i+1));
            // yield return new WaitForSeconds(speechDelay);
        }

        // Chip and Kenny leave the screen, you can modify the target positions for exit
        yield return MoveToPositions(targetChipPosition + new Vector3(-5.6f,1.19f,0), targetKennyPosition + new Vector3(6.5f,1.19f,0));

        StartCoroutine(Level1A());
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

    IEnumerator Level1A() 
    {
        yield return new WaitForSeconds(levelDelay);
        yield return MoveToPositions();

        // Chip says something
        chipSpeechBubble.Show("Chip Corner: Oh, look at that! Prince Patrick's gone down in the box, flopping like a fish out of water. I've seen better dives at the kiddie pool!");

        // Wait until user presses a button
        yield return new WaitUntil(() => Input.anyKeyDown);
        // yield return new WaitForSeconds(speechDelay); // alternatively wait speechDelay seconds

        // Kenny says something
        kennySpeechBubble.Show("Kenny Keeper: This is the moment of truth. Does our ref fall for the royal performance, or does he play the role of a heartless critic?");
        
        // Keep commentators on screen and handle player input
        while (true)
        {
            // player input options for the ref's speech bubble
            if (redCard.rising) {
                displayText.text = "Penalty for Royal United!";
            }
            else if (yellowCard.rising) {
                displayText.text = "Warning for Prince Patrick!";
            }
            else if (greenCard.rising) {
                displayText.text = "Nothing wrong here!";
            }
            else {
                displayText.text = "";
            }
            
            // Check for player input
            if (Input.GetMouseButtonDown(0)) // Left mouse button clicked
            {
                if (redCard.clicked) {
                    displayText.text = "red card clicked placeholder";
                }
                else if (yellowCard.rising) {
                    displayText.text = "yellow card clicked placeholder";
                    kennySpeechBubble.Show("Kenny Keeper: The ref saw through the act and rightly gave Patrick a yellow card!");
                    chipSpeechBubble.Hide();
                }
                else if (greenCard.rising) {
                    displayText.text = "green card clicked placeholder";
                    chipSpeechBubble.Show("Chip Corner: Patrick got away with it! The ref seems to have fallen for his theatrics!");
                    kennySpeechBubble.Hide();
                }
            }

            yield return null;
        }
    }
}
