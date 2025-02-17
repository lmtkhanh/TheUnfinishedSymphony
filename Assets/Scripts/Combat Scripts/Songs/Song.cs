using System.Collections.Generic;
using UnityEngine;

//the template class for all the songs

public class Song : MonoBehaviour
{
    public List<float> beatsToHit; // List of beats where the notes should be hit, set this manually

    //the BPM of the song
    public float BPM;

    //Just in case the music file has a slight delay at the beginning before the music starts, use this to delay the beat counting. Also set manually
    public float offset;

    //function that plays the song, use song manager
    public virtual void PlaySong(SongManager songManager)
    {
    }
}