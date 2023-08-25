using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicPicker : MonoBehaviour
{
    public AudioSource source;

    public AudioClip one;
    public AudioClip two;
    public AudioClip three;
    // Start is called before the first frame update
    void Start()
    {
        int rnd = Random.Range(0, 3);

        if (rnd == 0)
        {
            source.clip = one;
            source.Play();
            source.loop = true;

        }
        else if (rnd == 1)
        {

            source.clip = two;
            source.Play();
            source.loop = true;
        }
        else if (rnd == 2)
        {
            source.clip = three;
            source.Play();
            source.loop = true;
        }

    }
}
