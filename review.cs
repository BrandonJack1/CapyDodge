using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class review : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            int total = PlayerPrefs.GetInt("GamesPlayed") + 1;
            PlayerPrefs.SetInt("GamesPlayed", total);

        }
#if UNITY_IOS
        if (SceneManager.GetActiveScene().buildIndex == 1 && PlayerPrefs.GetInt("GamesPlayed") >=2 && PlayerPrefs.GetString("Review", "No") != "Yes")
        {
            
            Debug.Log("Review Pop Up");
            UnityEngine.iOS.Device.RequestStoreReview();
            PlayerPrefs.SetString("Review", "Yes");

        }
        
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
