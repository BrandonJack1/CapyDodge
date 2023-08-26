using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Combo : MonoBehaviour
{
    public static bool timerStarted = false;
    public static bool comboActive = false;
    private static bool messageActive;
    
    public static float multiplier= 1f;
    public static float comboTimer;
    public int lang;

    [SerializeField] private GameObject comboMessagePrefab;
    [SerializeField] private GameObject empty;

    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip clip;
    private static readonly int ComboTrigger = Animator.StringToHash("ComboTrigger");

    void Start()
    {
        //set the timer
        comboTimer = 0.8f;
    }

    // Update is called once per frame
    void Update()
    {
        //if the combo is active subtract time
        if (comboActive)
        {
            comboTimer -= Time.deltaTime;
        }
        
        //if the combo reaches 0 before the player collects another acorn
        if (comboTimer <= 0)
        {
            comboActive = false;
            
            //reset timer
            comboTimer = 1f;
            
            //display a bonus message if the player has a high multiplier
            if (multiplier >= 2f)
            {   
                //instantiate message
                GameObject comboMessage = Instantiate(comboMessagePrefab, new Vector3(-12.48f, -1.07f,-0.1f), quaternion.identity );
                comboMessage.transform.SetParent(empty.transform, false);
                
                messageActive = true;
                
                //choose a random message to show
                int rnd = Random.Range(1, 5);
                lang = PlayerPrefs.GetInt("Lang Pref", 0);
                var message = GetMessage(rnd, lang);

                //set the other layer of the text to the same message
                comboMessage.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = message;
                comboMessage.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = message;
                comboMessage.transform.GetComponent<Animator>().SetTrigger(ComboTrigger);
                
                //play the bonus sound effect 
                source.PlayOneShot(clip);
                
                StartCoroutine(ComboMessageActive(1.7f));
                Destroy(comboMessage, 1.4f);
            }
            multiplier = 1f;
        }
    }

    static IEnumerator ComboMessageActive(float secs)
    {
        yield return new WaitForSeconds(secs);
        messageActive = false;
    }

    private string GetMessage(int rnd, int lang)
    {
        string message = "Acorn-Tastic";
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
        return message;
    }
}
