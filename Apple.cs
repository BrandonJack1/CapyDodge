using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;
using UnityEngine.Localization.Settings;
using UnityEngine.Serialization;


public class Apple : MonoBehaviour
{
    [SerializeField] private GameObject apple;
    [SerializeField] private GameObject star;
    [SerializeField] private GameObject particle;
    [SerializeField] private GameObject floatingScore;
    [SerializeField] private GameObject appleCountBox;
    
    [SerializeField] private AudioSource Source;
    [SerializeField] private AudioClip collectSound;
    [SerializeField] private AudioClip leaves;
    
    private float leftOffset;
    private float rightOffset;
    private float middleOffset;
    private readonly float secondsToDestroy = 1f;
    
    public static int appleCount = 0;
    private int lang;

    [FormerlySerializedAs("appleCnt")] [SerializeField] private TextMeshProUGUI appleCountText;
    
    public Camera mainCamera;

    private bool appleCollision = false;
    public static bool starActive = false;
    private bool spawnRight = false;
    
    void Start()
    {
        //Get a random position to spawn
        float pos = RndPos(false, true);
        
        //spawn an apple
        GameObject appleClone = Instantiate<GameObject>(apple, new Vector3(pos, -4.2f, 0), Quaternion.identity);
        
        //reset apple counter
        appleCount = 0;
        
        //determine screen offsets
        leftOffset = Camera.main.pixelWidth / 30;
        rightOffset = Camera.main.pixelWidth / 20;
        middleOffset = Camera.main.pixelWidth / 10;

        starActive = false;
        
        //Set language prefs
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
        appleCountText.text = (10 - appleCount) + untilNet;
        
        //if the net is present, hide the apple count UI
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

            //get a random position
            float pos = RndPos(spawnRight, false);
            
            //add 20 to the score
            Score.score += 20;
            
            //if the player isnt using the net and there isnt one already in the area
            if (PowerUp.active != true && PowerUp.powerUpPresent != true)
            {
                //increase the apple count
                appleCount++;
            }
            GameObject appleClone = Instantiate(apple, new Vector3(pos, -4.3f, 0), Quaternion.identity);
            
            //hide and destroy the apple
            col.gameObject.SetActive(false);
            Destroy(col.transform.parent.gameObject);
            
            //instantiate a floating score for when the apple is collected
            GameObject prefab = Instantiate(floatingScore, transform.position, Quaternion.identity);
            prefab.GetComponentInChildren<TextMesh>().text = "20";
            Source.PlayOneShot(collectSound);
            Destroy(prefab, secondsToDestroy);

            //1 in 25 chance of the star spawning
            int rnd = Random.Range(0, 25);
            
            //if the chance happens and there isnt already a star spawned in
            if (rnd == 5 && starActive == false)
            {
                starActive = true;
                float starPos = Random.Range(mainCamera.ScreenToWorldPoint(new Vector2(0 + leftOffset, 0)).x, mainCamera.ScreenToWorldPoint(new Vector2(Screen.width - rightOffset, 0)).x);
                
                //spawn and play the leaf animation
                particle.transform.position = new Vector3(starPos, 4.5f, 0);
                particle.GetComponent<ParticleSystem>().Play();
        
                //disable the leave after an amount of time
                Source.PlayOneShot(leaves);
                
                //Create a new star to spawn
                GameObject starClone = Object.Instantiate<GameObject>(star, new Vector3(starPos, 5.5f, 0), Quaternion.identity);
                Rigidbody2D rb = starClone.GetComponent<Rigidbody2D>();
        
                //add bounce
                rb.AddTorque(-0.3f, ForceMode2D.Force);
        
                //create random forces for the stars when spawning in 
                float xForce = Random.Range(-30, 30);
                xForce = xForce / 10;
                float yForce = Random.Range(-30, 30);
                yForce = yForce / 10;
        
                //add the force
                rb.AddForce(new Vector2(xForce,yForce),ForceMode2D.Impulse);
            }
        }
    }

    public float RndPos(bool RightPos, bool start)
    {
        float pos;
        
        //alternate between spawning Apples on the left and right sides of the screen
        if (this.spawnRight)
        {
            this.spawnRight = false;
        }
        else
        {
            this.spawnRight = true;
        }
        
        //if this is the start of the game just spawn the apple anywhere
        if (start)
        {
            pos = Random.Range(mainCamera.ScreenToWorldPoint(new Vector2(leftOffset, 0)).x, mainCamera.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth/2, 0)).x);
        }
        else
        {
            //spawn the apple on the right side of the screen
            if (this.spawnRight)
            {
                pos = Random.Range(mainCamera.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth/2 + rightOffset, 0)).x, mainCamera.ScreenToWorldPoint(new Vector2(mainCamera.pixelWidth - rightOffset, 1)).x);
            }
            //spawn the apple on the left side of the screen
            else
            {
                pos = Random.Range(mainCamera.ScreenToWorldPoint(new Vector2(leftOffset, 0)).x, mainCamera.ScreenToWorldPoint(new Vector2(mainCamera.pixelWidth/2 - rightOffset, 0)).x);
            }
        }
        return pos;
    }
}
