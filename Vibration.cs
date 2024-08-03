using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Vibration : MonoBehaviour
{
    
     
    [SerializeField] private GameObject vibrationBtn;
    [SerializeField] private TMP_Text vibrationText;
    private int lang;
    
    // Start is called before the first frame update
    void Start()
    {
        if (Application.platform != RuntimePlatform.IPhonePlayer)
        {
            vibrationBtn.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        lang = PlayerPrefs.GetInt("Lang Pref", 0);
        
        if (PlayerPrefs.GetString("Vibration", "On") == "On")
        {
            vibrationText.text = Translate("On");
        }
        else if (PlayerPrefs.GetString("Vibration", "On") == "Off")
        {
            vibrationText.text = Translate("Off");
        }
    }

    public void switchVibration()
    {
        if (PlayerPrefs.GetString("Vibration", "On") == "On")
        {
            PlayerPrefs.SetString("Vibration", "Off");
        }
        else if (PlayerPrefs.GetString("Vibration", "On") == "Off")
        {
            PlayerPrefs.SetString("Vibration", "On");
        }
    }
    
    private string Translate(string word)
    {
        string label = "";
        
        if (word == "On")
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
