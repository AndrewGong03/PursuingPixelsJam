using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommentatorSpeechScript : MonoBehaviour
{
    public Text DisplayText; // drag DisplayText component here in the inspector
    public GameObject speechBubble;

    public void Show(string text)
    {
        speechBubble.GetComponent<SpriteRenderer>().enabled = true;
        DisplayText.enabled = true;
        DisplayText.text = text;
    }

    public void Hide()
    {
        speechBubble.GetComponent<SpriteRenderer>().enabled = false;
        DisplayText.enabled = false;
    }
}


