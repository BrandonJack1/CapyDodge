using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PromoCode : MonoBehaviour
{
    [SerializeField] private TMP_InputField input;
    [SerializeField] private TextMeshProUGUI message;
    [SerializeField] private GameObject panel;
    
    private string code;
    
    public void Enter()
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
    
    public void Back()
    {
        panel.SetActive(false);
    }

    public void Open()
    {
        panel.SetActive(true);
        message.text = "";
    }
}
