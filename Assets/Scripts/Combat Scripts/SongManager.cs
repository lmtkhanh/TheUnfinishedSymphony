using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip testSong;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //songs
    public void playTestSong()
    {
        audioSource.clip = testSong;
        audioSource.Play(); // Play the song continuously (not PlayOneShot)
    }
}
