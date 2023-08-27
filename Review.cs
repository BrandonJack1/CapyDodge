using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Review : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //if the player is on the main menu
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            //increase the games played count when on the game over screen
            int total = PlayerPrefs.GetInt("GamesPlayed") + 1;
            PlayerPrefs.SetInt("GamesPlayed", total);

        }
        
        //if its an iOS device and the player has played more than 2 games, ask for a review on the main menu
#if UNITY_IOS
        if (SceneManager.GetActiveScene().buildIndex == 1 && PlayerPrefs.GetInt("GamesPlayed") >=2 && PlayerPrefs.GetString("Review", "No") != "Yes")
        {
            Debug.Log("Review Pop Up");
            UnityEngine.iOS.Device.RequestStoreReview();
            PlayerPrefs.SetString("Review", "Yes");
        }
        
#endif
    }
}
