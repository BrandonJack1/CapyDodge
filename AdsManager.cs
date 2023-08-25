using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    // Start is called before the first frame update

    public string appleID = "5107550";
    public string androidID = "5107551";

    public string gameID;
    private bool testMode = false;

    public static int turnCount = 0;
    public string interstitalAd = "video";
    public int activeScene;

    public GameObject rewardedError;
    public GameObject continueBtn;
    
    public int switchScene;
    
    public GameObject noThanks;
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
        intializeAds();
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
    public void intializeAds()
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
    
    public void loadInterstialAd()
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
    
    public void loadInterstialAdMenu()
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

    public void loadRewardedVideo()
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

    public void OnInitializationComplete()
    {
        
    }
    
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        //Advertisement.Show(placementId,this);
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

    public void OnUnityAdsShowStart(string placementId)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        throw new System.NotImplementedException();
    }
    
}
