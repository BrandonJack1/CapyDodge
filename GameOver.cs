using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField] private GameObject floatingScore;
    public GameObject continueScreen;
    public GameObject pauseButton;
    
    public float secondsToDestroy = 0.8f;
    public static bool continueUsed = false;
    
    public AudioSource Source;
    public  AudioClip acornCollect;
    public AudioClip goldenAcornCollect;
    public AudioClip acronHit;
    public AudioClip starSound;
    public AudioClip coins;
    
    public AudioClip clock;
    public bool clockSoundActive = false;

    public int activeScene;

    public static Stopwatch timer;

    public GameObject slowRemaining;

    public static bool soundActive;
    public static bool toggleSound;
    
    public GameObject continueBtn;

    void Start()
    {
        continueUsed = false;
        pauseButton.SetActive(true);
        activeScene = SceneManager.GetActiveScene().buildIndex;
        
        //create stopwatch for the slow time 
        timer = new Stopwatch();
        timer.Stop(); 
        
        //hide the slowdown countdownn timer
        slowRemaining.SetActive(false);

        soundActive = true;
        toggleSound = true;

    }

    private void Update()
    {
        TimeSpan ts = timer.Elapsed;

        if (soundActive && toggleSound)
        {
            toggleSound = false;
            Source.UnPause();
        }
        if (soundActive == false && toggleSound)
        {
            toggleSound = false;
            Source.Pause();
        }
        
        //get the amount of seconds remainng for the power up
        slowRemaining.GetComponent<TextMeshProUGUI>().text = (10 - ts.Seconds).ToString();

        if (ts.Seconds >= 5 && clockSoundActive == false)
        {
            slowRemaining.SetActive(true);
            clockSoundActive = true;
            Source.PlayOneShot(clock);
        }
    
        if (ts.Seconds >= 10)
        {
            timer.Stop();
            timer.Reset();
            
            Acorn.slowActive = false;

            clockSoundActive = false;

            var acorns = GameObject.FindGameObjectsWithTag("Acorn");
            foreach (var obj in acorns)
            {
                obj.GetComponent<Rigidbody2D>().velocity /= new UnityEngine.Vector2(0.1f,0.1f);
                obj.GetComponent<Rigidbody2D>().gravityScale =  1f;
            }

            var goldenAcorn = GameObject.FindGameObjectsWithTag("GoldenAcorn");
            foreach (var obj in goldenAcorn)
            {
                obj.GetComponent<Rigidbody2D>().velocity /= new UnityEngine.Vector2(0.1f,0.1f);
                obj.GetComponent<Rigidbody2D>().gravityScale =  1f;
            }
            
            SlowTime.timer.Start();

            SlowTime.active = false;
            
            //hide the countdown timer when the power up is done
            slowRemaining.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Acorn"))
        {
            //if the power up isnt active then end the game
            if (!PowerUp.active)
            {
                col.gameObject.tag = "Untagged";
                
                Source.Stop();

                //if the continue is already used or the user removed the ads, skip to game over

                if (continueUsed)
                {
                    StartCoroutine(gameOverAnimation(false,col.gameObject));
                }
                //offer the continue
                else
                {
                    StartCoroutine(gameOverAnimation(true, col.gameObject));
                }
            }
            //if the power up is active
            else
            {   
                //get rid of acorn
                Destroy(col.gameObject);
                
                //increase score
                Score.score += 50;
                //spawn a floating number

                if (activeScene == 2)
                {
                    GameObject prefab = Instantiate(floatingScore, transform.position, Quaternion.identity);
                    prefab.GetComponentInChildren<TextMesh>().text = "50";
                    Destroy(prefab, secondsToDestroy);
                }
                Source.PlayOneShot(acornCollect);
            }

        }
        else if (col.CompareTag("Star"))
        {
            Destroy(col.gameObject);
            Score.score += 100;

            GameObject prefab = Instantiate(floatingScore, transform.position, Quaternion.identity);
            prefab.GetComponentInChildren<TextMesh>().text = "100";
            
            //power up sound
            Source.PlayOneShot(starSound);
            
            Destroy(prefab, secondsToDestroy);

            Apple.starActive = false;

        }
        else if (col.CompareTag("Clock"))
        {

            SlowTime.active = true;
            var acorns = GameObject.FindGameObjectsWithTag("Acorn");
            var goldenAcorns = GameObject.FindGameObjectsWithTag("GoldenAcorn");
            
            //slowRemaining.SetActive(true);
            
            Acorn.slowActive = true;
            
            Source.PlayOneShot(starSound);
            
            Destroy(col.gameObject);
            
            foreach (var obj in acorns)
            {
                //obj.GetComponent<Rigidbody2D>().velocity *= new UnityEngine.Vector2(0.1f,0.1f);
                //obj.GetComponent<Rigidbody2D>().gravityScale =  0.01f;
                
                obj.GetComponent<Rigidbody2D>().velocity *= new UnityEngine.Vector2(0.1f,0.1f);
                obj.GetComponent<Rigidbody2D>().gravityScale =  0.01f;
            }

            foreach (var obj in goldenAcorns)
            {
                obj.GetComponent<Rigidbody2D>().velocity *= new UnityEngine.Vector2(0.1f,0.1f);
                obj.GetComponent<Rigidbody2D>().gravityScale =  0.01f;
            }

            SlowTime.offset += 5;
            timer.Start();
        }
        //if the tag of the object is GoldenAcorn
        if (col.CompareTag("GoldenAcorn"))
        {
            Acorn.goldenActive = false;
            //if golden acorn collides with player and power up is not active, end the game
            if (PowerUp.active == false)
            {
                col.gameObject.tag = "Untagged";
                
                Source.Stop();

                //if the continue is already used or the user removed the ads, skip to game over

                if (continueUsed)
                {
                    StartCoroutine(gameOverAnimation(false,col.gameObject));
                }
                //offer the continue
                else
                {
                    StartCoroutine(gameOverAnimation(true, col.gameObject));
                }
                
            }
            //if power up is active
            else
            {
                //get rid of acorn
                Destroy(col.gameObject);
                
                //increase score
                Score.score += 100;
                //spawn a floating number

                if (activeScene == 2)
                {
                    GameObject prefab = Instantiate(floatingScore, transform.position, Quaternion.identity);
                    prefab.GetComponentInChildren<TextMesh>().text = "100";
                    Destroy(prefab, secondsToDestroy);
                }
                Source.PlayOneShot(goldenAcornCollect);
                
            }
        }


        //ST PATRICKS
        /*else if (col.CompareTag("Clover"))
        {

            int rnd = Random.Range(0, 4);
            Destroy(col.gameObject);
            GameObject prefab = Instantiate(floatingScore, transform.position, Quaternion.identity);

            if (rnd == 0)
            {
                prefab.GetComponentInChildren<TextMesh>().text = "2 Coins";
                Score.bonusCoins += 2;
            }
            else if (rnd == 1 || rnd == 2)
            {
                prefab.GetComponentInChildren<TextMesh>().text = "5 Coins";
                Score.bonusCoins += 5;
            }
            else if (rnd == 3 || rnd == 4)
            {
                prefab.GetComponentInChildren<TextMesh>().text = "10 Coins";
                Score.bonusCoins += 10;
            }
            
            
            //power up sound
            Source.PlayOneShot(coins);
            
            Destroy(prefab, secondsToDestroy);

            //Apple.cloverActive = false;

        }*/
        
        
    }
    
    IEnumerator gameOverAnimation(bool offerContinue, GameObject col)
    {
        //hide the pause button
        pauseButton.SetActive(false);
        Time.timeScale = 0f;
        
        //stop the slow mo
        SlowTime.timer.Stop();
        timer.Stop();

        //dont allow the player to move
        PlayerMovement.playerMove = false;
        Source.PlayOneShot(acronHit);
        
        //delay time for sound to play
        yield return new WaitForSecondsRealtime(2f);
        
        //hide acorn
        col.SetActive(false);
        
        //if the user is eligble for a continue
        if (offerContinue)
        {
            Time.timeScale = 0f;
            continueScreen.SetActive(true);

            if (Application.platform == RuntimePlatform.tvOS)
            {
                var eventSystem = EventSystem.current;
                eventSystem.SetSelectedGameObject(continueBtn, new BaseEventData(eventSystem));
            }
            
        }
        else
        {
            Time.timeScale = 0f;
            SceneManager.LoadScene(3);
            
        }
    }
}
