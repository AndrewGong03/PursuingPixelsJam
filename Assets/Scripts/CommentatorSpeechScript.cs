using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommentatorSpeechScript : MonoBehaviour
{
    public Text DisplayText; // drag your DisplayText component here in the inspector

    public void Show(string text)
    {
        DisplayText.text = text;
    }
}


