using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public static int score;

    public static int bonusCoins;
    // Start is called before the first frame update
    public void Start()
    {
        score = 0;
        bonusCoins = 0;
    }

  
    
    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
    }
}
