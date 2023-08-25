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

    void Start()
    {
        //set the default dimensions
        PLAYER_WIDTH = 0.28f;
        PLAYER_HEIGHT = 0.28f;

        ACORN_WIDTH = 0.68f;
        ACORN_HEIGHT = 0.65f;

        GIANT_ACORN_WIDTH = 1.02f;
        GIANT_ACORN_HEIGHT = 0.975f;

#if UNITY_IOS

        if (UnityEngine.iOS.Device.generation.ToString().Contains("ipad"))
        {
            PLAYER_WIDTH = 0.20f;
            PLAYER_HEIGHT = 0.25f;

            ACORN_WIDTH = 0.5f;
            ACORN_HEIGHT = 0.47f;

        }
#endif
    }
}
