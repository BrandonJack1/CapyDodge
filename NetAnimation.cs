using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetAnimation : MonoBehaviour
{


    public GameObject net;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        
        print(col.tag);
        if (col.gameObject.CompareTag("Acorn"))
        {
            Debug.Log("Entered");
            net.transform.GetComponent<Animator>().SetTrigger("NetTrigger");

        }
    }
}
