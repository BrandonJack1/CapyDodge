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
    [SerializeField] private TMP_Text controlsText;
    [SerializeField] private TMP_Text soundText;
    [SerializeField] private TMP_Text musicText;

    [SerializeField] private GameObject helpCanvas;
    [SerializeField] private GameObject language;
    [SerializeField] private GameObject soundBtn;

    [SerializeField] private GameObject controlsBtn;
    [SerializeField] private GameObject controlsLbl;
    
    private int lang;
    
    // Start is called before the first frame update
    void Start()
    {
        //TVOS set default selection
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
            //hide controls option for TVOS
            controlsBtn.SetActive(false);
            controlsLbl.SetActive(false);
        }

        if (PlayerPrefs.GetString("Controls", "Tilt") == "Touch")
        {
            controlsText.text = Translate("Touch");
        }
        else if (PlayerPrefs.GetString("Controls", "Tilt") == "Tilt")
        {
            controlsText.text = Translate("Tilt");
        }

        if (PlayerPrefs.GetString("Sound", "On") == "On")
        {
            soundText.text = Translate("On");
        }
        else if (PlayerPrefs.GetString("Sound", "On") == "Off")
        {
            soundText.text = Translate("Off");
        }
        
        if (PlayerPrefs.GetString("Music", "On") == "On")
        {
            musicText.text = Translate("On");
        }
        else if (PlayerPrefs.GetString("Music", "On") == "Off")
        {
            musicText.text = Translate("Off");
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
    
    private string Translate(string word)
    {
        string label = "";
        if (word == "Touch")
        {
            switch (lang)
            {
                //English
                case 0:
                    label = "Touch";
                    break;
                //French
                case 1:
                    label = "Toucher";
                    break;
                //Spansih
                case 2:
                    label = "Toque";
                    break;
                //German
                case 3:
                    label = "Beruhren Sie";
                    break;
                //Portagese
                case 4:
                    label = "Tacto";
                    break;
            }
        }
        else if (word == "Tilt")
        {
            switch (lang)
            {
                //English
                case 0:
                    label = "Tilt";
                    break;
                //French
                case 1:
                    label = "Inclinaison";
                    break;
                //Spansih
                case 2:
                    label = "Inclinacion";
                    break;
                //German
                case 3:
                    label = "Kippen";
                    break;
                //Portagese
                case 4:
                    label = "Inclinacao";
                    break;
            }
        }
        else if (word == "On")
        {
            switch (lang)
            {
                //English
                case 0:
                    label = "On";
                    break;
                //French
                case 1:
                    label = "Sur";
                    break;
                //Spansih
                case 2:
                    label = "En";
                    break;
                //German
                case 3:
                    label = "Auf";
                    break;
                //Portagese
                case 4:
                    label = "Em";
                    break;
            }
        }
        else if (word == "Off")
        {
            switch (lang)
            {
                //English
                case 0:
                    label = "Off";
                    break;
                //French
                case 1:
                    label = "Arret";
                    break;
                //Spansih
                case 2:
                    label = "Fuera de";
                    break;
                //German
                case 3:
                    label = "Aus";
                    break;
                //Portagese
                case 4:
                    label = "Desligado";
                    break;
            }
        }
        return label;
    }
    
}
