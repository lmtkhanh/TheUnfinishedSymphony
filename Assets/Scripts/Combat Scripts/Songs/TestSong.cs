using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSong : Song
{
    public TestSong() // Constructor to initialize BPM for this song
    {
        BPM = 90;  // Set the BPM of the song
    }

    // Override PlaySong method to play TestSong via AudioManager
    public override void PlaySong(SongManager songManager)
    {
        songManager.playTestSong(); // Calls the method to play the test song
    }
}