using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSettings : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }
    
    public void Enable120Hz()
    {
        Application.targetFrameRate = 120;
    }

    public void Enable60Hz()
    {
        Application.targetFrameRate = 60;
    }

    public void Enable30Hz()
    {
        Application.targetFrameRate = 30;
    }
}
