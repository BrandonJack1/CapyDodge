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
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject floatingScore;
    [SerializeField] private GameObject continueScreen;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject continueBtn;
    [SerializeField] private GameObject comboText;
    [SerializeField] private GameObject previousComboText;
    [SerializeField] private GameObject empty;
    [SerializeField] private GameObject slowRemaining;
    
    private const float SECONDS_TO_DESTROY = 0.8f;
    public static bool continueUsed = false;

    public AudioSource musicSource;
    [FormerlySerializedAs("Source")] public AudioSource source;
    [SerializeField] private AudioClip acornCollect;
    [SerializeField] private Canvas canvas;

    [SerializeField] private AudioClip acornCollect1;
    [SerializeField] private AudioClip acornCollect2;
    [SerializeField] private AudioClip acornCollect3;
    [SerializeField] private AudioClip acornCollect4;
    [SerializeField] private AudioClip acornCollect5;
    [SerializeField] private AudioClip acornCollect6;
    [SerializeField] private AudioClip acornCollect7;
    [SerializeField] private AudioClip acornCollect8;
    [SerializeField] private AudioClip acornCollect9;
    [SerializeField] private AudioClip goldenAcornCollect;
    [SerializeField] private AudioClip acronHit;
    [SerializeField] private AudioClip starSound;
    [SerializeField] private AudioClip coins;
    [SerializeField] private AudioClip clock;
    
    private int activeScene;
    
    public static Stopwatch timer;
    public static bool soundActive;
    private bool clockSoundActive = false;
    public static bool toggleSound;
    
    void Start()
    {
        //used to offer the player a chance to continue
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
        
        //Resume background music if the player continues 
        if (soundActive && toggleSound)
        {
            toggleSound = false;
            source.UnPause();
        }
        
        //pause the background music if the player loses
        if (soundActive == false && toggleSound)
        {
            toggleSound = false;
            source.Pause();
        }
        
        //get the amount of seconds remainng for the power up
        slowRemaining.GetComponent<TextMeshProUGUI>().text = (10 - ts.Seconds).ToString();
        
        //play a countdown sound for the clock power up
        if (ts.Seconds >= 5 && clockSoundActive == false)
        {
            slowRemaining.SetActive(true);
            clockSoundActive = true;
            source.PlayOneShot(clock);
        }
        
        //if the countdown for the clock power up runs out, reset everything
        if (ts.Seconds >= 10)
        {
            timer.Stop();
            timer.Reset();
            Acorn.slowActive = false;
            clockSoundActive = false;
            
            //make acorns go back to normal speed
            var acorns = GameObject.FindGameObjectsWithTag("Acorn");
            foreach (var obj in acorns)
            {
                obj.GetComponent<Rigidbody2D>().velocity /= new UnityEngine.Vector2(0.1f,0.1f);
                obj.GetComponent<Rigidbody2D>().gravityScale =  1f;
            }
            
            //make golden acorns go back to normal speed
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
        if (col.CompareTag("Acorn") || col.CompareTag(("GiantAcorn")) || col.CompareTag("GoldenAcorn"))
        {
            //if the power up isnt active then end the game
            if (!PowerUp.active)
            {
                col.gameObject.tag = "Untagged";
                
                //stop sound and background music
                source.Stop();
                musicSource.Pause();

                //if the player already used continue then go to game over
                if (continueUsed)
                {
                    StartCoroutine(GameOverAnimation(false,col.gameObject));
                }
                //otherwise offer the continue
                else
                {
                    StartCoroutine(GameOverAnimation(true, col.gameObject));
                }
            }
            //if the power up is active
            else
            {   
                //get rid of acorn
                Destroy(col.gameObject);
                
                //default points for normal acorn is 50
                float points = 50;
                
                if (col.CompareTag("GiantAcorn"))
                {
                    Acorn.giantActive = false;
                    points = 100;
                }
                else if (col.CompareTag("GoldenAcorn"))
                {
                    Acorn.goldenActive = false;
                    points = 200;
                }
                
                GameObject prefab = Instantiate(floatingScore, transform.position, Quaternion.identity);

                //if the player is currently in the middle of a combo when collecting this acorn
                if (Combo.comboActive)
                {   
                    //increase combo multiplier value
                    Combo.multiplier += 0.25f;
                    
                    //add default acorn value * multiplier to the score 
                    Score.score += (int)(points * Combo.multiplier);
                    
                    //spawn the acorn score value
                    prefab.GetComponentInChildren<TextMesh>().text = ((int)(points * Combo.multiplier)).ToString();
                    prefab.GetComponentInChildren<TextMesh>().color = ComboColor(Combo.multiplier, false);
                    Combo.comboTimer = 0.8f;
                    
                    //play the sound for the combo
                    PlayComboSound();

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
                    firstChild.GameObject().GetComponent<TextMeshProUGUI>().color = ComboColor(Combo.multiplier, true);
                    comboUI.GetComponent<TextMeshProUGUI>().color = Color.white;
                    
                    //set this text as previous so it can be deleted if the player performs another combo (to prevent overlap)
                    previousComboText = comboUI;
                    float rndRotate = Random.Range(-10, 10);
                    
                    //randomly rotate the combo ui in different directions
                    comboUI.transform.Rotate(0, 0, rndRotate, Space.Self);
                    
                    //destroy it after 1 second
                    Destroy(comboUI, 1f);
                    
                    //instantiate the combo with the amount and play the animation
                    comboUI.GetComponent<TextMeshProUGUI>().text = "x" + Combo.multiplier;
                    firstChild.GameObject().GetComponent<TextMeshProUGUI>().text =
                        comboUI.GetComponent<TextMeshProUGUI>().text;
                    
                    comboUI.GetComponent<Animator>().SetTrigger("ComboTrigger");
                }
                else
                {
                    //set the combo as active 
                    Combo.comboActive = true;
                    
                    //increase score
                    Score.score += (int)points;
                    
                    //spawn a floating number
                    prefab.GetComponentInChildren<TextMesh>().text = points.ToString();

                    source.PlayOneShot(col.CompareTag("GoldenAcorn") ? goldenAcornCollect : acornCollect);
                }
                Destroy(prefab, SECONDS_TO_DESTROY);
            }
        }
        else if (col.CompareTag("Star"))
        {
            //destroy the star
            Destroy(col.gameObject);
            
            //add 150 to the score
            Score.score += 150;
            
            //instantiate floating score
            GameObject prefab = Instantiate(floatingScore, transform.position, Quaternion.identity);
            prefab.GetComponentInChildren<TextMesh>().text = "150";
            
            //power up sound
            source.PlayOneShot(starSound);
            
            //destroy the floating score
            Destroy(prefab, SECONDS_TO_DESTROY);
            
            Apple.starActive = false;
        }
        else if (col.CompareTag("Clock"))
        {
            SlowTime.inArea = false;
            SlowTime.active = true;
            
            //find all active acorns in play area
            var acorns = GameObject.FindGameObjectsWithTag("Acorn");
            var goldenAcorns = GameObject.FindGameObjectsWithTag("GoldenAcorn");
            
            Acorn.slowActive = true;
            source.PlayOneShot(starSound);
            
            //destroy clock
            Destroy(col.gameObject);
            
            //slowdown all acorns in the game area
            foreach (var obj in acorns)
            {
                obj.GetComponent<Rigidbody2D>().velocity *= new UnityEngine.Vector2(0.1f,0.1f);
                obj.GetComponent<Rigidbody2D>().gravityScale =  0.01f;
            }
            
            //slowdown all golden acorns in the game area
            foreach (var obj in goldenAcorns)
            {
                obj.GetComponent<Rigidbody2D>().velocity *= new UnityEngine.Vector2(0.1f,0.1f);
                obj.GetComponent<Rigidbody2D>().gravityScale =  0.01f;
            }
            
            SlowTime.offset += 15;
            timer.Start();
        }
    }

    private IEnumerator GameOverAnimation(bool offerContinue, GameObject col)
    {
        //hide the pause button
        pauseButton.SetActive(false);
        Time.timeScale = 0f;
        
        //stop the slow mo
        SlowTime.timer.Stop();
        timer.Stop();

        //dont allow the player to move
        PlayerMovement.playerMove = false;
        source.PlayOneShot(acronHit);
        
        //delay time for sound to play
        yield return new WaitForSecondsRealtime(2f);
        
        //hide acorn
        col.SetActive(false);
        
        //if the user is eligble for a continue
        if (offerContinue)
        {
            //freeze time
            Time.timeScale = 0f;
            
            //show continue screen
            continueScreen.SetActive(true);
            
            //Apple TV set selection
            if (Application.platform == RuntimePlatform.tvOS)
            {
                var eventSystem = EventSystem.current;
                eventSystem.SetSelectedGameObject(continueBtn, new BaseEventData(eventSystem));
            }
            
        }
        //if player already used continue then bring them to game over screen
        else
        {
            //freeze time
            Time.timeScale = 0f;
            
            //load game over scene
            SceneManager.LoadScene(3);
            
        }
    }

    private void PlayComboSound()
    {   
        //play pitched sound depending on the multiplier active
        switch (Combo.multiplier)
        {
            case 1.25f:
                source.PlayOneShot(acornCollect1);
                break;
            case 1.5f:
                source.PlayOneShot(acornCollect2);
                break;
            case 1.75f:
                source.PlayOneShot(acornCollect3);
                break;
            case 2f:
                source.PlayOneShot(acornCollect4);
                break;
            case 2.25f:
                source.PlayOneShot(acornCollect5);
                break;
            case 2.5f:
                source.PlayOneShot(acornCollect6);
                break;
            case 2.75f:
                source.PlayOneShot(acornCollect7);
                break;
            case 3f:
                source.PlayOneShot(acornCollect8);
                break;
            case 3.25f:
                source.PlayOneShot(acornCollect9);
                break;
            default:
                source.PlayOneShot(acornCollect9);
                break;
        }
    }

    private Color ComboColor(float multiplier, bool largeText)
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
        
        //get color for multiplier depending on the multiplier value
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
