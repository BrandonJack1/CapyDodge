using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{

    public TextMeshProUGUI timer;
    public static float privateTimer;
    public static int minuteCount;
    public static float secondsCount;


    public int netSpawnTime = 25;
    
    public TextMeshProUGUI netTimer;
    public static float netPrivateTimer;
    public static float netSecondsCount;

    public GameObject timerUI;

    private int hourCount;
    // Start is called before the first frame update
    void Start()
    {
        privateTimer = 0.0f;
        secondsCount = 0.0f;
        minuteCount = 0;

        netSecondsCount = 25f;
        //netPrivateTimer = 25f;

    }

    // Update is called once per frame
    void Update()
    {
        updateTimerUI();
        updateNetUI();
        privateTimer += Time.deltaTime;
        netPrivateTimer -= Time.deltaTime;

        
        if (netSecondsCount >= 0)
        {
            timerUI.SetActive(true);
        }
    }

    public void updateTimerUI()
    {
        secondsCount += Time.deltaTime;

        string secZero;

        if (secondsCount < 10)
        {
            secZero = "0";
        }
        else
        {
            secZero = "";
        }
        timer.text = minuteCount + ":" + secZero + (int)secondsCount;
        if ((double) secondsCount >= 60.0)
        {
            ++minuteCount;
            secondsCount = 0.0f;
        }
        else
        {
            if (minuteCount < 60)
                return;
            ++hourCount;
            minuteCount = 0;
        }

    }
    
    public void updateNetUI()
    {
        if (netSecondsCount <= 0)
        {
            if (PowerUp.powerUpPresent == false)
            {
                timerUI.SetActive(false);
                PowerUp.activatePowerUp = true;
            }
        }
        else
        {
            
            netSecondsCount -= Time.deltaTime;

            string secZero;

            if (netSecondsCount < 10)
            {
                secZero = "0";
            }
            else
            {
                secZero = "";
            }

            netTimer.text = "Next net in " + secZero + (int)netSecondsCount + " seconds";
        }
        
    }

    IEnumerator showTimer()
    {

        yield return new WaitForSeconds(10);
        netTimer.gameObject.SetActive(true);
        netSecondsCount = 25f;

    }
}
