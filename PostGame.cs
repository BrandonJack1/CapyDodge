using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms; 
using UnityEngine.Localization.Settings;

public class PostGame : MonoBehaviour
{
    private int numCoins;
    private int gameCoins;
    private int totalCoins;
    
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI thisGameCoins;
    [SerializeField] private TextMeshProUGUI highScoreText;
    
    [SerializeField] private GameObject thisGameCoinsContainer;
    [SerializeField] private GameObject retryBtn;
    
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip coin;
    
    private int lang;
    
    // Start is called before the first frame update
    void Start()
    {
        //set the players langauge prefernece
        lang = PlayerPrefs.GetInt("Lang Pref", 0);
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[lang];
        
        Time.timeScale = 1f;
        
        //Display the score
        string scoreLang = "";

        switch (lang)
        {
            //English
            case 0:
                scoreLang = "Score";
                break;
            //French
            case 1:
                scoreLang = "Score";
                break;
            //Spanish
            case 2:
                scoreLang = "Puntuacion";
                break;
            //German
            case 3:
                scoreLang = "Ergebnis";
                break;
            //Portagese
            case 4:
                scoreLang = "Pontuacao";
                break;
        }
        
        scoreText.text = scoreLang + ": " + Score.score;
        
        //Set default selection for TVOS
        if (Application.platform == RuntimePlatform.tvOS)
        {
            var eventSystem = EventSystem.current;
            eventSystem.SetSelectedGameObject(retryBtn, new BaseEventData(eventSystem));
        }
        
        //if its an iOS device report the High Score
       if (Application.platform == RuntimePlatform.IPhonePlayer)
       {
           Social.ReportScore(Score.score, "1", HighScoreCheck);
       }

       //if the score is better than high score, update it
        if (Score.score > PlayerPrefs.GetInt("High Score", 0))
        {
            PlayerPrefs.SetInt("High Score",Score.score);
        }

        string highscoreLang = "";
        
        switch (lang)
        {
            //English
            case 0:
                highscoreLang = "High Score: ";
                break;
            //French
            case 1:
                highscoreLang = "Meilleur Score: ";
                break;
            //Spanish
            case 2:
                highscoreLang = "Alta Puntuacion: ";
                break;
            //German
            case 3:
                highscoreLang = "Hohe Punktzahl: ";
                break;
            //Portagese
            case 4:
                highscoreLang = "Pontuacao maxima: ";
                break;
        }
        
        //display the high score
        highScoreText.text = highscoreLang + PlayerPrefs.GetInt("High Score", 0).ToString();

        //Get the players num of coins
        numCoins = PlayerPrefs.GetInt("Coins", 0);

        coinsText.text = numCoins.ToString();
        
        //calculate the coins
        gameCoins = Score.score / 10;
        
        //Display the coins earned from this game
        thisGameCoins.text = gameCoins.ToString();
        
        //calc total number of coins
        //St Patricks
        totalCoins = numCoins + gameCoins;
        PlayerPrefs.SetInt("Coins", totalCoins);
        StartCoroutine(UpdateTotalCoins());

    }

    IEnumerator UpdateTotalCoins()
    {
        yield return new WaitForSeconds(1.8f);
        source.PlayOneShot(coin);
        thisGameCoinsContainer.gameObject.SetActive(false);
        coinsText.text = totalCoins.ToString();
    }
    
    static void HighScoreCheck(bool result) 
    {
        if(result)
            Debug.Log("score submission successful");
        else
            Debug.Log("score submission failed");
    }
}
