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
                                            52f, 52.5f, 53f, 53.5f, 54f, 54.5f, 57f, 58f, 59f, 60f, 60.25f, 60.5f}; //second attack mode phase

        defendBeatsToHit = new List<List<float>>()
            {
                new List<float>() {24f, 26f, 28f, 30f},  // Defend Phase 1
                 new List<float>() {40f, 41f, 42f, 43f, 44f, 45f, 46f}  // Defend Phase 1
            };
        songcompleteBeat = 62f;


        attackModeBeats = new List<float> {0f, 48f};
        defendModeBeats = new List<float> {15f};
    }

    public override void PlaySong(SongManager songManager)
    {
        songManager.playTestSong();
    }
}