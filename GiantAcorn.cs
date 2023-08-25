using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class GiantAcorn : MonoBehaviour
{
    public Animator animator;
    public ParticleSystem leaves;
    public AudioSource source;
    public AudioClip thud;

    public GameObject cam;
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
        if (col.CompareTag("GiantAcorn"))
        {
            
            source.PlayOneShot(thud);
            leaves.Clear();
            leaves.Play();
            animator.SetTrigger("Shake");
            //Make leaves particle system active

        }
    }

    
}
