using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using CloudOnce;


public class Acorn : MonoBehaviour
{
    public GameObject acorn;
    [FormerlySerializedAs("particle")] public GameObject leavesParticle;
    public GameObject goldenAcorn;
    public GameObject giantAcorn;
    public GameObject bombAcorn;
    public GameObject miniAcorn;
    public GameObject speedText;
    
    public static bool goldenActive = false;
    public static bool giantActive = false;
    public static bool bombActive = false;
    private static float spawnRate = 4f;
    private bool iPad = false;
    private float leftOffset;
    private float rightOffset;
    
    [SerializeField] private Animator animator;
    public GameObject bombSmoke;
    

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
    [SerializeField] private AudioClip bombExplosion;

    private ParticleSystem particles;
    [SerializeField] Camera mainCamera;
    public static bool slowActive;

    public static int acornCount;

    void Start()
    {

        acornCount = 0;
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
            increaseSpeed(1,4.2f);
        }
        else if (Score.score >= 1500 && speed2 == false)
        {
            increaseSpeed(2,3.8f);
        }
        else if (Score.score >= 2250 && speed3 == false)
        {
            increaseSpeed(3,3.5f);
        }
        else if (Score.score >= 3000 && speed4 == false)
        {
            increaseSpeed(4, 3.2f);
        }
        else if (Score.score >= 5000 && speed5 == false)
        {
            increaseSpeed(5, 2.8f);
        }
        else if (Score.score >= 7000 && speed6 == false)
        {
            increaseSpeed(6, 2.3f);
        }
        else if (Score.score >= 9000 && speed7 == false)
        {
            increaseSpeed(7, 1.9f);
        }
        else if (Score.score >= 11000 && speed8 == false)
        {
            increaseSpeed(8, 1.7f);
        }
        else if (Score.score >= 13000 && speed9 == false)
        {
            increaseSpeed(9, 1.5f);
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
        acornCount++;
        
        if (acornCount >= 20 && !Achievements.Active_Acorns.IsUnlocked && Application.platform == RuntimePlatform.IPhonePlayer)
        {
            Achievements.Active_Acorns.Unlock();
            print("Acheivment");
        }
        
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
            else if (spawnDiffAcorn is 11 or 29)
            {
                bombActive = true;
                InstantiateAcorns(pos, "Bomb");
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
            case "Bomb":
                acornType = bombAcorn;
                break;
            default:
                acornType = acorn;
                break;
        }
        var acornClone = Object.Instantiate<GameObject>(acornType, new Vector3(pos, 5.5f, 0), Quaternion.identity);
        var rb = acornClone.GetComponent<Rigidbody2D>();

        //set the proper size for the acorn
        acornClone.transform.localScale = type == "Giant" ? new Vector3(PlayerSize.GIANT_ACORN_WIDTH, PlayerSize.GIANT_ACORN_HEIGHT, 0.73f) : new Vector3(PlayerSize.ACORN_WIDTH, PlayerSize.ACORN_HEIGHT, 0.73f);

        if (acornType != bombAcorn)
        {
            acornClone.transform.eulerAngles = new Vector3(0, 0, Random.Range(1, 365));
        }

        if (acornType == bombAcorn)
        {
            StartCoroutine(startBombAcorn(acornClone));
        }

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

    IEnumerator startBombAcorn(GameObject acorn)
    {
        bool explode = false;
        
        var bombAcorn = acorn.GetComponent<BombAcorn>();
        //start the countdown on the acorn
        int currCountdownValue = 15;
        while (currCountdownValue > 0)
        {
            bombAcorn.countdown.text = currCountdownValue.ToString();
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }


        yield return new WaitUntil(() => bombAcorn.explodeArea || bombAcorn.IsDestroyed());

        if (bombAcorn.IsDestroyed())
        {
            yield break;
        }
        source.PlayOneShot(bombExplosion);
        animator.SetTrigger("Shake");
        GameObject smokeClone = Instantiate(bombSmoke, bombAcorn.transform.position, Quaternion.identity);
        Destroy(smokeClone, 0.4f);
        List<float> arr = new List<float>();

        for (int i = 0; i < 3; i++)
        {
            arr = randomForce();

            if (i % 2 == 0)
            {
                spawnMiniAcorns(acorn, -(arr[0]), arr[1]);
            }
            else
            {
                spawnMiniAcorns(acorn, arr[0], arr[1]);
            }
        }

        acorn.SetActive(false);
    }

    public List<float> randomForce()
    {
        List<float> arr  = new List<float>();
        float x = Random.Range(1f, 2.5f);
        arr.Add(x);
        float y = Random.Range(2f, 4f);
        arr.Add(y);

        return arr;


    }

    private void spawnMiniAcorns(GameObject bombAcorn,float x, float y)
    {

        Vector3 bombAcornPos = bombAcorn.transform.position;
        GameObject miniAcornClone = Instantiate(miniAcorn, bombAcornPos, Quaternion.identity);
        miniAcornClone.GetComponent<Rigidbody2D>().AddForce(new Vector2(x,y),ForceMode2D.Impulse);
    }
}