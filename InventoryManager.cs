using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    
    public static InventoryManager Instance;

    public List<Item> masterList = new List<Item>();

    public List<Item> skinSets = new List<Item>();
    public List<Item> skins = new List<Item>();
    public List<Item> accessories = new List<Item>();
    public List<Item> storeItems = new List<Item>();
    
    public Transform storeItemContent;

    public Transform skinSetContent;
    public Transform skinContent;
    public Transform accessoryContent;
    public GameObject InventoryItem;
    
    public InventoryItemController[] SkinSetItemController;
    public InventoryItemController[] SkinItemController;
    public InventoryItemController[] AccessoryItemController;
    public InventoryItemController[] storeItemsControllers;
    
    public static bool relist = false;

    public int lang;

    private void Awake()
    {
        Instance = this;
    }
    
    public void Start()
    {
        lang = PlayerPrefs.GetInt("Lang Pref");
        ListItems();
        

    }
    
    public void ListItems()
    {
        relist = false;
        
        //reset each of the grid
        foreach (Transform item in storeItemContent)
        {
            Destroy(item.GameObject());
        }
        
        storeItemContent.DetachChildren();
        
        //reset reach of the grids
        foreach (Transform item in skinSetContent)
        {
            Destroy(item.GameObject());
        }
        skinSetContent.DetachChildren();
        
        //reset reach of the grids
        foreach (Transform item in skinContent)
        {
            Destroy(item.GameObject());
        }
        skinContent.DetachChildren();
        
        //reset reach of the grids
        foreach (Transform item in accessoryContent)
        {
            Destroy(item.GameObject());
        }
        accessoryContent.DetachChildren();
        
        sortItems();
        
        //list each item thats in the store items grid
        
        listInventoryItems(skinSets, skinSetContent);
        listInventoryItems(skins, skinContent);
        listInventoryItems(accessories, accessoryContent);
        listStoreItems();


        setStoreItems();
        setInventoryItems(skins, skinContent, SkinItemController);
        setInventoryItems(skinSets, skinSetContent, SkinSetItemController);
        setInventoryItems(accessories, accessoryContent, AccessoryItemController);
    }

    public void listStoreItems()
    {
        foreach (var item in storeItems)
        {
            GameObject obj = Instantiate(InventoryItem, storeItemContent);

            var itemPrice = obj.transform.Find("Price").GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            itemPrice.text = item.price.ToString();
            itemIcon.sprite = item.icon;
            var name = obj.transform.Find("Name").GetComponent<TextMeshProUGUI>();
            name.text = item.storeName;
            var label = obj.transform.Find("Buy Button").transform.Find("Label").GetComponent<TextMeshProUGUI>();
            var type = obj.transform.Find("Type").GetComponent<TextMeshProUGUI>();
            
            
            switch (lang)
            {
                //English
                case 0:
                    label.text = "Buy";
                    break;
                //French
                case 1:
                    label.text = "Acheter";
                    break;
                //Spanish
                case 2:
                    label.text = "Comprar";
                    break;
                //German
                case 3:
                    label.text = "Kaufen";
                    break;
                //Portagese
                case 4:
                    label.text = "Comprar";
                    break;
            }
            if (item.skinSet)
            { 
                switch (lang)
                {
                    //English
                    case 0:
                        type.text = "Skin Set";
                        break;
                    //French
                    case 1:
                        type.text = "Set de peau";
                        break;
                    //Spanish
                    case 2:
                        type.text = "Juego de pieles";
                        break;
                    //German
                    case 3:
                        type.text = "Skin-Set";
                        break;
                    //Portagese
                    case 4:
                        type.text = "Conjunto de pele";
                        break;
                }
            }
            else if (item.skin)
            {
                switch (lang)
                {
                    //English
                    case 0:
                        type.text = "Skin";
                        break;
                    //French
                    case 1:
                        type.text = "Peau";
                        break;
                    //Spanish
                    case 2:
                        type.text = "Piel";
                        break;
                    //German
                    case 3:
                        type.text = "Haut";
                        break;
                    //Portagese
                    case 4:
                        type.text = "Pele";
                        break;
                }
            }
            else if (item.accessory)
            {
                switch (lang)
                {
                    //English
                    case 0:
                        type.text = "Accessory";
                        break;
                    //French
                    case 1:
                        type.text = "Accessoire";
                        break;
                    //Spanish
                    case 2:
                        type.text = "Accesorio";
                        break;
                    //German
                    case 3:
                        type.text = "Zubehor";
                        break;
                    //Portagese
                    case 4:
                        type.text = "Acessorio";
                        break;
                }
            }

        }
    }
    
    public void listInventoryItems(List<Item> items, Transform transform)
    {
        foreach (var item in items)
        {
            GameObject obj = Instantiate(InventoryItem, transform);
            var itemPrice = obj.transform.Find("Price").GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var label = obj.transform.Find("Buy Button").transform.Find("Label").GetComponent<TextMeshProUGUI>();
            var name = obj.transform.Find("Name").GetComponent<TextMeshProUGUI>();
            var type = obj.transform.Find("Type").GetComponent<TextMeshProUGUI>();
            
            var coin = obj.transform.Find("Price").transform.Find("Coin").GameObject();
            coin.SetActive(false);
            name.text = item.storeName;
            itemPrice.text = "";
            itemIcon.sprite = item.icon;

            if (item.skinSet)
            {
                switch (lang)
                {
                    //English
                    case 0:
                        type.text = "Skin Set";
                        break;
                    //French
                    case 1:
                        type.text = "Set de peau";
                        break;
                    //Spanish
                    case 2:
                        type.text = "Juego de pieles";
                        break;
                    //German
                    case 3:
                        type.text = "Skin-Set";
                        break;
                    //Portagese
                    case 4:
                        type.text = "Conjunto de pele";
                        break;
                }
                
                if (PlayerPrefs.GetString("Active Player Skin Set") == item.itemName)
                {
                    switch (lang)
                    {
                        //English
                        case 0:
                            label.text = "Equipped";
                            break;
                        //French
                        case 1:
                            label.text = "Equipe";
                            break;
                        //Spanish
                        case 2:
                            label.text = "Equipado";
                            break;
                        //German
                        case 3:
                            label.text = "Ausgestattet";
                            break;
                        //Portagese
                        case 4:
                            label.text = "Equipado";
                            break;
                    }
                }
                else
                {
                    switch (lang)
                    {
                        //English
                        case 0:
                            label.text = "Equip";
                            break;
                        //French
                        case 1:
                            label.text = "Equiper";
                            break;
                        //Spanish
                        case 2:
                            label.text = "Equipar";
                            break;
                        //German
                        case 3:
                            label.text = "Ausstatten";
                            break;
                        //Portagese
                        case 4:
                            label.text = "Equipar";
                            break;
                    }
                }
            }
            else if (item.skin)
            {
                switch (lang)
                {
                    //English
                    case 0:
                        type.text = "Skin";
                        break;
                    //French
                    case 1:
                        type.text = "Peau";
                        break;
                    //Spanish
                    case 2:
                        type.text = "Piel";
                        break;
                    //German
                    case 3:
                        type.text = "Haut";
                        break;
                    //Portagese
                    case 4:
                        type.text = "Pele";
                        break;
                }
                
                if (PlayerPrefs.GetString("Active Player Skin") == item.itemName)
                {
                    
                    switch (lang)
                    {
                        //English
                        case 0:
                            label.text = "Equipped";
                            break;
                        //French
                        case 1:
                            label.text = "Equipe";
                            break;
                        //Spanish
                        case 2:
                            label.text = "Equipado";
                            break;
                        //German
                        case 3:
                            label.text = "Ausgestattet";
                            break;
                        //Portagese
                        case 4:
                            label.text = "Equipado";
                            break;
                    }
                }
                else
                {
                    switch (lang)
                    {
                        //English
                        case 0:
                            label.text = "Equip";
                            break;
                        //French
                        case 1:
                            label.text = "Equiper";
                            break;
                        //Spanish
                        case 2:
                            label.text = "Equipar";
                            break;
                        //German
                        case 3:
                            label.text = "Ausstatten";
                            break;
                        //Portagese
                        case 4:
                            label.text = "Equipar";
                            break;
                    }
                }
            }
            else if (item.accessory)
            {
                switch (lang)
                {
                    //English
                    case 0:
                        type.text = "Accessory";
                        break;
                    //French
                    case 1:
                        type.text = "Accessoire";
                        break;
                    //Spanish
                    case 2:
                        type.text = "Accesorio";
                        break;
                    //German
                    case 3:
                        type.text = "Zubehor";
                        break;
                    //Portagese
                    case 4:
                        type.text = "Acessorio";
                        break;
                }
                
                if (PlayerPrefs.GetString("Active Player Accessory") == item.itemName)
                {
                    switch (lang)
                    {
                        //English
                        case 0:
                            label.text = "Equipped";
                            break;
                        //French
                        case 1:
                            label.text = "Equipe";
                            break;
                        //Spanish
                        case 2:
                            label.text = "Equipado";
                            break;
                        //German
                        case 3:
                            label.text = "Ausgestattet";
                            break;
                        //Portagese
                        case 4:
                            label.text = "Equipado";
                            break;
                    }

                }
                else
                {
                    switch (lang)
                    {
                        //English
                        case 0:
                            label.text = "Equip";
                            break;
                        //French
                        case 1:
                            label.text = "Equiper";
                            break;
                        //Spanish
                        case 2:
                            label.text = "Equipar";
                            break;
                        //German
                        case 3:
                            label.text = "Ausstatten";
                            break;
                        //Portagese
                        case 4:
                            label.text = "Equipar";
                            break;
                    }
                }
            }
        }
    }
    public void setInventoryItems(List<Item> items, Transform content, InventoryItemController[] controllersList)
    {
        controllersList = content.GetComponentsInChildren<InventoryItemController>();
        
        for (int i = 0; i < items.Count; i++)
        {
            
            controllersList[i].addItem(items[i]);
        }
    } 
    
    public void setStoreItems()
    {
        storeItemsControllers = storeItemContent.GetComponentsInChildren<InventoryItemController>();
       
        for (int i = 0; i < storeItems.Count; i++)
        {
            storeItemsControllers[i].addItem(storeItems[i]);
        }
    }

    public void sortItems()
    {
        skins.Clear();
        skinSets.Clear();
        accessories.Clear();
        storeItems.Clear();
        foreach (Item item in masterList)
        {
            //sort each item depending on if the player has purchased or not
            if (PlayerPrefs.GetString("Player " + item.name) == "Equipped" ||
                PlayerPrefs.GetString("Player " + item.name) == "Not Equipped")
            {
                if (item.skinSet)
                {
                    skinSets.Add(item);
                }
                else if (item.skin)
                {
                    skins.Add(item);
                }
                else
                {
                    accessories.Add(item);
                }
            }
            else
            {
                if (item.hideInStore != true)
                {
                    storeItems.Add(item);
                }
                
            }
        }
    }
}

