using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSong : Song
{
    public TestSong()
    {
        BPM = 90;
        offset = 0.1f;

      
        beatsToHit = new List<float> {5f, 6f, 7f, 8f, 9f, 9.25f, 9.5f, 9.75f, 10f};
    }

    public override void PlaySong(SongManager songManager)
    {
        songManager.playTestSong();
    }
}