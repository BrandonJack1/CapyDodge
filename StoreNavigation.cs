using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;

public class StoreNavigation : MonoBehaviour
{

    public GameObject inventory;

    public GameObject skinSets;
    public GameObject skins;
    public GameObject accessories;
    

    public GameObject store;

    public Button storeBtn;
    public Sprite pressed;
    public Sprite normal;
    
    public GameObject allStoreBought;
    public GameObject noSkinSetsOwned;
    public GameObject noSkinsOwned;
    public GameObject noAccessoriesOwned;

    public GameObject coinMenu;

    public TextMeshProUGUI coinAmount;

    public GameObject arrow1;
    public GameObject arrow2;
    public GameObject arrow3;


    public GameObject noAds;
    public GameObject buyCoins;
    public GameObject restorePurchases;

    public int lang;
    

    // public GameObject featured;

    public TextMeshProUGUI navLabel;
    // Start is called before the first frame update
    void Start()
    {
        lang = PlayerPrefs.GetInt("Lang Pref", 0);
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[lang];
        
        storeBtn.GetComponent<Image>().sprite = pressed;

        if (Application.platform == RuntimePlatform.tvOS)
        {
            noAds.SetActive(false);
            buyCoins.SetActive(false);
            restorePurchases.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

        coinAmount.text = PlayerPrefs.GetInt("Coins", 0).ToString();
        
        
        if (inventory.activeInHierarchy)
        {
            storeBtn.GetComponent<Image>().sprite = normal;
        }

        if (InventoryManager.instance.storeItems.Count == 0)
        {
            allStoreBought.SetActive(true);
        }
        else
        {
            allStoreBought.SetActive(false);
        }

        if (InventoryManager.instance.skinSets.Count == 0)
        {
            
            noSkinSetsOwned.SetActive(true);
        }
        else
        {
            noSkinSetsOwned.SetActive(false);
        }
        
        if (InventoryManager.instance.skins.Count == 0)
        {
            
            noSkinsOwned.SetActive(true);
        }
        else
        {
            noSkinsOwned.SetActive(false);
        }
        
        if (InventoryManager.instance.accessories.Count == 0)
        {
            
            noAccessoriesOwned.SetActive(true);
        }
        else
        {
            noAccessoriesOwned.SetActive(false);
        }
        
    }


    public void ShowInventory()
    {
        inventory.SetActive(true);
        store.SetActive(false);
        //featured.SetActive(false);
        
        
        switch (lang)
        {
            //English
            case 0:
                navLabel.text = "Inventory";
                break;
            //French
            case 1:
                navLabel.text = "Inventaire";
                break;
            //Spanish
            case 2:
                navLabel.text = "Inventario";
                break;
            //German
            case 3:
                navLabel.text = "Bestandsaufnahme";
                break;
            //Portageuse
            case 4:
                navLabel.text = "Inventario";
                break;
        }
    }

    public void showStore()
    {
        store.SetActive(true);
        inventory.SetActive(false);
        //featured.SetActive(false);
        
        
        switch (lang)
        {
            //English
            case 0:
                navLabel.text = "Store";
                break;
            //French
            case 1:
                navLabel.text = "Magasin";
                break;
            //Spanish
            case 2:
                navLabel.text = "Almacenar";
                break;
            //German
            case 3:
                navLabel.text = "Laden Sie";
                break;
            //Portagese
            case 4:
                navLabel.text = "Loja";
                break;
            
            
        }
    }

    public void ShowSkinSets()
    {
        skinSets.SetActive(true);
        skins.SetActive(false);
        accessories.SetActive(false);
        
        arrow1.SetActive(true);
        arrow2.SetActive(false);
        arrow3.SetActive(false);
        
    }
    
    public void ShowSkins()
    {
        skinSets.SetActive(false);
        skins.SetActive(true);
        accessories.SetActive(false);
        
        arrow1.SetActive(false);
        arrow2.SetActive(true);
        arrow3.SetActive(false);
        
    }
    
    public void ShowAccessories()
    {
        skinSets.SetActive(false);
        skins.SetActive(false);
        accessories.SetActive(true);
        
        arrow1.SetActive(false);
        arrow2.SetActive(false);
        arrow3.SetActive(true);
        
    }

    public void ShowFeatured()
    {
        
        //featured.SetActive(true);
        inventory.SetActive(false);
        store.SetActive(false);
    }

    public void ShowCoins()
    {
        coinMenu.SetActive(true);
        
    }

    public void CloseCoins()
    {
        
        coinMenu.SetActive(false);
    }
}
