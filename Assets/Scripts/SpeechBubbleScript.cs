using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubbleScript : MonoBehaviour
{
    public CardManagerScript redCard;
    public CardManagerScript yellowCard;
    public CardManagerScript greenCard;
    public Text displayText;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (redCard.rising) {
            displayText.text = "You're OUT!";
        }
        else if (yellowCard.rising) {
            displayText.text = "Penalty!";
        }
        else if (greenCard.rising) {
            displayText.text = "Nothing wrong here!";
        }
        else {
            displayText.text = "";
        }
    }
}
