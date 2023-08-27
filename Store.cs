using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//Legacy Store Code. No longer used
public class Store : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinAmt;

    [SerializeField] private TMP_Text topHat;
    [SerializeField] private TMP_Text santaHat;
    [SerializeField] private TMP_Text bow;
    [SerializeField] private TMP_Text crown;
    [SerializeField] private TMP_Text cone;
    [SerializeField] private TMP_Text prison;
    
    [SerializeField] private GameObject premiumBtn;
    [SerializeField] private GameObject equipBtn;
    [SerializeField] private GameObject noAdsImage;
    [SerializeField] private GameObject noAdsBox;
    

    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioClip doorOpen;
    [SerializeField] private AudioClip bell;
    [SerializeField] private AudioClip invalid;


    public static bool playPurchaseSound = false;
    public static bool playInvalidSound = false;

    public string[] oneThousand = {"Bow", "SantaHat" ,"TopHat" };
    public string[] twoThousand = {"Crown"};
    public string[] threeThousand = {"Prison"};
    
    // Start is called before the first frame update
    void Start()
    {
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
                Coins.SubtractCoins(1000);
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
                Coins.SubtractCoins(2000);
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
                Coins.SubtractCoins(3000);
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
