using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
//using UnityEditor.Localization.Plugins.XLIFF.V20;
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

    public AudioSource musicSource;
    public AudioSource Source;
    public AudioClip acornCollect;
    public Canvas canvas;

    public AudioClip acornCollect1;
    public AudioClip acornCollect2;
    public AudioClip acornCollect3;
    public AudioClip acornCollect4;
    public AudioClip acornCollect5;
    public AudioClip acornCollect6;
    public AudioClip acornCollect7;
    public AudioClip acornCollect8;
    public AudioClip acornCollect9;
    
    
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

    public GameObject comboText;

    public GameObject previousComboText;

    public GameObject empty;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
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
        if (col.CompareTag("Acorn") || col.CompareTag(("GiantAcorn")))
        {
            //if the power up isnt active then end the game
            if (!PowerUp.active)
            {
                col.gameObject.tag = "Untagged";
                
                Source.Stop();
                musicSource.Pause();

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

                if (col.CompareTag("GiantAcorn"))
                {
                    Acorn.giantActive = false;
                }
                
                //Game mode 1
                if (activeScene == 2)
                {
                    GameObject prefab = Instantiate(floatingScore, transform.position, Quaternion.identity);

                    if (Combo.comboActive)
                    {   
                        //increase combo multiplier value
                        Combo.multiplier += 0.25f;
                        
                        //add default acorn value * multiplier to the score 
                        Score.score += (int)(50 * Combo.multiplier);
                        
                        //spawn the acorn score value
                        prefab.GetComponentInChildren<TextMesh>().text = ((int)(50 * Combo.multiplier)).ToString();
                        prefab.GetComponentInChildren<TextMesh>().color = comboColor(Combo.multiplier, false);
                        Combo.comboTimer = 0.8f;
                        
                        //play the sound for the combo
                        playComboSound();

                        //delete the previous combo text to prevent overlap
                        if (previousComboText != null)
                        {
                            Destroy(previousComboText);
                        }
                        
                        //spawn combo value in center of the screen
                        GameObject comboUI = Instantiate(comboText, new Vector3(0.7f, -23f,-0.1f), quaternion.identity );
                        //set an empty parent so animation works
                        comboUI.transform.SetParent(empty.transform, false);
                        
                        //set the color of the multiplier text
                        
                        Transform firstChild = comboUI.transform.GetChild(0);
                        firstChild.GameObject().GetComponent<TextMeshProUGUI>().color = comboColor(Combo.multiplier, true);
                        comboUI.GetComponent<TextMeshProUGUI>().color = Color.white;

                        previousComboText = comboUI;
                        float rndRotate = Random.Range(-10, 10);
                        
                        
                        comboUI.transform.Rotate(0, 0, rndRotate, Space.Self);
                        Destroy(comboUI, 1f);


                        comboUI.GetComponent<TextMeshProUGUI>().text = "x" + Combo.multiplier;
                        firstChild.GameObject().GetComponent<TextMeshProUGUI>().text =
                            comboUI.GetComponent<TextMeshProUGUI>().text;
                        comboUI.GetComponent<Animator>().SetTrigger("ComboTrigger");
                        
                        //instantiate combo UI
                        //random rotation

                    }
                    else
                    {
                        Combo.comboActive = true;
                        //increase score
                        Score.score += 50;
                        //spawn a floating number
                        prefab.GetComponentInChildren<TextMesh>().text = "50";
                        Source.PlayOneShot(acornCollect);
                    }
                    Destroy(prefab, secondsToDestroy);
                }
                
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

            SlowTime.inArea = false;
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

            SlowTime.offset += 15;
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
                musicSource.Pause();

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

                if (activeScene == 2)
                {
                    GameObject prefab = Instantiate(floatingScore, transform.position, Quaternion.identity);

                    if (Combo.comboActive)
                    {
                        Combo.multiplier += 0.25f;
                        Score.score += (int)(200 * Combo.multiplier);
                        prefab.GetComponentInChildren<TextMesh>().text = ((int)(200 * Combo.multiplier)).ToString();
                        prefab.GetComponentInChildren<TextMesh>().color = comboColor(Combo.multiplier, false);
                        Combo.comboTimer = 0.8f;
                        playComboSound();
                        
                        //delete the previous one to prevent overlap
                        if (previousComboText != null)
                        {
                            Destroy(previousComboText);
                        }

                        GameObject comboUI = Instantiate(comboText, new Vector3(0.7f, -23f,-0.1f), quaternion.identity );
                        comboUI.transform.SetParent(empty.transform, false);
                        comboUI.GetComponent<TextMeshProUGUI>().color = comboColor(Combo.multiplier, true);

                        previousComboText = comboUI;
                        float rndRotate = Random.Range(-10, 10);
                        
                        Transform firstChild = comboUI.transform.GetChild(0);
                        firstChild.GameObject().GetComponent<TextMeshProUGUI>().color = comboColor(Combo.multiplier, true);
                        comboUI.transform.Rotate(0, 0, rndRotate, Space.Self);
                        Destroy(comboUI, 0.8f);
                        
                        comboUI.GetComponent<TextMeshProUGUI>().text = "x" + Combo.multiplier;
                        firstChild.GameObject().GetComponent<TextMeshProUGUI>().text =
                            comboUI.GetComponent<TextMeshProUGUI>().text;
                        
                        comboUI.GetComponent<TextMeshProUGUI>().color = Color.white;
                        comboUI.GetComponent<Animator>().SetTrigger("ComboTrigger");
                        //instantiate combo UI
                        //random rotation

                    }
                    else
                    {
                        Combo.comboActive = true;
                        //increase score
                        Score.score += 200;
                        //spawn a floating number
                        prefab.GetComponentInChildren<TextMesh>().text = "200";
                        Source.PlayOneShot(acornCollect);
                    }
                    Destroy(prefab, secondsToDestroy);
                }
                
                Source.PlayOneShot(goldenAcornCollect);
                
            }
        }
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

    public void playComboSound()
    {
        switch (Combo.multiplier)
        {
            case 1.25f:
                Source.PlayOneShot(acornCollect1);
                break;
            case 1.5f:
                Source.PlayOneShot(acornCollect2);
                break;
            case 1.75f:
                Source.PlayOneShot(acornCollect3);
                break;
            case 2f:
                Source.PlayOneShot(acornCollect4);
                break;
            case 2.25f:
                Source.PlayOneShot(acornCollect5);
                break;
            case 2.5f:
                Source.PlayOneShot(acornCollect6);
                break;
            case 2.75f:
                Source.PlayOneShot(acornCollect7);
                break;
            case 3f:
                Source.PlayOneShot(acornCollect8);
                break;
            case 3.25f:
                Source.PlayOneShot(acornCollect9);
                break;
            default:
                Source.PlayOneShot(acornCollect9);
                break;
        }
    }
    
    public Color comboColor(float multiplier, bool largeText)
    {

        byte transparency;
        if (largeText)
        {
            transparency = 230;
        }
        else
        {
            transparency = 255;
        }

        Color returnColor;
        switch (multiplier)
        {
            case 1.25f:
                returnColor = new Color32(255,60,60,transparency);
                break;
            case 1.5f:
                returnColor = new Color32(0,209,18,transparency);
                break;
            case 1.75f:
                returnColor = new Color32(82,38,255,transparency);
                break;
            case 2f:
                returnColor = new Color32(38,140,255,transparency);
                break;
            case 2.25f:
                returnColor = new Color32(255,60,60,transparency);
                break;
            case 2.5f:
                returnColor = new Color32(0,209,18,transparency);
                break;
            case 2.75f:
                returnColor = new Color32(82,38,255,transparency);
                break;
            case 3f:
                returnColor = new Color32(38,140,255,transparency);
                break;
            case 3.25f:
                returnColor = new Color32(255,60,60,transparency);
                break;
            default:
                returnColor = new Color32(0,209,18,200);
                break;
        }

        return returnColor;

    }
}
