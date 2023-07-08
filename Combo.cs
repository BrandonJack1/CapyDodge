using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Combo : MonoBehaviour
{
    // Start is called before the first frame update
    public static float comboTimer;
    public static bool timerStarted = false;
    public static bool comboActive = false;
    public static float multiplier= 1f;

    public GameObject comboMessagePrefab;
    public GameObject empty;

    public AudioSource source;
    public AudioClip clip;

    public static bool messageActive;
    public int lang;
    
    
    
    //1. Check if combo is active and the timer is not under 0, if the timer goes under 0 set the combo to be unactive
    //2. if its not active, dont do anything
    //3. if the combo is active increase the points and reset the timer
    
    void Start()
    {
        comboTimer = 0.8f;
    }

    // Update is called once per frame
    void Update()
    {

        if (comboActive)
        {
            comboTimer -= Time.deltaTime;
        }

        if (comboTimer <= 0)
        {
            comboActive = false;
            comboTimer = 0.8f;

            if (multiplier >= 2f)
            {
                
                GameObject comboMessage = Instantiate(comboMessagePrefab, new Vector3(-12.48f, -1.07f,-0.1f), quaternion.identity );
                comboMessage.transform.SetParent(empty.transform, false);
                
                messageActive = true;

                int rnd = Random.Range(1, 5);
                string message;
                lang = PlayerPrefs.GetInt("Lang Pref", 0);
                message = "Acorn-tastic";
                switch (rnd)
                {
                    case 1:
                        switch (lang)
                        {
                            //English
                            case 0:
                                message = "Amazing";
                                break;
                            //French
                            case 1:
                                message = "Incroyable";
                                break;
                            //Spansih
                            case 2:
                               message = "Asombroso";
                                break;
                            //German
                            case 3:
                                message = "Erstaunlich";
                                break;
                            //Portagese
                            case 4:
                                message = "Incrivel";
                                break;
                        }
                        break;
                    case 2:
                        switch (lang)
                        {
                            //English
                            case 0:
                                message = "Acorn-tastic";
                                break;
                            //French
                            case 1:
                                message = "Gland-Tastique";
                                break;
                            //Spansih
                            case 2:
                                message = "Bellota-Tastica";
                                break;
                            //German
                            case 3:
                                message = "Eichel-Tastisch";
                                break;
                            //Portagese
                            case 4:
                                message = "Bolota-Tastica";
                                break;
                        }
                        break;
                    case 3:
                        switch(lang)
                        {
                            //English
                            case 0:
                                message="Unbelievable";
                                break;
                            //French
                            case 1:
                                message="Incroyable";
                                break;
                            //Spansih
                            case 2:
                                message="Increible";
                                break;
                            //German
                            case 3:
                                message="Unglaublich";
                                break;
                            //Portagese
                            case 4:
                                message="Inacreditavel";
                                break;
                        }
                        break;
                    case 4:
                        switch(lang)
                        {
                            //English
                            case 0:
                                message="Awesome";
                                break;
                            //French
                            case 1:
                                message="Genial";
                                break;
                            //Spansih
                            case 2:
                                message="Impresionante";
                                break;
                            //German
                            case 3:
                                message="Fantastisch";
                                break;
                            //Portagese
                            case 4:
                                message="Incrivel";
                                break;
                        }
                        break;
                    default:
                        switch (lang)
                        {
                            //English
                            case 0:
                                message = "Acorn-tastic";
                                break;
                            //French
                            case 1:
                                message = "Gland-Tastique";
                                break;
                            //Spansih
                            case 2:
                                message = "Bellota-Tastica";
                                break;
                            //German
                            case 3:
                                message = "Eichel-Tastisch";
                                break;
                            //Portagese
                            case 4:
                                message = "Bolota-Tastica";
                                break;
                        }
                        break;
                        
                }

                comboMessage.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = message;
                comboMessage.transform.GetChild(0).GetComponent<Animator>().SetTrigger("ComboTrigger");
                source.PlayOneShot(clip);
                
                StartCoroutine(comboMessageActive(1.7f));
                Destroy(comboMessage, 1.6f);
            }
            
            multiplier = 1f;
            //multiplier += 0.25f;

        }
    }
    
    

    IEnumerator comboMessageActive(float secs)
    {
        yield return new WaitForSeconds(secs);
        messageActive = false;

    }
}
