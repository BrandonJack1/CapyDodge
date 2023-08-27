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
    [SerializeField] private GameObject capy;
    [SerializeField] private GameObject copper;
    [SerializeField] private TextMeshProUGUI coppy;
    [SerializeField] private GameObject currentCapy;
    
    private bool moveRight = true;
    private float xPos;
    private int objectEnaabled = 0;
    
    [SerializeField] private TextMeshProUGUI premiumText;
    [SerializeField] private Transform grassParent;
    
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip day;
    [SerializeField] private AudioClip night;

    public static bool playSound;

    [SerializeField] private GameObject playBtn;
    [SerializeField] private GameObject downloadText;
    
    // Start is called before the first frame update
    void Start()
    {
        //TVOS initialize code
        if (Application.platform == RuntimePlatform.tvOS)
        {
            PlayerPrefs.SetString("ShowAds", "No");
            var eventSystem = EventSystem.current;
            eventSystem.SetSelectedGameObject(playBtn, new BaseEventData(eventSystem));
            downloadText.SetActive(true);
        }
        
        //set the premium text if the player paid to remove ads
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
        //move the main menu character left and right
        if (moveRight)
        {
            currentCapy.transform.Translate(Vector3.right * 4f * Time.deltaTime);
        }
        else
        {
            currentCapy.transform.Translate(Vector3.left * 4f * Time.deltaTime);
        }

        xPos = currentCapy.transform.position.x;
        
        //if the player reached the right most border
        if (xPos > 13.5)
        {
            moveRight = false;
            //flip sprite
            currentCapy.transform.localScale = new Vector3(-0.8f, 0.8f, 1);
            
            //deactivate object on player
            currentCapy.transform.GetChild(objectEnaabled).gameObject.SetActive(false);
            
            //spawn new object on player
            RandomObject();
        }
        else if (xPos < -13.5)
        {
            moveRight = true;
            
            //flip sprite
            currentCapy.transform.localScale = new Vector3(0.8f, 0.8f, 1);
            
            //deactivate object on player
            currentCapy.transform.GetChild(objectEnaabled).gameObject.SetActive(false);
            
            //spawn new object on player
            RandomObject();
        }
    }

    void RandomObject()
    {
        int rnd = Random.Range(0, 3);
        
        //swap between activating apple and net on player
        if (rnd == 0 || rnd == 1)
        {
            objectEnaabled = rnd;
            currentCapy.transform.GetChild(rnd).gameObject.SetActive(true);
        }
    }
}
