using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip testSong;

    //songs
    public void playTestSong()
    {
        audioSource.clip = testSong;
        audioSource.Play(); // Play the song continuously (not PlayOneShot)
    }
}
