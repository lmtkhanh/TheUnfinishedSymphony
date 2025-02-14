using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip beat;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playBeatSound()
    {
       audioSource.clip = beat;
       audioSource.PlayOneShot(beat);
    }

}
