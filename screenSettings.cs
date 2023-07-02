using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenSettings : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enable120Hz()
    {

        Application.targetFrameRate = 120;

    }

    public void enable60Hz()
    {

        Application.targetFrameRate = 60;
    }

    public void enable30Hz()
    {

        Application.targetFrameRate = 30;
    }
}
