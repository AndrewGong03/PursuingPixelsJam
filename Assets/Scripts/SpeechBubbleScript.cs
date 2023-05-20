using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubbleScript : MonoBehaviour
{
    public SpriteRenderer speechBubbleSprite;
    public Text displayText;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        speechBubbleSprite.enabled = IngameCutsceneManagerScript.isCheckingCards;
    }
}
