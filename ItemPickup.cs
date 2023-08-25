using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
  public Item item;
  
  void pickup()
  {
    //InventoryManager.Instance.Add(item);
    //Destroy(gameObject);
    
  }

  private void OnMouseDown()
  {
    pickup();
  }
}
