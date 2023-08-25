using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UIElements;

public class InventoryItemController : MonoBehaviour
{ 
  public AudioSource source;
  public AudioClip chaching;
  public AudioClip invalid;
  public Item item;
  
  public void addItem(Item newItem)
  {
      item = newItem;
  }
  
  public void buy()
  {
      //if its already purchased
    
    InventoryManager.Instance.ListItems();
    
    if (PlayerPrefs.GetString("Player " + item.itemName) != "Equipped" &&
        PlayerPrefs.GetString("Player " + item.itemName) != "Not Equipped")
    {
        if (PlayerPrefs.GetInt("Coins") >= item.price)
        {
            
            Coins.SubtractCoins(item.price);
            PlayerPrefs.SetString("Player " + item.itemName, "Not Equipped");
            Store.playPurchaseSound = true;
            InventoryManager.Instance.ListItems();
        }
        else
        {
            Store.playInvalidSound = true;
        }
    }
    else if (item.skinSet && PlayerPrefs.GetString("Active Player Skin Set") == item.itemName)
    {
        
        PlayerPrefs.SetString("Player " + item.itemName, "Not Equipped");
        PlayerPrefs.SetString("Active Player Skin Set", "");
        PlayerPrefs.SetString("Active Skin Set", "");
        InventoryManager.Instance.ListItems();
    }
    else if (item.skin && PlayerPrefs.GetString("Active Player Skin") == item.itemName)
    {
        
        PlayerPrefs.SetString("Player " + item.itemName, "Not Equipped");
        PlayerPrefs.SetString("Active Player Skin", "");
        PlayerPrefs.SetString("Active Skin", "");
        InventoryManager.Instance.ListItems();
    }
    else if (item.accessory && PlayerPrefs.GetString("Active Player Accessory") == item.itemName)
    {
        
        PlayerPrefs.SetString("Player " + item.itemName, "Not Equipped");
        PlayerPrefs.SetString("Active Player Accessory", "");
        PlayerPrefs.SetString("Active Accessory", "");
        InventoryManager.Instance.ListItems();
    }
    else if (item.skinSet)
    {
        PlayerPrefs.SetString("Active Player Skin Set", item.itemName);
        PlayerPrefs.SetString("Player " + item.itemName, "Equipped");
        InventoryManager.Instance.ListItems();
        
        
        PlayerPrefs.SetString("Active Player Accessory", "");
        PlayerPrefs.SetString("Active Player Skin", "");
        InventoryManager.Instance.ListItems();
        
        
    }
    else if (item.skin)
    {
        PlayerPrefs.SetString("Active Player Skin", item.itemName);
        PlayerPrefs.SetString("Player " + item.itemName, "Equipped");
        InventoryManager.Instance.ListItems();
        
        PlayerPrefs.SetString("Active Player Skin Set", "");
        InventoryManager.Instance.ListItems();
        
    }
    else if (item.accessory)
    {
        PlayerPrefs.SetString("Active Player Accessory", item.itemName);
        PlayerPrefs.SetString("Player " + item.itemName, "Equipped");
        InventoryManager.Instance.ListItems();
        
        PlayerPrefs.SetString("Active Player Skin Set", "");
        InventoryManager.Instance.ListItems();
        
    }
  }
}
