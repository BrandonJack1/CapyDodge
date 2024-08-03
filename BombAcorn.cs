using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BombAcorn : MonoBehaviour
{

    public TextMeshProUGUI countdown;
    public static int bombAcornCount;

    public bool explodeArea = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Explode")
        {
            explodeArea = true;
        }
        else if (col.tag == "Normal")
        {
            explodeArea = false;
        }
    }

 

    // Update is called once per frame
    void Update()
    {
        
    }
}
