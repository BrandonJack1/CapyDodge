using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PromoCode : MonoBehaviour
{
    public TMP_InputField input;
    public string code;
    public TextMeshProUGUI message;

    public GameObject panel;
    private void Start()
    {
    }

    public void enter()
    {
        code = input.text;
        
        if(code == "COINS")
        {
            Coins.AddCoins(1000);
        }
        else if (code == "RESET")
        {
            PlayerPrefs.DeleteAll();
        }
        else
        {
            message.text = "Invalid Code";
        }
    }
    
    public void back()
    {
        panel.SetActive(false);
    }

    public void open()
    {
        panel.SetActive(true);
        message.text = "";
    }
}
