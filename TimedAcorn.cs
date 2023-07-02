using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimedAcorn : MonoBehaviour
{
    public GameObject acorn;
    public GameObject particle;

    public static float spawnRate = 4f;
    public float leftOffset;
    public float rightOffset;
    
    public bool speed1 = false;
    public bool speed2 = false;
    public bool speed3 = false;
    public bool speed4 = false;
    
    public AudioSource source;
    public AudioClip leaves;
    public AudioClip speedingUp;
    
    public TextMeshProUGUI speedText;
    private ParticleSystem particles;
    public Camera mainCamera;

    public Animator anim1;
    public Animator anim2;
    
    
    void Start()
    {

        //spawn acorns in relation to the spawn rate
        InvokeRepeating("spawnAcorn", 1, spawnRate);
        
        //used to spawn within the trees
        leftOffset = Camera.main.pixelWidth / 30;
        rightOffset = Camera.main.pixelWidth / 10;

        particles = particle.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //adjust the spawn speed
        if (Timer.minuteCount == 0 && (int)Timer.secondsCount >= 25 && speed1 == false)
        {
            speedText.gameObject.SetActive(true);
            source.PlayOneShot(speedingUp);
            StartCoroutine(disableText());
            speed1 = true;
            CancelInvoke();
            spawnRate = 3.5f;
            InvokeRepeating("spawnAcorn", 1, spawnRate);
            anim1.Play("vineMove");
            anim2.Play("vineMove");
            
            
            
        }
        else if (Timer.minuteCount == 1 && Timer.secondsCount >= 0 && speed2 == false)
        {
            
            speedText.gameObject.SetActive(true);
            source.PlayOneShot(speedingUp);
            StartCoroutine(disableText());
            speed2 = true;
            CancelInvoke();
            spawnRate = 3f;
            InvokeRepeating("spawnAcorn", 1, spawnRate);
        }
        else if (Timer.minuteCount == 1 && Timer.secondsCount >= 30 && speed3 == false)
        {
            
            speedText.gameObject.SetActive(true);
            source.PlayOneShot(speedingUp);
            StartCoroutine(disableText());
            speed3 = true;
            CancelInvoke();
            spawnRate = 2.5f;
            InvokeRepeating("spawnAcorn", 1, spawnRate);
        }
        else if (Timer.minuteCount == 2 && Timer.secondsCount >= 0 && speed4 == false)
        {
            
            speedText.gameObject.SetActive(true);
            source.PlayOneShot(speedingUp);
            StartCoroutine(disableText());
            speed4 = true;
            CancelInvoke();
            spawnRate = 2f;
            InvokeRepeating("spawnAcorn", 1, spawnRate);
        }
    }

    public void spawnAcorn()
    {
        //get a random value to spawn the acorn
        float pos = Random.Range(mainCamera.ScreenToWorldPoint(new Vector2(0 + leftOffset, 0)).x, mainCamera.ScreenToWorldPoint(new Vector2(Screen.width - rightOffset, 0)).x);
        
        //spawn and play the leaf animation
        particle.transform.position = new Vector3(pos, 4.5f, 0);
        particles.Play();
        
        //disable the leave after an amount of time
        source.PlayOneShot(leaves);
        
        //Create a new acorn to spawn
        GameObject acornClone = Object.Instantiate<GameObject>(acorn, new Vector3(pos, 5.5f, 0), Quaternion.identity);
        Rigidbody2D rb = acornClone.GetComponent<Rigidbody2D>();
        
        //if the player is playing on an ipad, scale down the acorns
/*#if UNITY_IOS

        if (UnityEngine.iOS.Device.generation.ToString().Contains("ipad"))
        {
            acornClone.gameObject.transform.localScale = new Vector3(0.512729168f,0.467506707f,1);
        }
#endif  */      
        
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

    IEnumerator disableText()
    {
        //hide the speed text after 4 seconds
        yield return new WaitForSeconds(4f);
        speedText.gameObject.SetActive(false);
    }

  
}
