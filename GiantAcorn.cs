using System;
using System.Collections;
using System.Collections.Generic;
using CandyCoded.HapticFeedback;
using UnityEngine;
using Random = UnityEngine.Random;

public class GiantAcorn : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private  ParticleSystem leaves;
    [SerializeField] private  AudioSource source;
    [SerializeField] private  AudioClip thud;

    private void OnTriggerEnter2D(Collider2D col)
    {
        //if the ground detects a giant acorn
        if (col.CompareTag("GiantAcorn"))
        {
            //play thud
            source.PlayOneShot(thud);
            
            //reset leaves particles and play it
            leaves.Clear();
            leaves.Play();
            
            if (Application.platform == RuntimePlatform.IPhonePlayer && PlayerPrefs.GetString("Vibration", "On") != "Off")
            {
                HapticFeedback.LightFeedback();
            }
            //play screen shake animaiton
            animator.SetTrigger("Shake");
        }
    }
}
