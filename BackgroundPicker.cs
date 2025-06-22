using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundPicker : MonoBehaviour
{
    [SerializeField] private GameObject background;
    [SerializeField] private Sprite bg1;
    [SerializeField] private Sprite bg2;
    
    public static int activeBG;
    
    // Start is called before the first frame update
    void Start()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        
        //if its the menu or game, choose a random background
        if (scene == 1 || scene == 2)
        {
            int bg = Random.Range(0, 2);
            if (bg == 0)
            {
                background.GetComponent<SpriteRenderer>().sprite = bg1;
                activeBG = 1;
            }
            else
            {
                background.GetComponent<SpriteRenderer>().sprite = bg2;
                activeBG = 2;
            }
        }
        //if its not the menu or game, choose the background that was previously used so it matches the previous screen
        else
        {
            if (activeBG == 1)
            {
                background.GetComponent<SpriteRenderer>().sprite = bg1;
            }
            else
            {
                background.GetComponent<SpriteRenderer>().sprite = bg1;
            }
        }
        ScaleToFitScreen.refresh = true;
        MainMenuPlayer.playSound = true;
    }
}
