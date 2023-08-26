using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsPref : MonoBehaviour
{

    public TMP_Text controlsText;
    public TMP_Text soundText;
    public TMP_Text musicText;

    public GameObject helpCanvas;
    public GameObject language;
    public GameObject soundBtn;

    public GameObject controlsBtn;
    public GameObject controlsLbl;

    public AudioMixer soundMixer;
    

    public int lang;
    // Start is called before the first frame update
    void Start()
    {
        
        if ((Application.platform == RuntimePlatform.tvOS) && SceneManager.GetActiveScene().buildIndex == 5)
        {
            var eventSystem = EventSystem.current;
            eventSystem.SetSelectedGameObject(soundBtn, new BaseEventData(eventSystem));
        }
    }

    // Update is called once per frame
    void Update()
    {


        lang = PlayerPrefs.GetInt("Lang Pref", 0);


        if (Application.platform == RuntimePlatform.tvOS && SceneManager.GetActiveScene().buildIndex == 5)
        {

            language.SetActive(false);

        }

        if (Application.platform == RuntimePlatform.tvOS)
        {

            controlsBtn.SetActive(false);
            controlsLbl.SetActive(false);


        }

        if (PlayerPrefs.GetString("Controls", "Tilt") == "Touch")
        {

            switch (lang)
            {
                //English
                case 0:
                    controlsText.text = "Touch";
                    break;
                //French
                case 1:
                    controlsText.text = "Toucher";
                    break;
                //Spansih
                case 2:
                    controlsText.text = "Toque";
                    break;
                //German
                case 3:
                    controlsText.text = "Beruhren Sie";
                    break;
                //Portagese
                case 4:
                    controlsText.text = "Tacto";
                    break;
            }
        }
        else if (PlayerPrefs.GetString("Controls", "Tilt") == "Tilt")
        {

            switch (lang)
            {
                //English
                case 0:
                    controlsText.text = "Tilt";
                    break;
                //French
                case 1:
                    controlsText.text = "Inclinaison";
                    break;
                //Spansih
                case 2:
                    controlsText.text = "Inclinacion";
                    break;
                //German
                case 3:
                    controlsText.text = "Kippen";
                    break;
                //Portagese
                case 4:
                    controlsText.text = "Inclinacao";
                    break;
            }

        }

        if (PlayerPrefs.GetString("Sound", "On") == "On")
        {
            switch (lang)
            {
                //English
                case 0:
                    soundText.text = "On";
                    break;
                //French
                case 1:
                    soundText.text = "Sur";
                    break;
                //Spansih
                case 2:
                    soundText.text = "En";
                    break;
                //German
                case 3:
                    soundText.text = "Auf";
                    break;
                //Portagese
                case 4:
                    soundText.text = "Em";
                    break;
            }
        }
        else if (PlayerPrefs.GetString("Sound", "On") == "Off")
        {

            switch (lang)
            {
                //English
                case 0:
                    soundText.text = "Off";
                    break;
                //French
                case 1:
                    soundText.text = "Arret";
                    break;
                //Spansih
                case 2:
                    soundText.text = "Fuera de";
                    break;
                //German
                case 3:
                    soundText.text = "Aus";
                    break;
                //Portagese
                case 4:
                    soundText.text = "Desligado";
                    break;
            }
        }


        if (PlayerPrefs.GetString("Music", "On") == "On")
        {
            switch (lang)
            {
                //English
                case 0:
                    musicText.text = "On";
                    break;
                //French
                case 1:
                    musicText.text = "Sur";
                    break;
                //Spansih
                case 2:
                    musicText.text = "En";
                    break;
                //German
                case 3:
                    musicText.text = "Auf";
                    break;
                //Portagese
                case 4:
                    musicText.text = "Em";
                    break;
            }
        }
        else if (PlayerPrefs.GetString("Music", "On") == "Off")
        {

            switch (lang)
            {
                //English
                case 0:
                    musicText.text = "Off";
                    break;
                //French
                case 1:
                    musicText.text = "Arret";
                    break;
                //Spansih
                case 2:
                    musicText.text = "Fuera de";
                    break;
                //German
                case 3:
                    musicText.text = "Aus";
                    break;
                //Portagese
                case 4:
                    musicText.text = "Desligado";
                    break;
            }



        }
    }

    public void SwitchControls()
    {
        
        if (PlayerPrefs.GetString("Controls", "Tilt") == "Touch")
        {
            
            PlayerPrefs.SetString("Controls", "Tilt");
         

        }
        else if (PlayerPrefs.GetString("Controls", "Tilt") == "Tilt")
        {
            PlayerPrefs.SetString("Controls", "Touch");
        }
        
    }

    public void SwitchSound()
    {
        if (PlayerPrefs.GetString("Sound", "On") == "On")
        {
            
            PlayerPrefs.SetString("Sound", "Off");
         

        }
        else if (PlayerPrefs.GetString("Sound", "On") == "Off")
        {
            PlayerPrefs.SetString("Sound", "On");
        }
        
    }

    public void SwitchMusic()
    {
        
        if (PlayerPrefs.GetString("Music", "On") == "On")
        {
            PlayerPrefs.SetString("Music", "Off");
            
        }
        else if (PlayerPrefs.GetString("Music", "On") == "Off")
        {
            PlayerPrefs.SetString("Music", "On");
        }
    }

    public void Reset()
    {
        
        PlayerPrefs.DeleteAll();
    }

    public void ShowCanvas()
    {
        helpCanvas.SetActive(true);
        
    }

    public void HideCanvas()
    {
        
        helpCanvas.SetActive(false);
    }
    
}
