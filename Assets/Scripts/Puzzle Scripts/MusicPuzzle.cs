using UnityEngine;
using UnityEngine.UI;
using TMPro;  // Include the TextMeshPro namespace

public class PuzzleMechanism : MonoBehaviour
{
    public AudioSource audioSource; // Main AudioSource for playing the music and notes
    public AudioClip musicSegment; // The main music segment
    public AudioClip[] noteClips; // Audio clips for each note
    public Button playButton; // Button to start the music segment
    public Button[] noteButtons; // Buttons for playing individual notes
    public TMP_Text timerText; // TextMeshProUGUI for displaying the timer

    private float countdown = 30.0f; // Timer countdown from 30 seconds

    void Start()
    {
        // Attach play music functionality to play button
        playButton.onClick.AddListener(PlayMusicSegment);

        // Attach note playing functionality to each note button
        for (int i = 0; i < noteButtons.Length; i++)
        {
            int index = i; // Local copy for the closure to capture
            noteButtons[index].onClick.AddListener(() => PlayNoteSound(index));
        }

        // Initialize timer text
        UpdateTimerText(countdown);
    }

    void Update()
    {
        // Timer countdown logic
        if (countdown > 0)
        {
            countdown -= Time.deltaTime;
            UpdateTimerText(countdown);
        }
        else
        {
            // Handle what happens when the timer runs out
            countdown = 0;
            UpdateTimerText(countdown);
            TimerEnded();
        }
    }

    void PlayMusicSegment()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = musicSegment;
            audioSource.Play();
        }
    }

    void PlayNoteSound(int noteIndex)
    {
        if (noteIndex < noteClips.Length)
        {
            audioSource.PlayOneShot(noteClips[noteIndex]); // Play the note sound
        }
    }

    private void UpdateTimerText(float time)
    {
        // Update the timer text with formatted time
        timerText.text = "Timer: " + time.ToString("F2") + "s";
    }

    private void TimerEnded()
    {
        // Optional: Do something when the timer ends
        Debug.Log("Timer has ended!");
        // E.g., disable note buttons or display a message
    }
}
