using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coins : MonoBehaviour
{
    private static int globalCoins;
    public TextMeshProUGUI coinsAmt;
    
    // Start is called before the first frame update
    void Start()
    {
        //get the players amount of coins
        globalCoins = PlayerPrefs.GetInt("Coins", 0);
        coinsAmt.text = globalCoins.ToString();
    }
    public static void AddCoins(int coins) => PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + coins);

    public static void SubtractCoins(int numCoins) => PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - numCoins);
}
