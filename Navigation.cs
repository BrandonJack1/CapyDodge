using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms; 

public class Navigation : MonoBehaviour
{
    [SerializeField] private RectTransform fader;
    
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 3)
        {
            //Set fader active
            fader.gameObject.SetActive(true);
            
            //Set fade size object to cover screen and scale it down to 0
            LeanTween.scale(fader, new Vector3(1, 1, 1), 0);
            LeanTween.scale(fader, Vector3.zero, 0.3f).setOnComplete(() => { fader.gameObject.SetActive(false); });
        }
    }
    public void Play()
    {
        ChangeScene(2);
    }

    public void Store()
    {
        ChangeScene(4);
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        ChangeScene(1);
    }

    public void Settings()
    {
        ChangeScene(5);
    }
    
    public void ShowHighScores()
    {
        Social.ShowLeaderboardUI();
    }

    public void Modes()
    {
       ChangeScene(6);
    }

    private void ChangeScene(int sceneNum)
    {   
        //set black fade object active
        fader.gameObject.SetActive(true);
        
        //scale fade object until it covers screen
        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, new Vector3(1,1,1), 0.3f).setOnComplete(() =>
        {
            SceneManager.LoadScene(sceneNum);

        });
    }
}
