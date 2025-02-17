using System.Collections.Generic;
using UnityEngine;

public class BeatManager : MonoBehaviour
{
    public Song currentSong;
    public SongManager songManager;
    public AudioManager audioManager;
    private AudioSource audioSource;

    private float songBPM;
    private float crotchet; // Duration of one beat
    public double dspTimeSongStart;
    public double dspTimeOffset; // original difference between song start dsp time and actual dsp time at the time.
    private double nextBeatTime;

    public int currentBeat;

    private bool songStarted = false;
    public NoteSpawner noteSpawner;

    void Update()
    {
        if (!songStarted && Input.GetKeyDown(KeyCode.Space))
        {
            startSong();
        }

        if (songStarted)
        {
            // Check and spawn notes based on beats before they arrive
            CheckAndSpawnNotes();

            // Schedule the next beat only when the current DSP time is beyond the next beat
            if (AudioSettings.dspTime >= nextBeatTime)
            {
                ScheduleNextBeat();
            }
        }
    }

    void startSong()
    {
        songStarted = true;
        currentSong = new TestSong();
        songBPM = currentSong.BPM;
        currentBeat = 0;
        audioSource = songManager.audioSource;

        crotchet = 60.0f / songBPM; // Duration of a single beat
        noteSpawner.setNoteSpeed(); // Update note speed based on BPM

        currentSong.PlaySong(songManager);
        dspTimeSongStart = AudioSettings.dspTime; // Reference time
        nextBeatTime = dspTimeSongStart + currentSong.offset;
    }

    void ScheduleNextBeat()
    {
        audioManager.playBeatSound(nextBeatTime);
        if (currentBeat == 8) currentBeat = 0;
        currentBeat++;

        nextBeatTime += crotchet;
    }

    void CheckAndSpawnNotes()
    {
        // Check if we have beats left to spawn
        if (currentSong.beatsToHit.Count > 0)
        {
            // Get the first beat from the list
            float nextBeat = currentSong.beatsToHit[0];

            // Calculate the DSP time for 4 beats before the current beat
            double dspTimeForNoteSpawn = GetDspTimeForBeat(nextBeat - 4);

            // Check if the current DSP time is within the window for spawning the note
            if (AudioSettings.dspTime >= dspTimeForNoteSpawn && AudioSettings.dspTime < dspTimeForNoteSpawn + crotchet)
            {
                // Spawn note for this hit point
                noteSpawner.SpawnNote();
                Debug.Log("spawned!");

                // Remove the beat from the list after it has been processed
                currentSong.beatsToHit.RemoveAt(0);
            }
        }
    }

    public double GetDspTimeForBeat(float beatTime)
    {
        // Convert the beat time to DSP time relative to the song's start
        return dspTimeSongStart + (beatTime * crotchet) + currentSong.offset;
    }

    public float getCrotchet()
    {
        return crotchet;
    }

    public int getCurrentBeat()
    {
        return currentBeat;
    }

    
}