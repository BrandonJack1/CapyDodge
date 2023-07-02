using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EasterEgg : MonoBehaviour
{
    public GameObject imgCapy;
    public GameObject imgCopper;
    public GameObject character;
    public Sprite copper;
    public int counter;
    public TextMeshProUGUI message;
    void Start()
    {

        
        if (PlayerPrefs.GetString("Copper", "No") == "Yes" && SceneManager.GetActiveScene().buildIndex == 2)
        {
            character.gameObject.GetComponent<SpriteRenderer>().sprite = copper;
        }
        else if(PlayerPrefs.GetString("Copper", "No") == "Yes")
        {
            imgCapy.SetActive(false);
            imgCopper.SetActive(true);
        }

    }

    public void btnPress()
    {

        if (counter >= 10)
        {
            if (PlayerPrefs.GetString("Copper", "No") == "Yes")
            {
                PlayerPrefs.SetString("Copper", "No");
                
            }
            else if (PlayerPrefs.GetString("Copper", "No") == "No")
            {
                PlayerPrefs.SetString("Copper", "Yes");
                
            }

            counter = 0;
        }
        counter += 1;

    }

    public void btnPressBeta()
    {
        if (counter >= 10)
        {
            PlayerPrefs.SetString("Beta", "Yes");
            PlayerPrefs.SetString("Player Cone", "Not Equipped");
            message.text = "Thanks for Beta Testing! Enjoy the traffic cone hat :)";
            
        }
        counter += 1;
        
    }
}
