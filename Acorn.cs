using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Acorn : MonoBehaviour
{
    public GameObject acorn;
    public GameObject particle;


    public GameObject goldenAcorn;
    public static bool goldenActive = false;

    public static float spawnRate = 4f;
    public float leftOffset;
    public float rightOffset;
    
    public bool speed1 = false;
    public bool speed2 = false;
    public bool speed3 = false;
    public bool speed4 = false;
    public bool speed5 = false;
    public bool speed6 = false;
    public bool speed7 = false;
    public bool speed8 = false;
    public bool speed9 = false;
    public bool speed10 = false;
    
    public AudioSource source;
    public AudioClip leaves;
    public AudioClip speedingUp;
    
    public TextMeshProUGUI speedText;
    private ParticleSystem particles;
    public Camera mainCamera;

    public float acornWidth;
    public float acornHeight;

    public Sprite easterEgg1;
    public Sprite easterEgg2;
    public Sprite easterEgg3;

    public static bool slowActive;
    
    void Start()
    {
        spawnRate = 4f;
        //spawn acorns in relation to the spawn rate
        InvokeRepeating("spawnAcorn", 1, spawnRate);

        goldenActive = false;
        
        //used to spawn within the trees
        leftOffset = Camera.main.pixelWidth / 30;
        rightOffset = Camera.main.pixelWidth / 10;


        acornWidth = PlayerSize.acornWidth;
        acornHeight = PlayerSize.acornHeight;

        particles = particle.GetComponent<ParticleSystem>();

        spawnRate = 4f;
    }

    // Update is called once per frame
    void Update()
    {
        //adjust the spawn speed
        if (Score.score >= 750 && speed1 == false)
        {
            increaseSpeed(1,3.5f);
        }
        else if (Score.score >= 1500 && speed2 == false)
        {
            increaseSpeed(2,3f);
        }
        else if (Score.score >= 2250 && speed3 == false)
        {
            increaseSpeed(3,2.5f);
        }
        else if (Score.score >= 3000 && speed4 == false)
        {
            increaseSpeed(4, 2f);
        }
        else if (Score.score >= 5000 && speed5 == false)
        {
            increaseSpeed(5, 1.9f);
        }
        else if (Score.score >= 7000 && speed6 == false)
        {
            increaseSpeed(6, 1.7f);
        }
        else if (Score.score >= 9000 && speed7 == false)
        {
            increaseSpeed(7, 1.6f);
        }
        else if (Score.score >= 11000 && speed8 == false)
        {
            increaseSpeed(8, 1.5f);
        }
        else if (Score.score >= 13000 && speed9 == false)
        {
            increaseSpeed(9, 1.4f);
        }
        else if (Score.score >= 15000 && speed10 == false)
        {
            increaseSpeed(10, 1.2f);
        }
    }


    public void increaseSpeed(int speed, float rate)
    {
        speedText.gameObject.SetActive(true);
        source.PlayOneShot(speedingUp);
        StartCoroutine(disableText());

        switch (speed)
        {
            case 1:
                speed1 = true;
                break;
            case 2:
                speed2 = true;
                break;
            case 3:
                speed3 = true;
                break;
            case 4:
                speed4 = true;
                break;
            case 5:
                speed5 = true;
                break;
            case 6:
                speed6 = true;
                break;
            case 7:
                speed7 = true;
                break;
            case 8:
                speed8 = true;
                break;
            case 9:
                speed9 = true;
                break;
            case 10:
                speed10 = true;
                break;
            default:
                break;
        }
        
        CancelInvoke();
        spawnRate = rate;
        InvokeRepeating("spawnAcorn", 1, spawnRate);
        
    }

    public void spawnAcorn()
    {
        if (SlowTime.active == false)
        {
            //get a random value to spawn the acorn
            float pos = Random.Range(mainCamera.ScreenToWorldPoint(new Vector2(0 + leftOffset, 0)).x,
                mainCamera.ScreenToWorldPoint(new Vector2(Screen.width - rightOffset, 0)).x);

            //spawn and play the leaf animation
            particle.transform.position = new Vector3(pos, 4.5f, 0);
            particles.Play();

            //disable the leave after an amount of time
            source.PlayOneShot(leaves);
            
            
            //1 in 10 chance of the golden acorn spawning
            int spawnGolden = Random.Range(1, 10);
          
            
            //If chance happens, spawn the golden acorn, otherwise spawn regular acorn
            GameObject acornClone;
            Rigidbody2D rb;
            if (spawnGolden == 2 && goldenActive == false)
            {
                goldenActive = true;
                //spawn the golden acorn
                acornClone =
                    Object.Instantiate<GameObject>(goldenAcorn, new Vector3(pos, 5.5f, 0), Quaternion.identity);
                rb = acornClone.GetComponent<Rigidbody2D>();
                
                
            }
            else
            {
                //Create a new acorn to spawn
                acornClone =
                    Object.Instantiate<GameObject>(acorn, new Vector3(pos, 5.5f, 0), Quaternion.identity);
                rb = acornClone.GetComponent<Rigidbody2D>();
                
            }
            acornWidth = 0.68f;
            acornHeight = 0.65f;

#if UNITY_IOS

        if (UnityEngine.iOS.Device.generation.ToString().Contains("iPad"))
        {
            acornWidth = 0.59f;
            acornHeight = 0.56f;
            
        }

#endif
            
            acornClone.transform.localScale = new Vector3(acornWidth, acornHeight, 0.732155502f);
            //add bounce
            rb.AddTorque(-0.3f, ForceMode2D.Force);

            //create random forces for the acorns when spawning in 
            float xForce = Random.Range(-30, 30);
            xForce = xForce / 10;
            float yForce = Random.Range(-30, 30);
            yForce = yForce / 10;

            //add the force
            rb.AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);
        }
    }

    IEnumerator disableText()
    {
        //hide the speed text after 4 seconds
        yield return new WaitForSeconds(4f);
        speedText.gameObject.SetActive(false);
    }

  
}
