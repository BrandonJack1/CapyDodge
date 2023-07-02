//////////inside game.cs/////////////////////////////////

using UnityEngine;
using UnityEngine.SocialPlatforms;    
public class GameCenter : MonoBehaviour 
{

    void Start () 
    {
        
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            Social.localUser.Authenticate (ProcessAuthentication);

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