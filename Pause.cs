using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pause : MonoBehaviour
{

    public AudioSource source;
    public AudioClip pauseOn;
    public AudioClip pauseOff;
    public GameObject pauseMenu;

    public bool paused = false;

    public GameObject resumeBtn;

    public GameObject controlsLbl;
    public GameObject controlsBtn;


    private void Start()
    {
        paused = false;
        
    }
        

    public void AppleTVToggle()
    {
        if (paused == false)
        {
            if (Application.platform == RuntimePlatform.tvOS)
            {
                var eventSystem = EventSystem.current;
                eventSystem.SetSelectedGameObject(resumeBtn, new BaseEventData(eventSystem));
            }
            
            controlsBtn.SetActive(false);
            controlsLbl.SetActive(true);
            
            
           
            paused = true;
            PlayerMovement.playerMove = false;
            source.PlayOneShot(pauseOn);
            Time.timeScale = 0f;
            pauseMenu.gameObject.SetActive(true);

            GameOver.soundActive = false;
            GameOver.toggleSound = true;
            //if the slowdown timer is active, pause it
            if (SlowTime.active)
            {
                //pause the slowdown timer
                GameOver.timer.Stop();
            }
        }
        else
        {
            paused = false;
            PlayerMovement.playerMove = true;
            source.PlayOneShot(pauseOff);
            Time.timeScale = 1f;
            pauseMenu.gameObject.SetActive(false);

        
            GameOver.soundActive = true;
            GameOver.toggleSound = true;
            //if the slowdown timer is active, resume it
            if (SlowTime.active)
            {
                //resume the slowdown timer
                GameOver.timer.Start();
            
            }
        }
    }
    
    public void PauseGame()
    {
        PlayerMovement.playerMove = false;
        source.PlayOneShot(pauseOn);
        Time.timeScale = 0f;
        pauseMenu.gameObject.SetActive(true);

        GameOver.soundActive = false;
        GameOver.toggleSound = true;
        //if the slowdown timer is active, pause it
        if (SlowTime.active)
        {
            //pause the slowdown timer
            GameOver.timer.Stop();
        }
        
    }

    public void Resume()
    {
        PlayerMovement.playerMove = true;
        source.PlayOneShot(pauseOff);
        Time.timeScale = 1f;
        pauseMenu.gameObject.SetActive(false);

        
        GameOver.soundActive = true;
        GameOver.toggleSound = true;
        //if the slowdown timer is active, resume it
        if (SlowTime.active)
        {
            //resume the slowdown timer
            GameOver.timer.Start();
            
        }
        
    }
}
