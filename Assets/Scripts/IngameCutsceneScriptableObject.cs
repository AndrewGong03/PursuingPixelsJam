using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IngameCutscene", menuName = "IngameCutscene")]
public class IngameCutsceneScriptableObject : ScriptableObject
{
    public string[] dialogueLines; // A string array containing the dialogue for the cutscene
    public int[] speakerOrder; // 1 for Chip Corner, 2 for Kenny Keeper - should be the same length as dialogueLines
    
    public bool hasChoices; // Whether the cutscene should prompt a card choice or not (some scenes like intros may not require any player action)
    public string infoText; // What should show up above the cards when the player is prompted to pick a card - leave blank if no choice

    //Index 0: What should show up in the ref's speech bubble when the player hovers over the red, yellow, and green cards
    //Index 1: What Chip Corner should say in reaction to the ref's selection of that card
    public string[] redCardText;
    public string[] yellowCardText;
    public string[] greenCardText;

    // Index 0: money value change if ref clicks red/yellow/green card 
    // Index 1: popularity value change 
    // Index 2: credibility value change 
    // Index 3: win value change 
    // still need to implement win score for team
    
    public int[] pointIfRed; 
    public int[] pointIfYellow; 
    public int[] pointIfGreen;

    // Which team this is: 0 for royal united, 1 for underdog, 2 for drug cartel 
    public int teamCode; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
