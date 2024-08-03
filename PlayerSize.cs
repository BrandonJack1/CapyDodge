using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSize : MonoBehaviour
{
    // Start is called before the first frame update

    public static float PLAYER_HEIGHT;
    public static float PLAYER_WIDTH;

    public static float ACORN_HEIGHT;
    public static float ACORN_WIDTH;

    public static float GIANT_ACORN_HEIGHT;
    public static float GIANT_ACORN_WIDTH;
    
    public static float APPLE_WIDTH;
    public static float APPLE_HEIGHT;
        

    void Start()
    {
        //set the default dimensions
        PLAYER_WIDTH = 0.28f;
        PLAYER_HEIGHT = 0.28f;
        
        ACORN_WIDTH = 0.68f;
        ACORN_HEIGHT = 0.65f;
        
        GIANT_ACORN_WIDTH = 1.02f;
        GIANT_ACORN_HEIGHT = 0.975f;

        APPLE_WIDTH = 0.36f;
        APPLE_HEIGHT = 0.44f;
        

#if UNITY_IOS

        var identifier = SystemInfo.deviceModel;
        if (identifier.StartsWith("iPad", StringComparison.Ordinal))
        {
            ACORN_WIDTH = 0.55f;
            ACORN_HEIGHT = 0.52f;
            
            
            GIANT_ACORN_WIDTH = 0.8f;
            GIANT_ACORN_HEIGHT = 0.8f;
            
        }
       
#endif
    }
}
