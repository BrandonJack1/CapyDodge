using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonalSkin : MonoBehaviour
{

    public Transform grassParent;
    public Transform bushParent;

    public Sprite seasonalGrass;

    // Start is called before the first frame update
    void Start()
    {

        //Valentines
        if (PlayerPrefs.GetString("Seasonal", "No") == "Yes")
        {
            //PlayerPrefs.SetString("Valentines", "Yes");

            foreach (Transform child in grassParent)
            {
                //child.gameObject.GetComponent<SpriteRenderer>().sprite = seasonalGrass;
            }
            foreach (Transform child in bushParent)
            {
                //child.gameObject.GetComponent<SpriteRenderer>().sprite = valentinesBush;
            }
        }
        else if (PlayerPrefs.GetString("Valentines", "No") == "No")
        {
            //PlayerPrefs.SetString("Valentines", "No");
            
            foreach (Transform child in grassParent)
            {
                //child.gameObject.GetComponent<SpriteRenderer>().sprite = normalGrass;
            }
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
