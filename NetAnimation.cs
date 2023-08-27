using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetAnimation : MonoBehaviour
{
    [SerializeField] private GameObject net;
    
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
