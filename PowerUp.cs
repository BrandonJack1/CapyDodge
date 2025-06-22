using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;
using DentedPixel;
using Unity.VisualScripting;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private GameObject powerUp;
    [SerializeField] private GameObject propNet;
    public GameObject goldPropNet;
    [SerializeField] private GameObject bg;
    [SerializeField] private GameObject bar;
    
    public static bool powerUpPresent = false;
    public static bool active = false;
    public static bool activatePowerUp;
    private bool timerIsRunning = false;
    private bool animationPlaying = false;
    
    private float timeRemaining = 10;
    private float leftOffset;
    private float rightOffset;

    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip netSound;
    [SerializeField] private AudioClip netSpawn;
    
    [SerializeField] private Camera mainCamera;

    public Sprite normalNet;
    public Sprite goldenNet;
    public GameObject goldenLight;
    public GameObject smoke;
    public Sprite coin;
    public GameObject coinPannel;

    public static bool goldRushActive;

    public AudioSource mainMusic;
    public AudioSource goldenMusic;

    public Sprite acornSprite;
    
    // Start is called before the first frame update
    void Start()
    {
        leftOffset = mainCamera.pixelWidth / 40;
        rightOffset = mainCamera.pixelWidth / 10;
        
        //reset the statuses
        powerUpPresent = false;
        active = false;
        goldRushActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        //called when a net needs to be spawned in
        if (activatePowerUp)
        {
            activatePowerUp = false;
            SpawnPowerUp();
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
                SpawnPowerUp();
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

                if (goldRushActive)
                {
                    goldPropNet.SetActive(false);
                    goldRushActive = false;
                    coinPannel.SetActive(false);
                    goldenLight.SetActive(false);
                    StartCoroutine(UndoGoldenNetAnimation());

                }
                else
                {
                    propNet.SetActive(false);
                }
               
                timeRemaining = 10;
                
                //hide the loading bar
                bg.SetActive(false);
                Apple.appleCount = 0;
            }
        }
    }

    private void SpawnPowerUp()
    {
        powerUpPresent = true;
        
        //play the spawn sound
        source.PlayOneShot(netSpawn);
        
        //get a random position
        float pos = Random.Range(mainCamera.ScreenToWorldPoint(new Vector2(0 + leftOffset, 0)).x, mainCamera.ScreenToWorldPoint(new Vector2(Screen.width - rightOffset, 0)).x);

        
        GameObject net = Instantiate(powerUp, new Vector3(pos, 5.5f, 0), Quaternion.identity);
        if (!Apple.goldenActive)
        {
            //if the golden apple is not active then spawn the normal net
            net.GetComponent<SpriteRenderer>().sprite = normalNet;

        }
        else
        {
            coinPannel.SetActive(true);
            //spawn the light
            GameObject goldenLightClone = Instantiate(goldenLight, new Vector3(pos, 5.5f, 0), Quaternion.identity);
            goldenLightClone.transform.GetChild(0).GetComponent<Animator>().SetTrigger("LightShine");
            //play the animation
            net.GetComponent<SpriteRenderer>().sprite = goldenNet;
            //spawn the golden net
            Apple.goldenActive = false;
        }
    }

    static IEnumerator NetPoint(GameObject net)
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
            
           
            
            if (col.GetComponent<SpriteRenderer>().sprite.name == "GoldNet")
            {
                
                goldRushActive = true;
                GameObject shine = GameObject.Find("LightParent(Clone)").transform.GetChild(0).gameObject;
                //GameObject shine = transform.Find("LightParent/Light").gameObject;
                shine.GetComponent<Animator>().SetTrigger("ShineEnd");
                StartCoroutine(GoldenNetAnimation());
                goldPropNet.SetActive(true);
                goldenMusic.Play();
                mainMusic.Pause();
                return;
            }

            //activate the prop net
            propNet.SetActive(true);

            //wait until co routine returns
        
            
            //start timer
            timerIsRunning = true;
            bg.SetActive(true);
            
            //scale bar back to 1
            bar.transform.localScale = new Vector3(1, 1, 1);

         
            AnimateBar();
        }
    }

    IEnumerator GoldenNetAnimation()
    {
        GameObject[] acorns = GameObject.FindGameObjectsWithTag("Acorn");
        GameObject[] giantAcorns = GameObject.FindGameObjectsWithTag("GiantAcorn");
        GameObject[] goldenAcorns = GameObject.FindGameObjectsWithTag("GoldenAcorn");

        var allAcorns = acorns.Concat(giantAcorns).Concat(goldenAcorns).ToArray();
        
        //Go over each acorn and freeze them all
        foreach (GameObject acorn in allAcorns)
        {
            acorn.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            if (acorn.GetComponent<SpriteRenderer>().sprite.name == "Bomb-Acorn")
            {
                acorn.transform.GetChild(0).gameObject.SetActive(false);
                
            }
            
        }
        
        foreach (GameObject acorn in allAcorns)
        {
            GameObject smokeClone = Instantiate(smoke, acorn.transform.position, Quaternion.identity);
            acorn.GetComponent<SpriteRenderer>().sprite = coin;
            Destroy(smokeClone, 0.4f);
            yield return new WaitForSeconds(0.2f);
            
        }
        
        foreach (GameObject acorn in allAcorns)
        {
            acorn.tag = "Coin";
            acorn.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            float x = Random.Range(1f, 3f);
            float y = Random.Range(3f, 6f);
            acorn.GetComponent<Rigidbody2D>().AddForce(new Vector2(x,y),ForceMode2D.Impulse);
            
            
        }
        //start timer
        timerIsRunning = true;
        bg.SetActive(true);
            
        //scale bar back to 1
        bar.transform.localScale = new Vector3(1, 1, 1);

         
        AnimateBar();
    }

    IEnumerator UndoGoldenNetAnimation()
    {
        GameObject[] acorns = GameObject.FindGameObjectsWithTag("Coin");
        //Go over each acorn and freeze them all
        foreach (GameObject acorn in acorns)
        {
            acorn.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        }
        
        foreach (GameObject acorn in acorns)
        {
            GameObject smokeClone = Instantiate(smoke, acorn.transform.position, Quaternion.identity);
            Destroy(acorn);
            Destroy(smokeClone, 0.4f);
            Acorn.acornCount--;
            acorn.tag = "Acorn";
            yield return new WaitForSeconds(0f);

        }
        
        goldenMusic.Pause();
        mainMusic.Play();
    }

    private void AnimateBar()
    {
        LeanTween.scaleX(bar, 0, 10);
    }
}
