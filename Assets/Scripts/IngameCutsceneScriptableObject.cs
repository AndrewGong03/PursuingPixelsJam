using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IngameCutscene", menuName = "IngameCutscene")]
public class IngameCutsceneScriptableObject : ScriptableObject
{
    public string[] dialogueLines;
    public int[] speakerOrder;
    public string redCardText;
    public string yellowCardText;
    public string greenCardText;
    public string infoText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
