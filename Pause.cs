using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pause : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip pauseOn;
    [SerializeField] private AudioClip pauseOff;
    [SerializeField] private GameObject pauseMenu;

    private bool paused = false;

    [SerializeField] private GameObject resumeBtn;
    [SerializeField] private GameObject controlsLbl;
    [SerializeField] private GameObject controlsBtn;
    
    private void Start()
    {
        paused = false;
    }
    
    public void PauseGame()
    {
        //Dont allow player to move
        PlayerMovement.playerMove = false;
        
        //Play pause sound
        source.PlayOneShot(pauseOn);
        
        //Freeze time
        Time.timeScale = 0f;
        
        //Show pause menu
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
        //Allow player to move
        PlayerMovement.playerMove = true;
        
        //Play unpause sound
        source.PlayOneShot(pauseOff);
        
        //unfreeze time
        Time.timeScale = 1f;
        
        //hide pause menu
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
    
    public void AppleTVToggle()
    {
        if (paused == false)
        {
            //Start default selection
            if (Application.platform == RuntimePlatform.tvOS)
            {
                var eventSystem = EventSystem.current;
                eventSystem.SetSelectedGameObject(resumeBtn, new BaseEventData(eventSystem));
            }
            
            controlsBtn.SetActive(false);
            controlsLbl.SetActive(true);
            paused = true;
            
            //stop player from moving
            PlayerMovement.playerMove = false;
            
            //play pause sound
            source.PlayOneShot(pauseOn);
            
            //pause time
            Time.timeScale = 0f;
            
            //show pause menu
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
            
            //allow player to move
            PlayerMovement.playerMove = true;
            
            //play unpause sound
            source.PlayOneShot(pauseOff);
            
            //unfreeze time
            Time.timeScale = 1f;
            
            //hide pause menu
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
}
