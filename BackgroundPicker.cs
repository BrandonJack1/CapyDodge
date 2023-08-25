using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundPicker : MonoBehaviour
{
    public GameObject background;

    public Sprite bg1;
    public Sprite bg2;
    
    public static int activeBG;
    
    // Start is called before the first frame update
    void Start()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;

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
        else
        {
            if (activeBG == 1)
            {
                background.GetComponent<SpriteRenderer>().sprite = bg1;
            }
            else
            {
                background.GetComponent<SpriteRenderer>().sprite = bg2;
            }
        }
        ScaleToFitScreen.refresh = true;
        MainMenuPlayer.playSound = true;
    }
}
