using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public static ScoreScript instance; 

    // 1) Popularity 
    public static int pop = 0;

    // 2) Credibility
    public static int cred = 0; 

    // 3) Win score for each team, only want to change score of team playing rn 
    // a) League "Royal United" 
    // b) Underdog 
    // c) Drug Cartel 
    public static int[] winScore = new int[3] {0,0,0};

    private void Awake()
    {
        instance = this; 
    }

    public void ChangePop(int popChange) 
    {
        pop += popChange; 
    }

    public void ChangeCred(int credChange) 
    {
        cred += credChange; 
    }

    public void ChangeWin(int winChange, int teamCode) 
    {
        winScore[teamCode] += winChange;
    }
}
