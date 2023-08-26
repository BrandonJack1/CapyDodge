using System;
using TMPro;
using Unity.VisualScripting;
//using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Purchasing;
using Product = UnityEngine.Purchasing.Product;


public class IAPManager : MonoBehaviour, IStoreListener
{
    
    
public static IAPManager instance;

    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;

    public AudioSource source;
    public AudioClip purchaseSound;
    
    private string removeAds = "remove_ad";
    private string smallCoins = "small_coins";
    private string mediumCoins = "medium_coins";
    private string largeCoins = "large_coins";
    
    public void InitializePurchasing()
    {
        if (IsInitialized()) { return; }
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        //Step 2 choose if your product is a consumable or non consumable
        builder.AddProduct(removeAds, ProductType.NonConsumable);
        builder.AddProduct(smallCoins, ProductType.Consumable);
        builder.AddProduct(mediumCoins, ProductType.Consumable);
        builder.AddProduct(largeCoins, ProductType.Consumable);
        UnityPurchasing.Initialize(this, builder);
    }
    
    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    public void BuyRemoveAds()
    {
        Debug.Log("Purchase Button Pressed");
        BuyProductID(removeAds);
    }

    public void BuySmallCoins()
    {
        BuyProductID(smallCoins);
    }
    public void BuyMediumCoins()
    {
        
        BuyProductID(mediumCoins);
    }
    public void BuyLargeCoins()
    {
        
        BuyProductID(largeCoins);
    }
    
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        Debug.Log("Starting purchase prcoess result");
        if (String.Equals(args.purchasedProduct.definition.id, removeAds, StringComparison.Ordinal))
        {
            Debug.Log("Purchase was success");
            source.PlayOneShot(purchaseSound);
            PlayerPrefs.SetString("ShowAds", "No");
            
        }
        else if (String.Equals(args.purchasedProduct.definition.id, smallCoins, StringComparison.Ordinal))
        {
            source.PlayOneShot(purchaseSound);
            Coins.AddCoins(1000);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, mediumCoins, StringComparison.Ordinal))
        {
            source.PlayOneShot(purchaseSound);
            Coins.AddCoins(5000);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, largeCoins, StringComparison.Ordinal))
        {
            source.PlayOneShot(purchaseSound);
            Coins.AddCoins(10000);
        }
        {
            Debug.Log("Purchase Failed");
        }
        
        Debug.Log("Returning result");
        return PurchaseProcessingResult.Complete;
    }
    
    private void Awake()
    {
        TestSingleton();
    }

    void Start()
    {
        if (m_StoreController == null) { InitializePurchasing(); }
        InitializePurchasing();
    }

    private void TestSingleton()
    {
        if (instance != null) { Destroy(gameObject); return; }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            Debug.Log("Intialized, starting purchase");
            Product product = m_StoreController.products.WithID(productId);
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                m_StoreController.InitiatePurchase(product);
                Debug.Log("intitiate purchase complete");
            }
            else
            {
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        else
        {
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

    public void RestorePurchases()
    {
        if (!IsInitialized())
        {
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            Debug.Log("RestorePurchases started ...");

            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            apple.RestoreTransactions((result) => {
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
        else
        {
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("OnInitialized: PASS");
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
    
}





