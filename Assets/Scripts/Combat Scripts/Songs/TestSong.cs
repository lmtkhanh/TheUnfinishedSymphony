using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSong : Song
{
    public TestSong()
    {
        BPM = 90;
        offset = 0.1f;

      
        attackBeatsToHit = new List<float> {5f, 6f, 7f, 8f, 9f, 9.25f, 9.5f, 9.75f, 10f, 11f, 12f, 13f, 14f,//first attack mode phase
                                            35f, 35.5f, 36f, 36.6f, 37f, 37.5f, 40f, 41f, 42f, 43f, 43.25f, 43.5f}; //second attack mode phase

        defendBeatsToHit = new List<List<float>>()
            {
                new List<float>() {24f, 26f, 28f, 30f }  // Defend Phase 1
            }; 


        attackModeBeats = new List<float> {0f, 31f};
        defendModeBeats = new List<float> {15f};
    }

    public override void PlaySong(SongManager songManager)
    {
        songManager.playTestSong();
    }
}