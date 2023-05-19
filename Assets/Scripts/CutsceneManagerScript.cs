using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneManagerScript : MonoBehaviour
{
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

    public Text displayText; // text next to ref

    // Start is called before the first frame update
    void Start()
    {
        chipSpeechBubble = chipCorner.GetComponent<CommentatorSpeechScript>();
        kennySpeechBubble = kennyKeeper.GetComponent<CommentatorSpeechScript>();
    }

    public void StartCutscene(IngameCutsceneScriptableObject cutscene) {
        currentCutscene = cutscene;
        StartCoroutine(StartCommentary());
    }


    IEnumerator StartCommentary()
    {
        yield return MoveToPositions();

        for (int i = 0; i < currentCutscene.dialogueLines.Length; i++) {
            if (currentCutscene.speakerOrder[i] == 1) {
                chipSpeechBubble.Show(currentCutscene.dialogueLines[i]);
                yield return new WaitUntil(() => Input.anyKeyDown);
                chipSpeechBubble.Hide();
            }
            else if (currentCutscene.speakerOrder[i] == 2) {
                kennySpeechBubble.Show(currentCutscene.dialogueLines[i]);
                yield return new WaitUntil(() => Input.anyKeyDown);
                kennySpeechBubble.Hide();
            }
        }

        // Chip and Kenny leave the screen, you can modify the target positions for exit
        yield return MoveToPositions(targetChipPosition + new Vector3(-5.6f,1.19f,0), targetKennyPosition + new Vector3(6.5f,1.19f,0));

        //
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
