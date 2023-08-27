using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StoreSkins : MonoBehaviour
{
    public GameObject topHat;
    public GameObject santaHat;
    public GameObject bow;
    public GameObject crown;
    public GameObject cone;
    public GameObject prison;
    public GameObject pirateHat;
    public GameObject cupid;

    public GameObject superHero;
    public GameObject patrick;
    public GameObject rainbow;
    public GameObject bunny;
    public GameObject egg;
    public GameObject tiger;
    public GameObject sunglasses;
    public GameObject suit;
    public GameObject chef;
    public GameObject wizard;
    public GameObject headBow;

    public GameObject construction;
    // Start is called before the first frame update
    void Update()
    {

        if (PlayerPrefs.GetString("Active Player Accessory") == "TopHat")
        {
            topHat.gameObject.SetActive(true);
        }
        else
        {
            topHat.gameObject.SetActive(false);
        }
        
        if (PlayerPrefs.GetString("Active Player Accessory") == "SantaHat")
        {
            santaHat.gameObject.SetActive(true);
        }
        else
        {
            santaHat.gameObject.SetActive(false);
        }
        
        if (PlayerPrefs.GetString("Active Player Accessory") == "Bow")
        {
            bow.gameObject.SetActive(true);
        }
        else
        {
            bow.gameObject.SetActive(false);
        }
        
        if (PlayerPrefs.GetString("Active Player Accessory") == "Crown")
        {
            crown.gameObject.SetActive(true);
        }
        else
        {
            crown.gameObject.SetActive(false);
        }
        
        if (PlayerPrefs.GetString("Active Player Accessory") == "Cone")
        {
            cone.gameObject.SetActive(true);
        }
        else
        {
            cone.gameObject.SetActive(false);
        }
        
        if (PlayerPrefs.GetString("Active Player Skin Set") == "Prison")
        {
            prison.gameObject.SetActive(true);
        }
        else
        {
            prison.gameObject.SetActive(false);
        }
        
        if (PlayerPrefs.GetString("Active Player Skin") == "Cupid")
        {
            cupid.gameObject.SetActive(true);
        }
        else
        {
            cupid.gameObject.SetActive(false);
        }
        
        if (PlayerPrefs.GetString("Active Player Accessory") == "PirateHat")
        {
            pirateHat.gameObject.SetActive(true);
        }
        else
        {
            pirateHat.gameObject.SetActive(false);
        }
        
        if (PlayerPrefs.GetString("Active Player Skin") == "SuperHero")
        {
            superHero.gameObject.SetActive(true);
        }
        else
        {
            superHero.gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetString("Active Player Skin Set") == "Patrick")
        {
            patrick.gameObject.SetActive(true);
        }
        else
        {
            patrick.gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetString("Active Player Skin") == "Rainbow")
        {
            rainbow.gameObject.SetActive(true);
        }
        else
        {
            rainbow.gameObject.SetActive(false);
        }
        
        if (PlayerPrefs.GetString("Active Player Skin Set") == "Bunny")
        {
            bunny.gameObject.SetActive(true);
        }
        else
        {
            bunny.gameObject.SetActive(false);
        }
        
        if (PlayerPrefs.GetString("Active Player Skin") == "Egg")
        {
            egg.gameObject.SetActive(true);
        }
        else
        {
            egg.gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetString("Active Player Skin") == "Tiger")
        {
            tiger.gameObject.SetActive(true);
        }
        else
        {
            tiger.gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetString("Active Player Accessory") == "Sunglasses")
        {
            sunglasses.gameObject.SetActive(true);
            
        }
        else
        {
            sunglasses.gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetString("Active Player Skin") == "Suit")
        {
            
            suit.gameObject.SetActive(true);
        }
        else
        {
            suit.gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetString("Active Player Skin Set") == "Chef")
        {
            chef.gameObject.SetActive(true);
        }
        else
        {
            chef.gameObject.SetActive(false);
        }
        
        if (PlayerPrefs.GetString("Active Player Skin Set") == "Construction")
        {
            construction.gameObject.SetActive(true);
        }
        else
        {
            construction.gameObject.SetActive(false);
        }
        
        if (PlayerPrefs.GetString("Active Player Accessory") == "Wizard")
        {
            wizard.gameObject.SetActive(true);
            
        }
        else
        {
            wizard.gameObject.SetActive(false);
        }
        
        if (PlayerPrefs.GetString("Active Player Accessory") == "HeadBow")
        {
            headBow.gameObject.SetActive(true);
            
        }
        else
        {
            headBow.gameObject.SetActive(false);
        }
    }
}
