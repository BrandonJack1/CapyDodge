using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FirstTime : MonoBehaviour
{
    public GameObject howTo;
    public GameObject controls;
    public GameObject appleTVControls;

    public GameObject howToBtn;
    public GameObject controlsBtn;
    
    void Start()
    {
        //if its the players first time then show the how to guide
        if (PlayerPrefs.GetString("FirstTime", "Yes") == "Yes")
        {
            howTo.gameObject.SetActive(true);
            
            
            //Used for Apple TV

            if (Application.platform == RuntimePlatform.tvOS)
            {
                
                var eventSystem = EventSystem.current;
                eventSystem.SetSelectedGameObject(howToBtn, new BaseEventData(eventSystem));
            }
            
            Time.timeScale = 0f;
        }
    }
    public void next()
    {
        //used for Apple TV

        if (Application.platform == RuntimePlatform.tvOS)
        {
            howTo.gameObject.SetActive(false);
            appleTVControls.gameObject.SetActive(true);

            var eventSystem = EventSystem.current;
            eventSystem.SetSelectedGameObject(controlsBtn, new BaseEventData(eventSystem));
        }
        else
        {
            howTo.gameObject.SetActive(false);
            controls.gameObject.SetActive(true);
        }


    }

    public void dismiss()
    {
        PlayerPrefs.SetString("FirstTime", "No");

        if (Application.platform == RuntimePlatform.tvOS)
        {
            appleTVControls.gameObject.SetActive(false);
            
        }
        else
        {
            controls.gameObject.SetActive(false);
        }
        
        Time.timeScale = 1f;
    }
}
