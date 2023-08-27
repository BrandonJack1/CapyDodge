using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    
    public List<Item> masterList = new List<Item>();
    public List<Item> skinSets = new List<Item>();
    public List<Item> skins = new List<Item>();
    public List<Item> accessories = new List<Item>();
    public List<Item> storeItems = new List<Item>();
    
    public Transform storeItemContent;
    public Transform skinSetContent;
    public Transform skinContent;
    public Transform accessoryContent;
    
    [FormerlySerializedAs("InventoryItem")] public GameObject inventoryItem;
    [FormerlySerializedAs("SkinSetItemController")] public InventoryItemController[] skinSetItemController;
    [FormerlySerializedAs("SkinItemController")] public InventoryItemController[] skinItemController;
    [FormerlySerializedAs("AccessoryItemController")] public InventoryItemController[] accessoryItemController;
    public InventoryItemController[] storeItemsControllers;
    
    public static bool relist = false;
    private int lang;

    private void Awake()
    {
        instance = this;
    }
    
    public void Start()
    {
        //set the language
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
        
        SortItems();
        
        //list each item thats in the store items grid
        ListInventoryItems(skinSets, skinSetContent);
        ListInventoryItems(skins, skinContent);
        ListInventoryItems(accessories, accessoryContent);
        ListStoreItems();
        
        SetStoreItems();
        
        //assign items to their respective controllers
        SetInventoryItems(skins, skinContent, skinItemController);
        SetInventoryItems(skinSets, skinSetContent, skinSetItemController);
        SetInventoryItems(accessories, accessoryContent, accessoryItemController);
    }

    private void ListStoreItems()
    {
        foreach (var item in storeItems)
        {
            GameObject obj = Instantiate(inventoryItem, storeItemContent);
            
            //set each variable for the item
            var itemPrice = obj.transform.Find("Price").GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            itemPrice.text = item.price.ToString();
            itemIcon.sprite = item.icon;
            var name = obj.transform.Find("Name").GetComponent<TextMeshProUGUI>();
            name.text = item.storeName;
            var label = obj.transform.Find("Buy Button").transform.Find("Label").GetComponent<TextMeshProUGUI>();
            var type = obj.transform.Find("Type").GetComponent<TextMeshProUGUI>();
            
           label.text = Translation("Buy");
            
            if (item.skinSet)
            {
                type.text = Translation("Skin Set");
            }
            else if (item.skin)
            {
                type.text = Translation("Skin");
            }
            else if (item.accessory)
            {
                type.text = Translation("Accessory");
            }
        }
    }

    private void ListInventoryItems(List<Item> items, Transform transform)
    {
        foreach (var item in items)
        {
            //set each variable for the item
            GameObject obj = Instantiate(inventoryItem, transform);
            var itemPrice = obj.transform.Find("Price").GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var label = obj.transform.Find("Buy Button").transform.Find("Label").GetComponent<TextMeshProUGUI>();
            var name = obj.transform.Find("Name").GetComponent<TextMeshProUGUI>();
            var type = obj.transform.Find("Type").GetComponent<TextMeshProUGUI>();
            
            var coin = obj.transform.Find("Price").transform.Find("Coin").GameObject();
            
            //remove the store aspects of the item for inventory use
            coin.SetActive(false);
            name.text = item.storeName;
            itemPrice.text = "";
            itemIcon.sprite = item.icon;
            
            //set type and equipped status labels
            if (item.skinSet)
            {
                type.text = Translation("Skin Set");
                
                if (PlayerPrefs.GetString("Active Player Skin Set") == item.itemName)
                {
                    label.text = Translation("Equipped");
                }
                else
                {
                    label.text = Translation("Equip");
                }
            }
            else if (item.skin)
            {
                type.text = Translation("Skin");

                if (PlayerPrefs.GetString("Active Player Skin") == item.itemName)
                {
                    label.text = Translation("Equipped");
                }
                else
                {
                    label.text = Translation("Equip");
                }
            }
            else if (item.accessory)
            {
                type.text = Translation("Accessory");
                
                if (PlayerPrefs.GetString("Active Player Accessory") == item.itemName)
                {
                    label.text = Translation("Equipped");

                }
                else
                {
                    label.text = Translation("Equip");
                }
            }
        }
    }
    
    private static void SetInventoryItems(List<Item> items, Transform content, InventoryItemController[] controllersList)
    {
        controllersList = content.GetComponentsInChildren<InventoryItemController>();
        
        for (int i = 0; i < items.Count; i++)
        {
            controllersList[i].AddItem(items[i]);
        }
    }

    private void SetStoreItems()
    {
        storeItemsControllers = storeItemContent.GetComponentsInChildren<InventoryItemController>();
       
        for (int i = 0; i < storeItems.Count; i++)
        {
            storeItemsControllers[i].AddItem(storeItems[i]);
        }
    }

    private void SortItems()
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
    private string Translation(string word)
    {
        string label = "";
        
        if (word == "Equipped")
        {
            switch (lang)
            {
                //English
                case 0:
                    label = "Equipped";
                    break;
                //French
                case 1:
                    label = "Equipe";
                    break;
                //Spanish
                case 2:
                    label = "Equipado";
                    break;
                //German
                case 3:
                    label = "Ausgestattet";
                    break;
                //Portagese
                case 4:
                    label = "Equipado";
                    break;
            }
        }
        else if (word == "Equip")
        {
            switch (lang)
            {
                //English
                case 0:
                    label = "Equip";
                    break;
                //French
                case 1:
                    label = "Equiper";
                    break;
                //Spanish
                case 2:
                    label = "Equipar";
                    break;
                //German
                case 3:
                    label = "Ausstatten";
                    break;
                //Portagese
                case 4:
                    label = "Equipar";
                    break;
            }
            
        }
        else if (word == "Accessory")
        {
            switch (lang)
            {
                //English
                case 0:
                    label = "Accessory";
                    break;
                //French
                case 1:
                    label = "Accessoire";
                    break;
                //Spanish
                case 2:
                    label = "Accesorio";
                    break;
                //German
                case 3:
                    label = "Zubehor";
                    break;
                //Portagese
                case 4:
                    label = "Acessorio";
                    break;
            }
        }
        else if (word == "Skin")
        {
            switch (lang)
            {
                //English
                case 0:
                    label = "Skin";
                    break;
                //French
                case 1:
                    label = "Peau";
                    break;
                //Spanish
                case 2:
                    label = "Piel";
                    break;
                //German
                case 3:
                    label = "Haut";
                    break;
                //Portagese
                case 4:
                    label = "Pele";
                    break;
            }
        }
        else if (word == "Skin Set")
        {
            switch (lang)
            {
                //English
                case 0:
                    label = "Skin Set";
                    break;
                //French
                case 1:
                    label = "Set de peau";
                    break;
                //Spanish
                case 2:
                    label = "Juego de pieles";
                    break;
                //German
                case 3:
                    label = "Skin-Set";
                    break;
                //Portagese
                case 4:
                    label = "Conjunto de pele";
                    break;
            }
        }
        else if (word == "Buy")
        {
            switch (lang)
            {
                //English
                case 0:
                    label = "Buy";
                    break;
                //French
                case 1:
                    label = "Acheter";
                    break;
                //Spanish
                case 2:
                    label = "Comprar";
                    break;
                //German
                case 3:
                    label = "Kaufen";
                    break;
                //Portagese
                case 4:
                    label = "Comprar";
                    break;
            }
        }
        return label;
    }
}

