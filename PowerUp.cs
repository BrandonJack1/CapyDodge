using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;
using DentedPixel;
using Unity.VisualScripting;

public class PowerUp : MonoBehaviour
{
    public GameObject powerUp;
    public GameObject propNet;
    public GameObject bg;
    public GameObject bar;
    

    public static bool powerUpPresent = false;

    public static bool active = false;
 
    public bool timerIsRunning = false;
    public bool animationPlaying = false;
    public float timeRemaining = 10;

    public AudioSource source;
    public AudioClip netSound;
    public AudioClip netSpawn;

    public GameObject netArrow;
    
    public AnimationClip clip;

    public float leftOffset;

    public float rightOffset;
    public float screenLength;

    public Camera mainCamera;


    public static bool activatePowerUp;
    // Start is called before the first frame update
    void Start()
    {
        //myAnimation = GetComponent<Animation>();
        
        leftOffset = mainCamera.pixelWidth / 40;
        rightOffset = mainCamera.pixelWidth / 10;
        
        //reset the statuses
        powerUpPresent = false;
        active = false;

    }

    // Update is called once per frame
    void Update()
    {

        if (activatePowerUp)
        {
            activatePowerUp = false;
            spawnPowerUp();
        }
        
        //if the player collects 10 apples
        if (Apple.appleCount >= 10)
        {
            //set apple count to zero
            Apple.appleCount = 0;
            
            //if there is a power up present dont spawn another one
            if (powerUpPresent != true)
            {
                powerUpPresent = true;
                spawnPowerUp();
            }

        }
        
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
                
                //reset the statuses
                active = false;
                powerUpPresent = false;
                
                propNet.SetActive(false);
                timeRemaining = 10;
                
                //hide the loading bar
                bg.SetActive(false);
                Apple.appleCount = 0;

                //TIMED GAME MODE
                Timer.netSecondsCount = 25f;
            }
        }
    }

    public void spawnPowerUp()
    {

        powerUpPresent = true;
        //play the spawn sound
        source.PlayOneShot(netSpawn);
        
        //get a random position
        float pos = Random.Range(mainCamera.ScreenToWorldPoint(new Vector2(0 + leftOffset, 0)).x, mainCamera.ScreenToWorldPoint(new Vector2(Screen.width - rightOffset, 0)).x);
        
        //spawn the item
        GameObject net = Object.Instantiate<GameObject>(powerUp, new Vector3(pos, 5.5f, 0), Quaternion.identity);
        
        //if the player doesnt pick up the net for an amount of time, point an arrow to it
        StartCoroutine(netPoint(net));

    }
    
    IEnumerator netPoint(GameObject net)
    {
        //enable the arrow pointing to the net
        yield return new WaitForSeconds(5);

        if (active == false)
        {
            net.transform.GetChild(0).GameObject().SetActive(true);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        
        //if the player runs into a net
        if (col.CompareTag("Net"))
        {
            //play the pickup sound
            source.PlayOneShot(netSound);
            
            //destroy it and set the power up status to active
            Destroy(col.gameObject);
            active = true;
            
            //activate the prop net
            propNet.SetActive(true);
            //start timer
            timerIsRunning = true;
            bg.SetActive(true);
            
            //scale bar back to 1
            bar.transform.localScale = new Vector3(1, 1, 1);
            animateBar();

        }
    }

    public void animateBar()
    {
        
        LeanTween.scaleX(bar, 0, 10);
    }
}
