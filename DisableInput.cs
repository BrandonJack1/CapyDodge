using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

public class DisableInput : MonoBehaviour
{ 
    public EventSystem eventSystem;
    void Start()
    {
        if (Application.platform == RuntimePlatform.tvOS)
        {
            eventSystem.GetComponent<InputSystemUIInputModule>().enabled = false;
            eventSystem.GetComponent<EasyInputModule>().enabled = true;
        }
        else
        {
            eventSystem.GetComponent<EasyInputModule>().enabled = false;
            eventSystem.GetComponent<InputSystemUIInputModule>().enabled = true;
        }
    }
}
