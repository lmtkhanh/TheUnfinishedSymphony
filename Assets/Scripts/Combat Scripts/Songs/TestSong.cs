using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSong : Song
{
    public TestSong()
    {
        BPM = 90;
        offset = 0.1f;

      
        attackBeatsToHit = new List<float> {5f, 6f, 7f, 8f, 9f, 9.25f, 9.5f, 9.75f, 10f, 11f}; //first attack mode phase

        defendBeatsToHit = new List<List<float>>()
            {
                new List<float>() { 25f, 27f, 29f, 31f }  // Defend Phase 1
            }; 


        attackModeBeats = new List<float> {0f};
        defendModeBeats = new List<float> {12f};
    }

    public override void PlaySong(SongManager songManager)
    {
        songManager.playTestSong();
    }
}