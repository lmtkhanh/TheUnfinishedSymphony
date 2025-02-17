using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuzzleMechanism : MonoBehaviour
{
    public AudioSource audioSource; // Main AudioSource for playing the music and notes
    public AudioClip musicSegment; // The main music segment
    public AudioClip[] noteClips; // Audio clips for each note
    public Button playButton; // Button to start the music segment
    public Button[] noteButtons; // Buttons for playing individual notes
    public TMP_Text timerText; // TextMeshProUGUI for displaying the timer
    public TMP_Text feedbackText; // TextMeshProUGUI for displaying feedback

    private float countdown = 30.0f; // Timer countdown from 30 seconds
    private int[] correctSequence = {1, 3, 5, 6}; // Indices for the 2th, 4th, 6th, and 7th notes
    private int[] playerSequence = new int[4]; // Array to store player's sequence of note indices
    private int attemptCount = 0; // Number of attempts made

    void Start()
    {
        playButton.onClick.AddListener(PlayMusicSegment);
        SetupNoteButtons();
        UpdateTimerText(countdown);
        feedbackText.text = ""; // Initialize feedback text
    }

    void Update()
    {
        if (countdown > 0)
        {
            countdown -= Time.deltaTime;
            UpdateTimerText(countdown);
        }
        else if (countdown <= 0)
        {
            countdown = 0;
            UpdateTimerText(countdown);
            TimerEnded();
        }
    }

    private void SetupNoteButtons()
    {
        for (int i = 0; i < noteButtons.Length; i++)
        {
            int index = i;
            noteButtons[index].onClick.AddListener(() => HandleNotePress(index));
        }
    }

    private void HandleNotePress(int index)
    {
        if (attemptCount < 4) // Only allow interaction if less than 4 attempts
        {
            audioSource.PlayOneShot(noteClips[index]);
            playerSequence[attemptCount++] = index;

            if (attemptCount == 4)
            {
                CheckSequence();
            }
        }
    }

    private void CheckSequence()
    {
        for (int i = 0; i < correctSequence.Length; i++)
        {
            if (playerSequence[i] != correctSequence[i])
            {
                feedbackText.text = "Incorrect sequence, try again!";
                ResetSequence();
                return;
            }
        }

        feedbackText.text = "Congratulations! You've completed the puzzle.";
        DisableAllButtons();
    }

    private void ResetSequence()
    {
        attemptCount = 0;
    }

    private void DisableAllButtons()
    {
        foreach (Button btn in noteButtons)
        {
            btn.interactable = false;
        }
    }

    private void UpdateTimerText(float time)
    {
        timerText.text = "Timer: " + time.ToString("F2") + "s";
    }

    private void TimerEnded()
    {
        feedbackText.text = "Time's up! Please try again.";
        DisableAllButtons();
    }

    void PlayMusicSegment()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = musicSegment;
            audioSource.Play();
        }
    }
}
