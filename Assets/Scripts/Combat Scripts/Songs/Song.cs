using System.Collections.Generic;
using UnityEngine;

//the template class for all the songs

public class Song : MonoBehaviour
{
    public List<float> beatsToHit; // List of beats where the notes should be hit
    public BeatManager beatManager;

    private List<double> spawnTimes; // List of dspTimes when notes should spawn


    //the BPM of the song
    public float BPM;

    //Just in case the music file has a slight delay at the beginning before the music starts, use this to delay the beat counting.
    public float offset;

    public virtual void PlaySong(SongManager songManager)
    {
    }
}