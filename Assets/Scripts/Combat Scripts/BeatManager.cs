using UnityEngine;
using UnityEngine.Events;

public class BeatManager : MonoBehaviour
{
    public Song currentSong;           // Reference to the current song (TestSong)
    public SongManager songManager;    // Reference to the SongManager for playing the song
    public AudioManager audioManager;  // Reference to AudioManager for playing the beat sound
    private AudioSource audioSource;   // AudioSource used to play the song
    private float bpm;                 // BPM of the song
    private float crotchet;            // Duration of a beat (crotchet) in seconds
    private float songPosition;        // Current song position (in seconds)
    private float lastBeat;            // Last beat position in the song
    private double dspTimeSong;        // DSP time when the song started (for synchronization)
    private float offset;              // Offset for the song to handle MP3 gaps
    private float gracePeriod = 0.2f;  // Grace period for hitting the beat (±100ms)

    void Start()
    {
        // Set up the current song and SongManager
        currentSong = new TestSong();  // Initialize with a test song (replace with your actual song)
        audioSource = songManager.audioSource;  // Get AudioSource from SongManager
        bpm = currentSong.BPM;  // Get BPM from the current song

        // Calculate the duration of a beat (crotchet)
        crotchet = 60f / bpm;

        // Record the DSP time at the moment the song starts
        dspTimeSong = AudioSettings.dspTime;

        // Start the song via SongManager
        currentSong.PlaySong(songManager);

        // Initialize offset (for handling any small start gaps in MP3)
        offset = 0f;
        lastBeat = 0f;
    }

    void Update()
    {
        // Calculate current song position in seconds using the DSP time
        songPosition = (float)((AudioSettings.dspTime - dspTimeSong) * audioSource.pitch) - offset;

        // Check if the song has progressed to the next beat
        if (songPosition > lastBeat + crotchet)
        {
            // Play the beat sound or trigger other actions
            audioManager.playBeatSound();

            // Update the last beat time to prevent redundancy
            lastBeat += crotchet;
            // Check for key press
            
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            CheckTiming();
        }


    }

    void CheckTiming()
    {
        // Calculate the difference between current song position and last beat
        float timeSinceLastBeat = Mathf.Abs(songPosition - lastBeat);

        // Check if key press is within the grace period
        if (timeSinceLastBeat <= gracePeriod)
        {
            Debug.Log("Hit");
        }
        else
        {
            Debug.Log("Miss");
        }
    }
}