using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class SlowTime : MonoBehaviour
{
    public static int offset;
    private static int spawnRate;

    [SerializeField] private GameObject warningCountdown;
    [SerializeField] private GameObject powerUp;
    [SerializeField] private Camera mainCamera;
    
    private float leftOffset;
    private float rightOffset;
    public static Stopwatch timer;
    
    public static bool active = false;
    public static bool inArea = false;
    
    void Start()
    {
        //set initial spawnRate
        spawnRate = 10;
        
        //spawn offsets for screens
        leftOffset = Camera.main.pixelWidth / 30;
        rightOffset = Camera.main.pixelWidth / 20;

        //start the spawn timer
        timer = new Stopwatch();
        timer.Start();
        offset = Random.Range(20, 35);
        
        //dont show the slowdonw countdown upon starting
        warningCountdown.gameObject.SetActive(false);

        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        //TimeSpan ts = timer.Elapsed;

        //once the timer reaches 10 + the offset
        //if (ts.Seconds >= spawnRate + offset)
        {
            //stop and reset the timer
            timer.Stop();
            timer.Reset();
            
            //Spawn power up
            //spawnPowerUp();
        }
    }
    
    void SpawnPowerUp()
    {
        float powerUpPos = Random.Range(mainCamera.ScreenToWorldPoint(new Vector2(0 + leftOffset, 0)).x, mainCamera.ScreenToWorldPoint(new Vector2(Screen.width - rightOffset, 0)).x);
        
        //Create a new acorn to spawn
        GameObject powerUpClone = Instantiate<GameObject>(powerUp, new Vector3(powerUpPos, 5.5f, 0), Quaternion.identity);
        Rigidbody2D rb = powerUpClone.GetComponent<Rigidbody2D>();

        inArea = true;
        //add bounce
        rb.AddTorque(-0.3f, ForceMode2D.Force);
        
        //create random forces for the acorns when spawning in 
        float xForce = Random.Range(-30, 30);
        xForce = xForce / 10;
        float yForce = Random.Range(-30, 30);
        yForce = yForce / 10;
        
        //add the force
        rb.AddForce(new Vector2(xForce,yForce),ForceMode2D.Impulse);

    }
}
