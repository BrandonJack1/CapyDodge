using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Acorn : MonoBehaviour
{
    public GameObject acorn;
    [FormerlySerializedAs("particle")] public GameObject leavesParticle;
    public GameObject goldenAcorn;
    public GameObject giantAcorn;
    public GameObject speedText;
    
    public static bool goldenActive = false;
    public static bool giantActive = false;
    private static float spawnRate = 4f;
    private bool iPad = false;
    private float leftOffset;
    private float rightOffset;

    private bool speed1 = false;
    private bool speed2 = false;
    private bool speed3 = false;
    private bool speed4 = false;
    private bool speed5 = false;
    private bool speed6 = false;
    private bool speed7 = false;
    private bool speed8 = false;
    private bool speed9 = false; 
    private bool speed10 = false;
    
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip leaves;
    [SerializeField] private AudioClip speedingUp;

    private ParticleSystem particles;
    [SerializeField] Camera mainCamera;
    public static bool slowActive;

    void Start()
    {
        //starting spawn rate for acorns
        spawnRate = 4f;
        
        //spawn acorns in relation to the spawn rate
        InvokeRepeating("spawnAcorn", 1, spawnRate);
        goldenActive = false;
        giantActive = false;
        
        //used to spawn within the trees
        leftOffset = Camera.main.pixelWidth / 30;
        rightOffset = Camera.main.pixelWidth / 10;
        
        //particle system for the leaves
        particles = leavesParticle.GetComponent<ParticleSystem>();
        spawnRate = 4f;
        
#if UNITY_IOS
        if (UnityEngine.iOS.Device.generation.ToString().Contains("iPad"))
        {

            iPad = true;
        }
#endif
        
    }
    
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
        //run the "Speeding up text"
        speedText.SetActive(true);
        
        //play the sound 
        source.PlayOneShot(speedingUp);
        StartCoroutine(DisableText());
        
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
        //stop the previous invoke for the previous spawn rate
        CancelInvoke();
        spawnRate = rate;
        InvokeRepeating("spawnAcorn", 1, spawnRate);
    }
    public void spawnAcorn()
    {
        GameObject acornClone;
        Rigidbody2D rb;
        
        //only spawn acorns when the slow time isnt active
        if (SlowTime.active == false)
        {
            //get a random value to spawn the acorn
            float pos = Random.Range(mainCamera.ScreenToWorldPoint(new Vector2(0 + leftOffset, 0)).x,
                mainCamera.ScreenToWorldPoint(new Vector2(Screen.width - rightOffset, 0)).x);
            
            //spawn and play the leaf animation
            leavesParticle.transform.position = new Vector3(pos, 4.5f, 0);
            particles.Play();
            
            //disable the leave after an amount of time
            source.PlayOneShot(leaves);
            
            //1 in 30 chance of spawning different acorn (giant or golden)
            int spawnDiffAcorn = Random.Range(1, 30);
            
            //spawn golden acorn if there isnt already one present
            if (spawnDiffAcorn == 5 && goldenActive == false)
            {
                goldenActive = true;
                InstantiateAcorns(pos, "Golden");
            }
            else if (spawnDiffAcorn is 10 or 20 && giantActive == false && iPad == false)
            {
                giantActive = true;
                InstantiateAcorns(pos, "Giant");
            }
            else
            {
                InstantiateAcorns(pos, "Normal");
            }
        }
    }
    
    public void InstantiateAcorns(float pos, string type)
    {
        GameObject acornType;
        
        switch (type)
        {
            case "Golden":
                acornType = goldenAcorn;
                break;
            case "Giant":
                acornType = giantAcorn;
                break;
            default:
                acornType = acorn;
                break;
        }
        var acornClone = Object.Instantiate<GameObject>(acornType, new Vector3(pos, 5.5f, 0), Quaternion.identity);
        var rb = acornClone.GetComponent<Rigidbody2D>();

        //set the proper size for the acorn
        acornClone.transform.localScale = type == "Giant" ? new Vector3(PlayerSize.GIANT_ACORN_WIDTH, PlayerSize.GIANT_ACORN_HEIGHT, 0.73f) : new Vector3(PlayerSize.ACORN_WIDTH, PlayerSize.ACORN_HEIGHT, 0.73f);
        
        //add bounce to the acorn
        rb.AddTorque(-0.3f, ForceMode2D.Force);
        
        //create random forces for the acorns when spawning in 
        float xForce = Random.Range(-30, 30);
        xForce = xForce / 10;
        float yForce = Random.Range(-30, 30);
        yForce = yForce / 10;
        
        //add the force
        rb.AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);
    }
    IEnumerator DisableText()
    {
        //hide the speed text after 4 seconds
        yield return new WaitForSeconds(4f);
        speedText.gameObject.SetActive(false);
    }
}