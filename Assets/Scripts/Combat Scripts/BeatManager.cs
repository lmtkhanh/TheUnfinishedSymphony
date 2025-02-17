using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BeatManager : MonoBehaviour
{
    //---------------------------initiate all the necessary objects and components------------------------------------
    //current song that will be played, when loading combat this should be loaded based on which level
    public Song currentSong;

    //managers
    public SongManager songManager;
    public AudioManager audioManager;
    public NoteSpawner noteSpawner;

    //just for testing
    public TMP_Text dspTimeText;

    //variables carrying current attribute of songs
    private bool songStarted = false;
    private float songBPM; //bpm, carry over from currentSong
    private float crotchet; // Duration of one beat
    public double dspTimeSongStart; //the reference time of when the song actually starts
    public double dspTimeOffset; // original difference between song start dsp time and actual dsp time at the time.

    //for identifying individual beats
    private double nextBeatTime; //the next beat time, when dsp time reaches this time, play a beat.

    public int currentBeat;  //calculate current beat

    void Update()
    {
        //press space bar to start the song
        if (!songStarted && Input.GetKeyDown(KeyCode.Space))
        {
            startSong();
        }

        //testing
        if (dspTimeText != null)
        {
            dspTimeText.text = "DSP Time: " + AudioSettings.dspTime.ToString("F3"); // Show DSP time with 3 decimal places
        }

        //code for gameplay once song started
        if (songStarted)
        {
            CheckAndSpawnNotes(); // Check and spawn notes based on beats before they arrive

            if (AudioSettings.dspTime >= nextBeatTime) // Schedule the next beat only when the current DSP time is beyond the next beat
            {
                ScheduleNextBeat();
            }
        }
    }

    //game initialization. Set up the song before playing
    void startSong()
    {
        songStarted = true;
        currentSong = new TestSong(); //insert whatever the current level song is
        songBPM = currentSong.BPM; //carry over BPM
        currentBeat = 0;

        crotchet = 60.0f / songBPM; // calculate uration of a single beat
        noteSpawner.setNoteSpeed(); // update note speed based on BPM

        currentSong.PlaySong(songManager); //play the song
        dspTimeSongStart = AudioSettings.dspTime; // save reference time for future calculations

        nextBeatTime = dspTimeSongStart + currentSong.offset; //next beat sound time
    }

    //---------------------------playing beat sounds every whole beat-----------------------------
    void ScheduleNextBeat()
    {
        audioManager.playBeatSound(nextBeatTime);
        if (currentBeat == 8) currentBeat = 0;
        currentBeat++;

        nextBeatTime += crotchet;
    }

    //------------------------------checking if notes need to be spawned, and spawn them-----------------------------
    void CheckAndSpawnNotes()
    {
        if (currentSong.beatsToHit.Count > 0)
        {
            float nextBeat = currentSong.beatsToHit[0]; // Get the first beat from the list (of current song)

            double dspTimeForNoteSpawn = GetDspTimeForBeat(nextBeat - 4);  // calculate spawn time, 4 beats before the current beat

            if (AudioSettings.dspTime >= dspTimeForNoteSpawn && AudioSettings.dspTime < dspTimeForNoteSpawn + crotchet)
            {  
                noteSpawner.SpawnNote(nextBeat);
     
                currentSong.beatsToHit.RemoveAt(0); // remove the beat from the list after it has been processed
            }
        }
    }

    //------------------------------for conversion from beattime to dsptime-----------------------------
    public double GetDspTimeForBeat(float beatTime)
    {
        // Convert the beat time to DSP time relative to the song's start
        return dspTimeSongStart + (beatTime * crotchet) + currentSong.offset;
    }

    //-------------------------retrieve methods---------------------------------
    public float getCrotchet()
    {
        return crotchet;
    }

    public int getCurrentBeat()
    {
        return currentBeat;
    }

    
}