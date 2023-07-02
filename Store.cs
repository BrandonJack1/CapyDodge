using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{

    public TextMeshProUGUI coinAmt;

    public TMP_Text topHat;
    public TMP_Text santaHat;
    public TMP_Text bow;
    public TMP_Text crown;
    public TMP_Text cone;
    public TMP_Text prison;
    
    public GameObject premiumBtn;
    public GameObject equipBtn;
    public GameObject noAdsImage;
    public GameObject noAdsBox;
    

    public AudioSource source;
    public AudioClip clip;
    public AudioClip doorOpen;
    public AudioClip bell;
    public AudioClip invalid;

    public GameObject price1;
    public GameObject price2;
    public GameObject price3;
    public GameObject price4;
    public GameObject price5;

    public GameObject betaBox;


    public static bool playPurchaseSound = false;
    public static bool playInvalidSound = false;

    public string[] oneThousand = {"Bow", "SantaHat" ,"TopHat" };
    public string[] twoThousand = {"Crown"};
    public string[] threeThousand = {"Prison"};
    
    
   
     
    
    

    // Start is called before the first frame update
    void Start()
    {
        
        //PlayerPrefs.DeleteAll();
        //REMOVE
        //PlayerPrefs.SetInt("Coins", 500);
        
        source.PlayOneShot(bell);
        source.PlayOneShot(doorOpen);
        
        
        
        
        coinAmt.text = PlayerPrefs.GetInt("Coins", 0).ToString();

    }

    // Update is called once per frame
    void Update()
    {
        if (playPurchaseSound == true)
        {
            playPurchaseSound = false;
            source.PlayOneShot(clip);
            
        }
        else if (playInvalidSound == true)
        {
            playInvalidSound = false;
            source.PlayOneShot(invalid);
        }
        
        
        coinAmt.text = PlayerPrefs.GetInt("Coins", 0).ToString();
        
        /*if (PlayerPrefs.GetString("Beta", "No") == "Yes")
        {
            betaBox.SetActive(true);
        }
        
        if (PlayerPrefs.GetString("Player Cone") == "Not Equipped")
        {
            cone.text = "Equip";
        }
        if(PlayerPrefs.GetString("Active Player Skin") == "Cone")
        {
            cone.text = "Equipped";
            
        }

        if (PlayerPrefs.GetString("Player TopHat") == "Not Equipped")
        {
            topHat.text = "Equip";
            price1.SetActive(false);
        }
        if(PlayerPrefs.GetString("Active Player Skin") == "TopHat")
        {
            topHat.text = "Equipped";
            price1.SetActive(false);
        }
        
        if (PlayerPrefs.GetString("Player Bow") == "Not Equipped")
        {
            bow.text = "Equip";
            price2.SetActive(false);
            
        }
        if(PlayerPrefs.GetString("Active Player Skin") == "Bow")
        {
            bow.text = "Equipped";
            price2.SetActive(false);
        }
        
        if (PlayerPrefs.GetString("Player SantaHat") == "Not Equipped")
        {
            santaHat.text = "Equip";
            price3.SetActive(false);
        }
        if(PlayerPrefs.GetString("Active Player Skin") == "SantaHat")
        {
            santaHat.text = "Equipped";
            price3.SetActive(false);
        }

        if (PlayerPrefs.GetString("Player Crown") == "Not Equipped")
        {
            crown.text = "Equip";
            price4.SetActive(false);
        }
        if(PlayerPrefs.GetString("Active Player Skin") == "Crown")
        {
            crown.text = "Equipped";
            price4.SetActive(false);
        }
        
        if (PlayerPrefs.GetString("Player Prison") == "Not Equipped")
        {
            prison.text = "Equip";
            price5.SetActive(false);
        }
        if(PlayerPrefs.GetString("Active Player Skin") == "Prison")
        {
            prison.text = "Equipped";
            price5.SetActive(false);
        }*/

        //PREIMIUM CODE

        if (PlayerPrefs.GetString("ShowAds", "Yes") == "No")
        {
            
            premiumBtn.gameObject.SetActive(false);
            noAdsImage.gameObject.SetActive(false);
            noAdsBox.gameObject.SetActive(false);
            
        }

    }



    
    public void Buy(Button btn)
    {
        
        //if the item is not already owned
        if (PlayerPrefs.GetString("Player " + btn.name) != "Equipped" && PlayerPrefs.GetString("Player " + btn.name) != "Not Equipped")
        {
            
            Debug.Log(btn.name);
            Debug.Log("TopHat");
            if (oneThousand.Contains(btn.name))
            {
                //if player doesnt have enough coins then dont do anything
                if (PlayerPrefs.GetInt("Coins") < 1000)
                {
                    source.PlayOneShot(invalid);
                    return;
                }
                Coins.subtractCoins(1000);
                source.PlayOneShot(clip);
                PlayerPrefs.SetString("Player " + btn.name, "Not Equipped");
            }
            else if (twoThousand.Contains(btn.name))
            {
                //if player doesnt have enough coins then dont do anything
                if (PlayerPrefs.GetInt("Coins") < 2000)
                {
                    source.PlayOneShot(invalid);
                    return;
                }
                Coins.subtractCoins(2000);
                source.PlayOneShot(clip);
                PlayerPrefs.SetString("Player " + btn.name, "Not Equipped");
            }
            else if (threeThousand.Contains(btn.name))
            {
                //if player doesnt have enough coins then dont do anything
                if (PlayerPrefs.GetInt("Coins") < 3000)
                {
                    source.PlayOneShot(invalid);
                    return;
                }
                Coins.subtractCoins(3000);
                source.PlayOneShot(clip);
                PlayerPrefs.SetString("Player " + btn.name, "Not Equipped");
            }

        }
        else if (PlayerPrefs.GetString("Active Player Skin") == btn.name)
        {
            PlayerPrefs.SetString("Player " + btn.name, "Not Equipped");
            PlayerPrefs.SetString("Active Player Skin", "");
            
        }
        else
        {
            PlayerPrefs.SetString("Active Player Skin", btn.name);
           
        }
    }
}
