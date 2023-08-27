using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem",menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public string storeName;
    public int price;
    public Sprite icon;
    public bool purchased;
    public bool hideInStore;
    public bool limitedTime;
    public bool skinSet;
    public bool skin;
    public bool accessory;
}