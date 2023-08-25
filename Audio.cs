using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Audio : MonoBehaviour
{

    public AudioMixer soundMixer;
    
    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetString("Sound", "On") == "On")
        {
            
            soundMixer.SetFloat("soundVolume", 1);
        }
        else if (PlayerPrefs.GetString("Sound", "On") == "Off")
        {
            soundMixer.SetFloat("soundVolume", -80f);
        }
        
        if (PlayerPrefs.GetString("Music", "On") == "On")
        {
            soundMixer.SetFloat("musicVolume", -10f);
        }
        else if (PlayerPrefs.GetString("Music", "On") == "Off")
        {
            soundMixer.SetFloat("musicVolume", -80f);
        }
        
    }
}
