using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSong : Song
{
    public TestSong()
    {
        BPM = 90;
        offset = 0.1f;

        // Fast 5-hit combo between beats 5 and 6
        beatsToHit = new List<float> {6};
    }

    public override void PlaySong(SongManager songManager)
    {
        songManager.playTestSong();
        //BeatManager.Instance.StartSong(this); // BeatManager now handles note spawning
    }
}