using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneManagerScript : MonoBehaviour
{
    public static bool isInCutscene;
    public static bool isCheckingCards;
    public IngameCutsceneScriptableObject[] cutsceneArray;
    public IngameCutsceneScriptableObject currentCutscene;
    public GameObject chipCorner;
    public GameObject kennyKeeper;
    public Vector3 targetChipPosition;
    public Vector3 targetKennyPosition;

    public float movementDuration = 2.0f; // the time it takes for the movement

    public CommentatorSpeechScript chipSpeechBubble;
    public CommentatorSpeechScript kennySpeechBubble;

    public CardManagerScript redCard;
    public CardManagerScript yellowCard;
    public CardManagerScript greenCard;

    public Text infoText; // text above cards
    public Text refText; // text next to ref
    public TransitionSpawnerScript grayTransition;

    private bool cardClicked = false;

    public float exitDelay = 0.5f; // how long it'll linger on the last dialogue before ending the cutscene


    // Start is called before the first frame update
    void Start()
    {
        isInCutscene = false;
        isCheckingCards = false;
        StartCutscene(cutsceneArray[0]);
    }

    public void StartCutscene(IngameCutsceneScriptableObject cutscene) {
        isInCutscene = true;
        currentCutscene = cutscene;
        cardClicked = false;
        StartCoroutine(grayTransition.FadeInGrayWholeScreen());
        StartCoroutine(PlayCutscene());
    }


    IEnumerator PlayCutscene()
    {
        yield return MoveToPositions();

        for (int i = 0; i < currentCutscene.dialogueLines.Length; i++) {
            if (currentCutscene.speakerOrder[i] == 1) {
                chipSpeechBubble.Show(currentCutscene.dialogueLines[i]);
                yield return WaitForNextKeyPress();
                chipSpeechBubble.Hide();
            }
            else if (currentCutscene.speakerOrder[i] == 2) {
                kennySpeechBubble.Show(currentCutscene.dialogueLines[i]);
                yield return WaitForNextKeyPress();
                kennySpeechBubble.Hide();
            }
        }

        if (currentCutscene.hasChoices) {
            isCheckingCards = true;
            StartCoroutine(grayTransition.FadeOutGrayBottomHalf());
            StartCoroutine(CheckCards());
        }
    }

    IEnumerator CheckCards() {
        while (!cardClicked) {
            yield return null;

            infoText.text = currentCutscene.infoText;
            
            // check the status of the cards
            if (redCard.rising)
            {
                refText.text = currentCutscene.redCardText[0];
            }
            else if (yellowCard.rising)
            {
                refText.text = currentCutscene.yellowCardText[0];
            }
            else if (greenCard.rising)
            {
                refText.text = currentCutscene.greenCardText[0];
            }
            else
            {
                refText.text = "";
            }

            if (Input.GetMouseButtonDown(0)) // Left mouse button clicked
            {
                if (redCard.clicked) {
                    chipSpeechBubble.Show(currentCutscene.redCardText[1]);
                    cardClicked = true;
                }
                else if (yellowCard.clicked) {
                    chipSpeechBubble.Show(currentCutscene.yellowCardText[1]);
                    cardClicked = true;
                }
                else if (greenCard.clicked) {
                    chipSpeechBubble.Show(currentCutscene.greenCardText[1]);
                    cardClicked = true;
                }
                else {
                    infoText.text = "";
                }
            }
        }

        StartCoroutine(ExitCutscene());
    }

    IEnumerator ExitCutscene() {
        isCheckingCards = false;
        infoText.text = "";
        yield return new WaitForSeconds(exitDelay);
        yield return WaitForNextKeyPress();
        StartCoroutine(grayTransition.FadeOutGrayWholeScreen());
        yield return MoveToPositions(targetChipPosition + new Vector3(-5.6f,1.19f,0), targetKennyPosition + new Vector3(6.5f,1.19f,0));
        isInCutscene = false;
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

    private IEnumerator WaitForNextKeyPress()
    {
        bool done = false;
        while (!done)
        {
            if (Input.anyKeyDown)
            {
                done = true;
            }

            yield return null;
        }
    }

    private IEnumerator WaitForNextMouseDown()
    {
        bool done = false;
        while (!done)
        {
            if (Input.GetMouseButtonDown(0))
            {
                done = true;
            }

            yield return null;
        }
    }
}