using UnityEngine;
using UnityEngine.SocialPlatforms;    
public class GameCenter : MonoBehaviour 
{
    void Start () 
    {
        //if the device is an iOS device, authenticate user
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            Social.localUser.Authenticate (ProcessAuthentication);
            
            //put the users high score on the leaderboard
            int highScore = PlayerPrefs.GetInt("High Score", 0);
            Social.ReportScore(highScore,"1",HighScoreCheck);
        }
    }

    void ProcessAuthentication (bool success) 
    {
        if (success) 
        {
            Debug.Log ("Authentication successful");
            Social.CreateLeaderboard();
            Social.CreateLeaderboard().id = "1";
            
        }
        else
        {
            Debug.Log ("Failed to authenticate");
        }
    }
    
    static void HighScoreCheck(bool result) 
    {
        if(result)
            Debug.Log("score submission successful");
        else
            Debug.Log("score submission failed");
    }
}