using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSize : MonoBehaviour
{
    // Start is called before the first frame update

    public static float playerHeight;
    public static float playerWidth;

    public static float acornHeight;
    public static float acornWidth;
    void Start()
    {
        
        //set the default dimensions
        //playerWidth = 0.2481744f;
        //playerHeight = 0.2857763f;
        
        //acornWidth = 0.681005657f;
        //acornHeight = 0.65039885f;
        
        //set the default dimensions
        playerWidth = 0.2481744f;
        playerHeight = 0.2857763f;
            
        acornWidth = 0.681005657f;
        acornHeight = 0.65039885f;

        

        
#if UNITY_IOS
        
        if (UnityEngine.iOS.Device.generation.ToString().Contains("ipad"))
        {
            
            playerWidth = 0.20385161f;
            playerHeight = 0.270000011f;

            acornWidth = 0.498602033f;
            acornHeight = 0.47619307f;
            
        }
      
        
#endif
        
        



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
