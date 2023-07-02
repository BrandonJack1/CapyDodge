using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modes : MonoBehaviour
{

    public GameObject leaderBoard;
    // Start is called before the first frame update
    void Start()
    {

        if (Application.platform != RuntimePlatform.IPhonePlayer)
        {
            leaderBoard.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
