using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
public class Language : MonoBehaviour
{
    public GameObject pannel;
    private bool active = false;
    public void ChangeLocale(int localeID)
    {
        if (active == true)
            return;
        StartCoroutine(SetLocale(localeID));
    }

    void Start()
    {
        
        //if the player hasnt selected a language yet, present it to them (dont show for Apple TV)
        if (PlayerPrefs.GetString("Lang Set", "No") == "No" && Application.platform != RuntimePlatform.tvOS)
        {
            pannel.SetActive(true);
        }
        else
        {
            //otherwise set the locale to their preference
            int locale;
            locale = PlayerPrefs.GetInt("Lang Pref", 0);
            ChangeLocale(locale);
        }
    }

    IEnumerator SetLocale(int _localeID)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
        active = false;
        PlayerPrefs.SetString("Lang Set", "Yes");
        PlayerPrefs.SetInt("Lang Pref", _localeID);
        pannel.SetActive(false);
    }

    public void closePannel()
    {
        pannel.SetActive(false);
        PlayerPrefs.SetString("Lang Set", "Yes");
    }

    public void openPannel()
    {
        pannel.SetActive(true);
    }
}
