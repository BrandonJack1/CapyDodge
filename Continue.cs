using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Continue : MonoBehaviour
{

    public GameObject continueScreen;
    public GameObject powerUp;
    public GameObject propNet;
    public GameObject bg;
    public GameObject bar;
    public GameObject[] acorns;
    public GameObject pauseButton;
    
    public bool timerIsRunning = false;
    public bool animationPlaying = false;
    public float timeRemaining = 10;
    public static bool adContinue = false;

    public GameObject freeContinue;
    public GameObject premiumContinueBtn;
    public TextMeshProUGUI title;
    public TextMeshProUGUI coinAmt;
    public GameObject coinParent;

    // Start is called before the first frame update
    void Start()
    {

        int lang = PlayerPrefs.GetInt("Lang Pref", 0);
        PowerUp.active = false;
        


        if (PlayerPrefs.GetString("ShowAds", "Yes") == "Yes")
        {
            
            freeContinue.SetActive(true);
            premiumContinueBtn.SetActive(false);

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
                //Spansih
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
        else
        {
            freeContinue.SetActive(false);
            premiumContinueBtn.SetActive(true);
            
            
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
                //Spansih
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
            //if the timer ran out, resset everything
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

        if (adContinue)
        {
            //allow the player to move
            PlayerMovement.playerMove = true;

            if (SlowTime.active)
            {
                SlowTime.timer.Start();
                GameOver.timer.Start();
            }

            //show pause button
            pauseButton.SetActive(true);
            
            //stop method from starting again
            adContinue = false;
            
            //set there is a power up present
            PowerUp.powerUpPresent = true;
            PowerUp.active = true;
            Time.timeScale = 1f;
            continueScreen.gameObject.SetActive(false);
            
            //dont show the continue screen again
            GameOver.continueUsed = true;
            
            propNet.SetActive(true);
            
            //start timer
            timerIsRunning = true;
            bg.SetActive(true);

            //scale bar back to 1
            bar.transform.localScale = new Vector3(1, 1, 1);
            animateBar();
        }
    }

    public void noThanks()
    {
        SceneManager.LoadScene(3);
        Time.timeScale = 1f;
    }

    public void animateBar()
    {
        LeanTween.scaleX(bar, 0, 10);
    }

    public void premiumContinue()
    {

        if (PlayerPrefs.GetInt("Coins") >= 100)
        {
            
            Coins.subtractCoins(100);
            adContinue = true;
        }
        
    }
}
