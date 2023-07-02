using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseButton : MonoBehaviour
{

    public enum PurchaseType {removeAds};

    public PurchaseType purchaseType;


    public void clickPurchaseButton()
    {
        switch (purchaseType)
        {
            case PurchaseType.removeAds:
                IAPManager.instance.buyRemoveAds();
                break;
            
        }
        
    }
}
