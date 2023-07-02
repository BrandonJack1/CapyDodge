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
            fader.gameObject.SetActive(true);
            LeanTween.scale(fader, new Vector3(1, 1, 1), 0);
            LeanTween.scale(fader, Vector3.zero, 0.3f).setOnComplete(() => { fader.gameObject.SetActive(false); });
        }

    }
    public void play()
    {
        
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, new Vector3(1,1,1), 0.3f).setOnComplete(() =>
        {
            SceneManager.LoadScene(2);

        });
    }

    public void store()
    {
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, new Vector3(1,1,1), 0.3f).setOnComplete(() =>
        {
            SceneManager.LoadScene(4);

        });
    }

    public void menu()
    {
        Time.timeScale = 1f;
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, new Vector3(1,1,1), 0.3f).setOnComplete(() =>
        {
            SceneManager.LoadScene(1);

        });
    }

    public void settings()
    {
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, new Vector3(1,1,1), 0.3f).setOnComplete(() =>
        {
            SceneManager.LoadScene(5);

        });
    }
    
    public void showHighScores()
    {
        Social.ShowLeaderboardUI();
    }

    public void modes()
    {
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, new Vector3(1,1,1), 0.3f).setOnComplete(() =>
        {
            SceneManager.LoadScene(6);

        });
        
    }
}
