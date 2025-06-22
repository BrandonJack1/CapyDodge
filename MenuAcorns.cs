using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAcorns : MonoBehaviour
{

    [SerializeField] private GameObject acorn;
    private float leftOffset;
    private float rightOffset;
    [SerializeField] private GameObject leftWall;
    [SerializeField] private GameObject rightWall;
    public Camera camera;
    public GameObject cake;

    private int acornCount;
    // Start is called before the first frame update
    void Start()
    {
        //used to spawn within the trees
        leftOffset = Camera.main.pixelWidth / 30;
        rightOffset = Camera.main.pixelWidth / 10;
        InvokeRepeating("spawnAcorn", 1, 3);
        
        
        Vector3 left = camera.ScreenToWorldPoint(new Vector3(0, 0, 5));
        Vector3 right = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        rightWall.transform.position = right;
        leftWall.transform.position = left;
        
    }

    // Update is called once per frame
    void Update()
    {

        if (acornCount >= 6)
        {
            CancelInvoke(nameof(spawnAcorn));
        }
        
    }

    private void spawnAcorn()
    {
        float pos = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0 + leftOffset, 0)).x,
            Camera.main.ScreenToWorldPoint(new Vector2(Screen.width - rightOffset, 0)).x);
        
        
        var acornClone = Instantiate(acorn, new Vector3(pos, 5.5f, 0), Quaternion.identity);
        var rb = acornClone.GetComponent<Rigidbody2D>();
        acornClone.transform.localScale = new Vector3(1, 1, 1);
        acornClone.transform.eulerAngles = new Vector3(0,0, Random.Range(1, 365));
        acornClone.GetComponent<SpriteRenderer>().sortingOrder = 1;
        
        
        //add bounce to the acorn
        rb.AddTorque(-0.3f, ForceMode2D.Force);
        
        //create random forces for the acorns when spawning in 
        float xForce = Random.Range(-30, 30);
        xForce = xForce / 10;
        float yForce = Random.Range(-30, 30);
        yForce = yForce / 10;
        
        //add the force
        rb.AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);

        acornCount++;

    }
}
