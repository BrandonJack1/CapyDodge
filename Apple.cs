using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;
using UnityEngine.Localization.Settings;


public class Apple : MonoBehaviour
{
    public bool spawnRight = false;
    public GameObject apple;
    public GameObject star;
    public static int appleCount = 0;
    [SerializeField] private GameObject floatingScore;
    public float secondsToDestroy = 1f;
    public int numApplePresent;

    public AudioSource Source;
    public AudioClip collectSound;
    public AudioClip leaves;
   

    public float leftOffset;
    public float rightOffset;
    public float middleOffset;

    public TextMeshProUGUI appleCnt;
    public GameObject appleCountBox;

    public Camera mainCamera;

    public bool appleCollision = false;
    public static bool starActive = false;
    public GameObject particle;

    public int lang;

    //ST PATRICKS
    //public GameObject clover;
    //public static bool cloverActive;
    
    
    void Start()
    {
      
        //Get a random position to spawn
        float pos = rndPos(false, true);
        
        //spawn an apple
        GameObject appleClone = Instantiate<GameObject>(apple, new Vector3(pos, -4.2f, 0), Quaternion.identity);
        
        //reset apple counter
        appleCount = 0;
        
        //determine screen offsets
        leftOffset = Camera.main.pixelWidth / 30;
        rightOffset = Camera.main.pixelWidth / 20;
        middleOffset = Camera.main.pixelWidth / 10;

        starActive = false;
        
        //ST PATRICKS
        //cloverActive = false;

        lang = PlayerPrefs.GetInt("Lang Pref", 0);
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[lang];
    }

    // Update is called once per frame
    void Update()
    {
        string untilNet = "";
        
        switch(lang)
        {
            //English
            case 0:
                untilNet = " apples until net";
                break;
            //French
            case 1:
                untilNet = " pommes jusqu'au filet";
                break;
            //Spanish
            case 2:
                untilNet = " manzanas hasta red";
                break;
            //German
            case 3:
                untilNet = " Apfel bis netto";
                break;
            //Portagese
            case 4:
                untilNet = " macas ate ao liquido";
                break;

        }
        appleCnt.text = (10 - appleCount) + untilNet;

        if (PowerUp.powerUpPresent)
        {
            appleCountBox.gameObject.SetActive(false);
        }
        else
        {
            appleCountBox.gameObject.SetActive(true);
        }
        
        appleCollision = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.CompareTag("Apple") && appleCollision == false)
        {
            appleCollision = true;
            //hide the colliding apple
            
            
            //get a random position
            float pos = rndPos(spawnRight, false);
            
            //add 20 to the score
            Score.score += 20;
            
            //if the player isnt using the net and there isnt one already in the area
            if (PowerUp.active != true && PowerUp.powerUpPresent != true)
            {
                //increase the apple count
                appleCount++;
            }
            GameObject appleClone = Instantiate(apple, new Vector3(pos, -4.3f, 0), Quaternion.identity);
            
            col.gameObject.SetActive(false);
            Destroy(col.transform.parent.gameObject);
            
            GameObject prefab = Instantiate(floatingScore, transform.position, Quaternion.identity);
            prefab.GetComponentInChildren<TextMesh>().text = "20";
            Source.PlayOneShot(collectSound);
            Destroy(prefab, secondsToDestroy);


            int rnd = Random.Range(0, 16);

            if (rnd == 5 && starActive == false)
            {
                starActive = true;
                float starPos = Random.Range(mainCamera.ScreenToWorldPoint(new Vector2(0 + leftOffset, 0)).x, mainCamera.ScreenToWorldPoint(new Vector2(Screen.width - rightOffset, 0)).x);
                //spawn and play the leaf animation
                particle.transform.position = new Vector3(pos, 4.5f, 0);
                particle.GetComponent<ParticleSystem>().Play();
        
                //disable the leave after an amount of time
                Source.PlayOneShot(leaves);
        
                //Create a new acorn to spawn
                GameObject starClone = Object.Instantiate<GameObject>(star, new Vector3(starPos, 5.5f, 0), Quaternion.identity);
                Rigidbody2D rb = starClone.GetComponent<Rigidbody2D>();
        
                //add bounce
                rb.AddTorque(-0.3f, ForceMode2D.Force);
        
                //create random forces for the acorns when spawning in 
                float xForce = Random.Range(-30, 30);
                xForce = xForce / 10;
                float yForce = Random.Range(-30, 30);
                yForce = yForce / 10;
        
                //add the force
                rb.AddForce(new Vector2(xForce,yForce),ForceMode2D.Impulse);
            }
            
            //St Patricks
            
            /*if ((rnd == 7 || rnd == 4) && cloverActive == false )
            {
                cloverActive = true;
                float cloverPos = Random.Range(mainCamera.ScreenToWorldPoint(new Vector2(0 + leftOffset, 0)).x, mainCamera.ScreenToWorldPoint(new Vector2(Screen.width - rightOffset, 0)).x);
                //spawn and play the leaf animation
                particle.transform.position = new Vector3(pos, 4.5f, 0);
                particle.GetComponent<ParticleSystem>().Play();
        
                //disable the leave after an amount of time
                Source.PlayOneShot(leaves);
        
                //Create a new acorn to spawn
               Instantiate(clover, new Vector3(cloverPos, 5.5f, 0), Quaternion.identity);
                
            }*/
            

        }

       
    }

    public float rndPos(bool RightPos, bool start)
    {
        
        float pos;
        if (this.spawnRight)
        {
            this.spawnRight = false;
        }
        else
        {
            this.spawnRight = true;
        }

        if (start)
        {
            pos = Random.Range(mainCamera.ScreenToWorldPoint(new Vector2(leftOffset, 0)).x, mainCamera.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth/2, 0)).x);
        }
        else
        {

            if (spawnRight)
            {
                pos = Random.Range(mainCamera.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth/2 + rightOffset, 0)).x, mainCamera.ScreenToWorldPoint(new Vector2(mainCamera.pixelWidth - rightOffset, 1)).x);
            }
            else
            {
                pos = Random.Range(mainCamera.ScreenToWorldPoint(new Vector2(leftOffset, 0)).x, mainCamera.ScreenToWorldPoint(new Vector2(mainCamera.pixelWidth/2 - rightOffset, 0)).x);
            }
        }
        return pos;
    }
}
