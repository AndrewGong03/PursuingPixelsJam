using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public static ScoreScript instance; 

    // 1) Money stats
    public Text moneyText;
    public static int money = 0; 

    // 2) Popularity 
    public static int pop = 0;

    // 3) Win score for each team, only want to change score of team playing rn 
    // a) League "Royal United" 
    // b) Underdog 
    // c) Drug Cartel 


    // 4) Credibility with each team, just sum of "win scores" for that team 
    // a) League "Royal United" 
    // b) Underdog 
    // c) Drug Cartel 


    private void Awake()
    {
        instance = this; 
    }

    void Start()
    {
        moneyText.text = money.ToString();
    }

    public void ChangeMoney(int moneyChange) 
    {
        money += moneyChange; 
        moneyText.text = money.ToString(); 
    }

    public void ChangePop(int popChange) 
    {
        pop += popChange; 
    }
}
