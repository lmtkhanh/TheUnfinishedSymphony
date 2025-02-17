using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip beat;
    public AudioClip hitSoundA;

    public void playBeatSound(double playTime)
    {
        audioSource.clip = beat;
        audioSource.PlayScheduled(playTime);
    }

    public void playHitSoundA()
    {
        audioSource.clip = hitSoundA;
        audioSource.PlayOneShot(hitSoundA);
    }

}
