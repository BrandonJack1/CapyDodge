using UnityEngine;

public class RestorePurchases : MonoBehaviour
{
    public void ClickRestorePurchaseButton() {
        IAPManager.instance.RestorePurchases();
    }
}
