using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IngameCutsceneManagerScript : MonoBehaviour
{
    // Global variables
    public static bool isInCutscene;
    public static bool isCheckingCards;
    public static int currentDay;

    // Load in cutscenes
    public IngameCutsceneScriptableObject[] cutsceneArray;
    public IngameCutsceneScriptableObject currentCutscene;

    // Chip and Kenny objects
    public GameObject chipCorner;
    public GameObject kennyKeeper;
    public Vector3 targetChipPosition;
    public Vector3 targetKennyPosition;
    public CommentatorSpeechScript chipSpeechBubble;
    public CommentatorSpeechScript kennySpeechBubble;
    public float movementDuration = 2.0f; // the time it takes for the movement

    // Card objects
    public CardManagerScript redCard;
    public CardManagerScript yellowCard;
    public CardManagerScript greenCard;
    public Text infoText; // text above cards
    public TMP_Text scoreText; // text that displays score changes when the ref makes a choice
    public Text refText; // text next to ref
    // Gray filter transition
    public TransitionSpawnerScript grayTransition;
    private bool cardClicked = false; // Whether a card has been selected

    // Move between cutscenes
    public float startDelay = 5f;
    public float betweenCutsceneDelay = 5f;
    public float exitDelay = 0.5f; // how long it'll linger on the last dialogue before ending the cutscene
    public int sceneCount = 5; // number of scenes, indexed at 1
    public int sceneCounter = 1;

    // Start is called before the first frame update
    void Start()
    {
        isInCutscene = false;
        isCheckingCards = false;
        StartCoroutine(StartDay());
    }

    void Update()
    {
        /**
        if (sceneCounter < sceneCount && !isInCutscene) 
        {
            StartCutscene(cutsceneArray[sceneCounter]);
            sceneCounter += 1;
        }
        */
    }

    IEnumerator StartDay() { // Plays all of the scenes in a given day
        yield return new WaitForSeconds(startDelay);

        for (int i = 0; i < sceneCount; i++) {
            StartCutscene(cutsceneArray[i]);
            yield return PlayCutscene();
            yield return new WaitForSeconds(betweenCutsceneDelay);
        }
    }

    public void StartCutscene(IngameCutsceneScriptableObject cutscene) {
        isInCutscene = true;
        currentCutscene = cutscene;
        cardClicked = false;
        StartCoroutine(grayTransition.FadeInGrayWholeScreen());
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
            yield return CheckCards();
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
                    StartCoroutine(grayTransition.FadeInGrayBottomHalf());
                    chipSpeechBubble.Show(currentCutscene.redCardText[1]);
                    cardClicked = true;
                    ScoreScript.instance.ChangePop(currentCutscene.pointIfRed[0]);
                    ScoreScript.instance.ChangeCred(currentCutscene.pointIfRed[1]);
                    ScoreScript.instance.ChangeWin(currentCutscene.pointIfRed[2], currentCutscene.teamCode);
                }
                else if (yellowCard.clicked) {
                    StartCoroutine(grayTransition.FadeInGrayBottomHalf());
                    chipSpeechBubble.Show(currentCutscene.yellowCardText[1]);
                    cardClicked = true;
                    ScoreScript.instance.ChangePop(currentCutscene.pointIfYellow[0]);
                    ScoreScript.instance.ChangeCred(currentCutscene.pointIfYellow[1]);
                    ScoreScript.instance.ChangeWin(currentCutscene.pointIfYellow[2], currentCutscene.teamCode);
                }
                else if (greenCard.clicked) {
                    StartCoroutine(grayTransition.FadeInGrayBottomHalf());
                    chipSpeechBubble.Show(currentCutscene.greenCardText[1]);
                    cardClicked = true;
                    ScoreScript.instance.ChangePop(currentCutscene.pointIfGreen[0]);
                    ScoreScript.instance.ChangeCred(currentCutscene.pointIfGreen[1]);
                    ScoreScript.instance.ChangeWin(currentCutscene.pointIfGreen[2], currentCutscene.teamCode);
                }
                else {
                    infoText.text = "";
                }
                
                /**
                Debug.Log(ScoreScript.pop); // debugging purposes only 
                Debug.Log(ScoreScript.cred);
                Debug.Log(ScoreScript.winScore[currentCutscene.teamCode]);
                */
            }
        }

        yield return ExitCutscene();
    }

    IEnumerator ExitCutscene() {
        refText.text = "";
        infoText.text = "";
        scoreText.text = "";
        yield return new WaitForSeconds(exitDelay);
        yield return WaitForNextKeyPress();
        StartCoroutine(grayTransition.FadeOutGrayWholeScreen());
        yield return DisplayScoreText(0);
        yield return DisplayScoreText(1);
        yield return MoveToPositions(targetChipPosition + new Vector3(-5.6f,1.19f,0), targetKennyPosition + new Vector3(6.5f,1.19f,0));
        isInCutscene = false;
        isCheckingCards = false;
        UnclickAllCards();
    }

    IEnumerator DisplayScoreText(int index) { // Important: Does not display win score changes, I'll try to implement that separately
        Debug.Log("Displaying score text: " + index);
        Vector3 scoreTextPos = new Vector3 (0, 1.6f, 0); // Starting position for score text
        scoreText.GetComponent<RectTransform>().anchoredPosition = scoreTextPos;

        // Preset color of score text, this will be changed
        Color32 scoreTextColor = new Color(255,255,255,255);
        scoreText.color = scoreTextColor;
        Color32 red = new Color32(255, 0, 0, 255);
        Color32 yellow = new Color32(254, 224, 0, 255);
        Color32 green = new Color32(0, 255, 0, 255);

        // Content of score change text depends on what type of score we're dealing with
        // Score change text will show up as red if losing points, green if gaining points
        switch (index) {
            case 0:
                scoreText.text = "Popularity";
                break;
            case 1:
                scoreText.text = "Credibility";
                break;
        }

        if (redCard.clicked) {
            if (currentCutscene.pointIfRed[index] < 0) {
                scoreText.text = "- " + scoreText.text;
                scoreTextColor = red;
            }
            else if (currentCutscene.pointIfRed[index] > 0) {
                scoreText.text = "+ " + scoreText.text;
                scoreTextColor = green;
            }
            else {
                scoreText.text = scoreText.text + " unaffected";
                scoreTextColor = yellow;
            }
        }
        else if (yellowCard.clicked) {
            if (currentCutscene.pointIfYellow[index] < 0) {
                scoreText.text = "- " + scoreText.text;
                scoreTextColor = red;
            }
            else if (currentCutscene.pointIfYellow[index] > 0) {
                scoreText.text = "+ " + scoreText.text;
                scoreTextColor = green;
            }
            else {
                scoreText.text = scoreText.text + " unaffected";
                scoreTextColor = yellow;
            }
        }
        else if (greenCard.clicked) {
            if (currentCutscene.pointIfGreen[index] < 0) {
                scoreText.text = "- " + scoreText.text;
                scoreTextColor = red;
            }
            else if (currentCutscene.pointIfGreen[index] > 0) {
                scoreText.text = "+ " + scoreText.text;
                scoreTextColor = green;
            }
            else {
                scoreText.text = scoreText.text + " unaffected";
                scoreTextColor = yellow;
            }
        }

        Color32 targetColor = scoreTextColor;
        targetColor.a = 0;
        while (scoreText.color.a > 0f) {
            scoreTextColor = Color32.Lerp(scoreTextColor, targetColor, 2f * Time.deltaTime); // Change alpha
            scoreText.color = scoreTextColor;

            scoreTextPos.y -= 0.1f * Time.deltaTime; // Change y position (edit the number to change falling speed)
            scoreText.GetComponent<RectTransform>().anchoredPosition = scoreTextPos;

            yield return null;
        }
        Debug.Log("Ending display score text: " + index);
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
    
    void UnclickAllCards() {
        redCard.clicked = false;
        yellowCard.clicked = false;
        greenCard.clicked = false;
    }
}