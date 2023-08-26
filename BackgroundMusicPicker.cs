using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicPicker : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip one;
    [SerializeField] private AudioClip two;
    [SerializeField] private AudioClip three;
    
    // Start is called before the first frame update
    void Start()
    {
        //get random int to pick background audio
        int rnd = Random.Range(0, 3);

        if (rnd == 0)
        {
            source.clip = one;
        }
        else if (rnd == 1)
        {
            source.clip = two;
        }
        else if (rnd == 2)
        {
            source.clip = three;
        }
        source.Play();
        source.loop = true;
    }
}
