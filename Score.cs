using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    public static int score;
    
    // Start is called before the first frame update
    public void Start()
    {
        score = 0;
    }
    
    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
    }
}
