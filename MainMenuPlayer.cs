using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class MainMenuPlayer : MonoBehaviour
{
    public GameObject capy;
    public GameObject copper;
    public TextMeshProUGUI coppy;
    public GameObject currentCapy;
    public bool moveRight = true;
    float xPos;
    public int objectEnaabled = 0;
    public TextMeshProUGUI premiumText;
    public Transform grassParent;


    public AudioSource source;
    public AudioClip day;
    public AudioClip night;

    public static bool playSound;

    public GameObject playBtn;

    public GameObject downloadText;




    //SEASONAL

    public Sprite seasonalGrass;
    public Sprite normalGrass;

    public void EnableSeasonal()
    {
        /*if (PlayerPrefs.GetString("Seasonal", "No") == "No")
        {
            PlayerPrefs.SetString("Seasonal", "Yes");
            
            foreach (Transform child in grassParent)
            {
                child.gameObject.GetComponent<SpriteRenderer>().sprite = seasonalGrass;
            }
            
        }
        else if (PlayerPrefs.GetString("Seasonal", "No") == "Yes")
        {
            PlayerPrefs.SetString("Seasonal", "No");
            
            foreach (Transform child in grassParent)
            {
                child.gameObject.GetComponent<SpriteRenderer>().sprite = normalGrass;
            }
        }*/
    }



    // Start is called before the first frame update
    void Start()
    {


        /*if (PlayerPrefs.GetString("Seasonal", "No") == "Yes")
        {

            foreach (Transform child in grassParent)
            {
                child.gameObject.GetComponent<SpriteRenderer>().sprite = seasonalGrass;
            }
            
        }
        else if (PlayerPrefs.GetString("Seasonal", "No") == "No")
        {
            foreach (Transform child in grassParent)
            {
                child.gameObject.GetComponent<SpriteRenderer>().sprite = normalGrass;
            }
        }*/

        if (Application.platform == RuntimePlatform.tvOS)
        {
            PlayerPrefs.SetString("ShowAds", "No");
            var eventSystem = EventSystem.current;
            eventSystem.SetSelectedGameObject(playBtn, new BaseEventData(eventSystem));
            downloadText.SetActive(true);
        }

        if (PlayerPrefs.GetString("ShowAds") == "No")
        {
            
            if (Application.platform == RuntimePlatform.tvOS)
            {
                
                premiumText.gameObject.SetActive(false);
            }
            else
            {
                premiumText.gameObject.SetActive(true);
            }
            
        }
        else
        {
            premiumText.gameObject.SetActive(false);
        }
        
        if (PlayerPrefs.GetString("Sound", "On") == "On")
        {
            //AudioListener.volume = 1;

        }
        else if (PlayerPrefs.GetString("Sound", "On") == "Off")
        {
            //AudioListener.volume = 0;
        }
        //if copper mode is enabled
        if (PlayerPrefs.GetString("Copper", "No") == "Yes")
        {
            
            GameObject startCapy = Instantiate(copper, new Vector3(-11.8f, -3.9f, 1), Quaternion.identity);
            currentCapy = startCapy;
            coppy.gameObject.SetActive(true);
        }
        else{
            
            GameObject startCapy = Instantiate(capy, new Vector3(-11.8f, -3.9f, 1), Quaternion.identity);
            currentCapy = startCapy;
        }
        
       Time.timeScale = 1f;
        
       //show a random object with the player
       RandomObject();
    }

    // Update is called once per frame
    void Update()
    {
        if (playSound)
        {
            playSound = false;
            if (BackgroundPicker.activeBG == 1)
            {
                source.loop = true;
                source.clip = day;
                //source.Play();
                //source.volume = 1f;
            }
            else if (BackgroundPicker.activeBG == 2)
            {
                source.loop = true;
                source.clip = night;
                //source.Play();
                //source.volume = 0.5f;
            }
        }

        if (moveRight)
        {
            currentCapy.transform.Translate(Vector3.right * 4f * Time.deltaTime);
        }
        else
        {
            currentCapy.transform.Translate(Vector3.left * 4f * Time.deltaTime);
        }

        xPos = currentCapy.transform.position.x;

        if (xPos > 13.5)
        {
            moveRight = false;
            currentCapy.transform.localScale = new Vector3(-0.8f, 0.8f, 1);
            currentCapy.transform.GetChild(objectEnaabled).gameObject.SetActive(false);
            RandomObject();
        }
        else if (xPos < -13.5)
        {
            moveRight = true;
            currentCapy.transform.localScale = new Vector3(0.8f, 0.8f, 1);
            currentCapy.transform.GetChild(objectEnaabled).gameObject.SetActive(false);
            RandomObject();
            
        }
        
        

    }

    void RandomObject()
    {
        int rnd = Random.Range(0, 3);

        if (rnd == 0 || rnd == 1)
        {
            objectEnaabled = rnd;
            currentCapy.transform.GetChild(rnd).gameObject.SetActive(true);
        }
    }
}
