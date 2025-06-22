using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Continue : MonoBehaviour
{
    [SerializeField] private GameObject continueScreen;
    [SerializeField] private GameObject powerUp;
    [SerializeField] private GameObject propNet;
    [SerializeField] private GameObject bg;
    [SerializeField] private GameObject bar;
    [SerializeField] private GameObject[] acorns;
    [SerializeField] private GameObject pauseButton;

    [SerializeField] private AudioSource musicSource;
    
    private bool timerIsRunning = false;
    private bool animationPlaying = false;
    private float timeRemaining = 10;
    public static bool adContinue = false;

    [SerializeField] private GameObject freeContinue;
    [SerializeField] private GameObject premiumContinueBtn;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI coinAmt;
    [SerializeField] private GameObject coinParent;

    // Start is called before the first frame update
    void Start()
    {
        //set the language preference
        int lang = PlayerPrefs.GetInt("Lang Pref", 0);
        
        //set power up as not active
        PowerUp.active = false;
        
        //if the player hasnt bought the remove ads, offer to watch an ad
        if (PlayerPrefs.GetString("ShowAds", "Yes") == "Yes")
        {
            //show continue screen
            freeContinue.SetActive(true);
            premiumContinueBtn.SetActive(false);
            
            //display message based on language
            switch (lang)
            {
                //English
                case 0:
                    title.text = "Continue with a power up by watching an ad?";
                    break;
                //French
                case 1:
                    title.text = "Poursuivre la mise sous tension en regardant une publicite?";
                    break;
                //Spanish
                case 2:
                    title.text = "¿Continuar con un encendido viendo un anuncio?";
                    break;
                //German
                case 3:
                    title.text = "Mit einem Powerup weitermachen, indem man eine Werbung ansieht?";
                    break;
                //Portagese
                case 4:
                    title.text = "Continuar com um power up vendo um anuncio?";
                    break;
            }
            coinParent.SetActive(false);
        }
        //If the player bought to remove ads, set the text box to offer to continue for 100 coins
        else
        {
            //show continue screen
            freeContinue.SetActive(false);
            premiumContinueBtn.SetActive(true);
            
            //show message based on users language
            switch (lang)
            {
                //English
                case 0:
                    title.text = "Continue with a power up for 100 coins?";
                    break;
                //French
                case 1:
                    title.text = "Poursuivre avec une montee en puissance pour 100 pieces ?";
                    break;
                //Spanish
                case 2:
                    title.text = "¿Continuar con un power up por 100 monedas?";
                    break;
                //German
                case 3:
                    title.text = "Weiter mit einem Power-Up für 100 Munzen?";
                    break;
                //Portagese
                case 4:
                    title.text = "Continuar com um power up por 100 moedas?";
                    break;
            }
            coinParent.SetActive(true);
            coinAmt.text = PlayerPrefs.GetInt("Coins").ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if the timer is running from the powerup
        if (timerIsRunning)
        {
            //if the time is greater than zero, subtract the time
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            //if the timer ran out, reset everything
            else
            {
                timerIsRunning = false;
                PowerUp.active = false;
                PowerUp.powerUpPresent = false;
                propNet.SetActive(false);
                timeRemaining = 10;
                bg.SetActive(false);
                Apple.appleCount = 0;
            }
        }
        
        //if the player chose to continue by watching ad
        if (adContinue)
        {
            //remove the net that is already present in the game area 
            GameObject net = GameObject.Find("net (Power Up)(Clone)");
            Destroy(net);
            
            //allow the player to move
            PlayerMovement.playerMove = true;

            if (SlowTime.active)
            {
                GameOver.timer.Start();
            }

            if (SlowTime.inArea == false)
            {
                SlowTime.timer.Start();
            }
            
            //resume the music
            musicSource.UnPause();
            
            //show pause button
            pauseButton.SetActive(true);
            
            //stop method from starting again
            adContinue = false;
            
            //set there is a power up present
            PowerUp.powerUpPresent = true;
            Apple.goldenActive = false;
            PowerUp.active = true;
            Time.timeScale = 1f;
            continueScreen.gameObject.SetActive(false);
            
            //dont show the continue screen again
            GameOver.continueUsed = true;
            
            propNet.SetActive(true);
            
            //start timer
            timerIsRunning = true;
            bg.SetActive(true);

            //scale net progression bar back to 1
            bar.transform.localScale = new Vector3(1, 1, 1);
            AnimateBar();
        }
    }

    public void NoThanks()
    {
        //if the player does not want to continue, show game over screen
        SceneManager.LoadScene(3);
        Time.timeScale = 1f;
    }

    public void AnimateBar()
    {
        LeanTween.scaleX(bar, 0, 10);
    }

    public void PremiumContinue()
    {
        //if the player continues with premium, subtract coins
        if (PlayerPrefs.GetInt("Coins") >= 100)
        {
            Coins.SubtractCoins(100);
            adContinue = true;
        }
    }
}
