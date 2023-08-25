using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    private string appleID = "5107550";
    private string androidID = "5107551";
    private string gameID;
    private string interstitalAd = "video";
    
    [SerializeField] private GameObject rewardedError;
    [SerializeField] private GameObject continueBtn;
    
    private bool testMode = false;

    public static int turnCount = 0;
    private int activeScene;
    private int switchScene;
    
    void Start()
    {
        //set the game id based on the device
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            gameID = appleID;
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            gameID = androidID;
        }
        
#if UNITY_EDITOR
        
        //set game id to apple id when its the editor to prevent errors
        gameID = appleID;
        
#endif
        IntializeAds();
        LoadBannerAd();
        
        //load ads
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            Advertisement.Load("Rewarded_iOS");
            Advertisement.Load("Interstitial_iOS");
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            Advertisement.Load("Interstitial_Android");
            Advertisement.Load("Rewarded_Android");
        }
    }
    
    public void IntializeAds()
    {
        Advertisement.Initialize(gameID, testMode, this);
    }

    // Update is called once per frame
    void Update()
    {
        activeScene = SceneManager.GetActiveScene().buildIndex;
        
        //hide the banner if its not the game over screen
        if (activeScene != 3)
        {
            Advertisement.Banner.Hide();
        }
    }
    
    public void LoadInterstialAd()
    {
        //show the add every 2 retries if they have not removed ads
        if (turnCount >= 1 && PlayerPrefs.GetString("ShowAds", "Yes") == "Yes")
        {
            switchScene = 2;
            turnCount = 0;
            
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                Advertisement.Show("Interstitial_iOS",this);
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
                Advertisement.Show("Interstitial_Android",this);
            }
            
#if UNITY_EDITOR
            Advertisement.Show("Interstitial_iOS",this);
#endif
        }
        else
        {
            turnCount++;
            SceneManager.LoadScene(2);
        }
    }
    
    public void LoadInterstialAdMenu()
    {
        //show the add every 2 retries if they have not removed ads
        if (turnCount >= 1 && PlayerPrefs.GetString("ShowAds", "Yes") == "Yes")
        {
            switchScene = 1;
            turnCount = 0;
            
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                Advertisement.Show("Interstitial_iOS",this);
            }
            else if (Application.platform == RuntimePlatform.Android)
            {
                Advertisement.Show("Interstitial_Android",this);
            }
            
#if UNITY_EDITOR
            Advertisement.Show("Interstitial_iOS",this);
#endif
        }
        else
        {
            turnCount++;
            SceneManager.LoadScene(1);
        }
    }

    public void LoadRewardedVideo()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer && PlayerPrefs.GetString("ShowAds", "Yes") == "Yes")
        {
            Advertisement.Show("Rewarded_iOS", this);
        }
        else if (Application.platform == RuntimePlatform.Android && PlayerPrefs.GetString("ShowAds", "Yes") == "Yes")
        {
            Advertisement.Show("Rewarded_Android", this);
        }
        
#if UNITY_EDITOR
        Advertisement.Show("Rewarded_iOS",this);
#endif
    }
    
    //legacy method
    public void LoadBannerAd()
    {
        
    }
    
    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId.Equals("Rewarded_iOS") && UnityAdsShowCompletionState.COMPLETED.Equals(showCompletionState))
        {
            Continue.adContinue = true;
        }
        else if (placementId.Equals("Interstitial_iOS"))
        {
            SceneManager.LoadScene(switchScene);
            turnCount = 0;
        }
        else if (placementId.Equals("Rewarded_Android") && UnityAdsShowCompletionState.COMPLETED.Equals(showCompletionState))
        {
            Continue.adContinue = true;
        }
        else if (placementId.Equals("Interstitial_Android"))
        {
            SceneManager.LoadScene(2);
            turnCount = 0;
        }
        Time.timeScale = 1;
    }
    
    void OnBannerLoaded()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            Advertisement.Banner.Show("Banner_iOS");
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            Advertisement.Banner.Show("Banner_Android");
        }
    }
    
    //legacy method
    public void OnInitializationComplete()
    {
        
    }
    
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        throw new System.NotImplementedException();
    }
    
    //legacy method
    public void OnUnityAdsAdLoaded(string placementId)
    {
        
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        if (placementId == "Rewarded_iOS")
        { 
            rewardedError.SetActive(true);
            continueBtn.SetActive(false);
        }
    }
    
    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        throw new System.NotImplementedException();
    }
    
    //legacy method
    public void OnUnityAdsShowStart(string placementId)
    {
        
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        throw new System.NotImplementedException();
    }
    
}
