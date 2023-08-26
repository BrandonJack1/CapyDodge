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
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip chaching;
    [SerializeField] private AudioClip invalid;
    [SerializeField] private Item item;

    public void AddItem(Item newItem)
    {
        item = newItem;
    }

    public void Buy()
    {
        InventoryManager.instance.ListItems();

        //if the player clicks on item and does not own the item 
        if (PlayerPrefs.GetString("Player " + item.itemName) != "Equipped" &&
            PlayerPrefs.GetString("Player " + item.itemName) != "Not Equipped")
        {
            //if the player has enough coins
            if (PlayerPrefs.GetInt("Coins") >= item.price)
            {
                //subtract the amount from the players balance
                Coins.SubtractCoins(item.price);

                //list that the player owns the item by setting it as not equipped
                PlayerPrefs.SetString("Player " + item.itemName, "Not Equipped");
                Store.playPurchaseSound = true;

                //refresh inventory list now that this item has been added
                InventoryManager.instance.ListItems();
            }
            else
            {
                //if player does not have enough, play invalid sound
                Store.playInvalidSound = true;
            }
        }
        //if the player clicks on an owned item and it is a skin set and currently equipped
        else if (item.skinSet && PlayerPrefs.GetString("Active Player Skin Set") == item.itemName)
        {
            PlayerPrefs.SetString("Player " + item.itemName, "Not Equipped");
            PlayerPrefs.SetString("Active Player Skin Set", "");
            PlayerPrefs.SetString("Active Skin Set", "");
        }
        //if the player clicks on an owned item and it is a skin and currently equipped
        else if (item.skin && PlayerPrefs.GetString("Active Player Skin") == item.itemName)
        {
            PlayerPrefs.SetString("Player " + item.itemName, "Not Equipped");
            PlayerPrefs.SetString("Active Player Skin", "");
            PlayerPrefs.SetString("Active Skin", "");
        }
        //if the player clicks on an owned item and it is an accessory and currently equipped
        else if (item.accessory && PlayerPrefs.GetString("Active Player Accessory") == item.itemName)
        {
            PlayerPrefs.SetString("Player " + item.itemName, "Not Equipped");
            PlayerPrefs.SetString("Active Player Accessory", "");
            PlayerPrefs.SetString("Active Accessory", "");
        }
        //if the player clicks on a owned item that is a skin set and not equipped
        else if (item.skinSet)
        {
            PlayerPrefs.SetString("Active Player Skin Set", item.itemName);
            PlayerPrefs.SetString("Player " + item.itemName, "Equipped");
            PlayerPrefs.SetString("Active Player Accessory", "");
            PlayerPrefs.SetString("Active Player Skin", "");
        }
        //if the player clicks on a owned item that is a skin and not equipped
        else if (item.skin)
        {
            PlayerPrefs.SetString("Active Player Skin", item.itemName);
            PlayerPrefs.SetString("Player " + item.itemName, "Equipped");
            PlayerPrefs.SetString("Active Player Skin Set", "");
        }
        //if the player clicks on a owned item that is a accessory and not equipped
        else if (item.accessory)
        {
            PlayerPrefs.SetString("Active Player Accessory", item.itemName);
            PlayerPrefs.SetString("Player " + item.itemName, "Equipped");
            PlayerPrefs.SetString("Active Player Skin Set", "");
        }
        
        InventoryManager.instance.ListItems();
    }
}

  