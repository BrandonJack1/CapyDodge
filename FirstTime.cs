using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FirstTime : MonoBehaviour
{
    [SerializeField] private GameObject howTo;
    [SerializeField] private GameObject controls;
    [SerializeField] private GameObject appleTVControls;
    [SerializeField] private GameObject howToBtn;
    [SerializeField] private GameObject controlsBtn;
    
    void Start()
    {
        //if its the players first time then show the how to guide
        if (PlayerPrefs.GetString("FirstTime", "Yes") == "Yes")
        {
            howTo.gameObject.SetActive(true);
            
            //Show Apple TV how to play
            if (Application.platform == RuntimePlatform.tvOS)
            {
                var eventSystem = EventSystem.current;
                eventSystem.SetSelectedGameObject(howToBtn, new BaseEventData(eventSystem));
            }
            
            //freeze time of the game
            Time.timeScale = 0f;
        }
    }
    public void Next()
    {
        //Method shows the next portion of the tutorial screen
        if (Application.platform == RuntimePlatform.tvOS)
        {
            //hide previous screen and show next
            howTo.gameObject.SetActive(false);
            appleTVControls.gameObject.SetActive(true);

            var eventSystem = EventSystem.current;
            eventSystem.SetSelectedGameObject(controlsBtn, new BaseEventData(eventSystem));
        }
        else
        {
            //hide previous screen and show next
            howTo.gameObject.SetActive(false);
            controls.gameObject.SetActive(true);
        }
    }

    public void Dismiss()
    {
        //record that the player did the tutorial so it is not shown again
        PlayerPrefs.SetString("FirstTime", "No");

        if (Application.platform == RuntimePlatform.tvOS)
        {
            appleTVControls.gameObject.SetActive(false);
        }
        else
        {
            controls.gameObject.SetActive(false);
        }
        
        //resume time
        Time.timeScale = 1f;
    }
}
