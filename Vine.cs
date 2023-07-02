using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vine : MonoBehaviour
{
    // Start is called before the first frame update

    public static bool spawnVines = false;
    public float leftOffset;
    public float rightOffset;
    public Camera mainCamera;
    public GameObject vine;
    
    void Start()
    {
        //used to spawn within the trees
        leftOffset = Camera.main.pixelWidth / 30;
        rightOffset = Camera.main.pixelWidth / 10;

      
    }

    // Update is called once per frame
    void Update()
    {
        
        if (spawnVines == true)
        {
            spawnVines = false;
            
            playVine();
        }
        
    }

    public void playVine()
    {
        //get a random value to spawn the acorn
        float pos = Random.Range(mainCamera.ScreenToWorldPoint(new Vector2(0 + leftOffset, 0)).x, mainCamera.ScreenToWorldPoint(new Vector2(Screen.width - rightOffset, 0)).x);
        
        //Create a new acorn to spawn
        GameObject acornClone = Object.Instantiate<GameObject>(vine, new Vector3(pos, 5.5f, 0), Quaternion.identity);
        Rigidbody2D rb = acornClone.GetComponent<Rigidbody2D>();
       
    }
}
