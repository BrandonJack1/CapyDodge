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

    [SerializeField] private  AudioSource source;
    [SerializeField] private  AudioClip purchaseSound;

    private const string REMOVE_ADS = "remove_ad";
    private const string SMALL_COINS = "small_coins";
    private const string MEDIUM_COINS = "medium_coins";
    private const string LARGE_COINS = "large_coins";

    public void InitializePurchasing()
    {
        if (IsInitialized()) { return; }
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
       
        builder.AddProduct(REMOVE_ADS, ProductType.NonConsumable);
        builder.AddProduct(SMALL_COINS, ProductType.Consumable);
        builder.AddProduct(MEDIUM_COINS, ProductType.Consumable);
        builder.AddProduct(LARGE_COINS, ProductType.Consumable);
        UnityPurchasing.Initialize(this, builder);
    }
    
    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    public void BuyRemoveAds()
    {
        BuyProductID(REMOVE_ADS);
    }

    public void BuySmallCoins()
    {
        BuyProductID(SMALL_COINS);
    }
    public void BuyMediumCoins()
    {
        
        BuyProductID(MEDIUM_COINS);
    }
    public void BuyLargeCoins()
    {
        BuyProductID(LARGE_COINS);
    }
    
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (String.Equals(args.purchasedProduct.definition.id, REMOVE_ADS, StringComparison.Ordinal))
        {
            //play purchase sound
            source.PlayOneShot(purchaseSound);
            
            //record that the player bought the remove ads
            PlayerPrefs.SetString("ShowAds", "No");
            
        }
        else if (String.Equals(args.purchasedProduct.definition.id, SMALL_COINS, StringComparison.Ordinal))
        {
            //play purchase sound
            source.PlayOneShot(purchaseSound);
            
            //add 1000 coins to the players balance
            Coins.AddCoins(1000);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, MEDIUM_COINS, StringComparison.Ordinal))
        {
            //play purchase sound
            source.PlayOneShot(purchaseSound);
            
            //add 5000 coins to the players balance
            Coins.AddCoins(5000);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, LARGE_COINS, StringComparison.Ordinal))
        {
            //play purchase sound
            source.PlayOneShot(purchaseSound);
            
            //add 10000 coins to the players balance
            Coins.AddCoins(10000);
        }
        {
            Debug.Log("Purchase Failed");
        }
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
            Product product = m_StoreController.products.WithID(productId);
            if (product != null && product.availableToPurchase)
            {
                m_StoreController.InitiatePurchase(product);
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





