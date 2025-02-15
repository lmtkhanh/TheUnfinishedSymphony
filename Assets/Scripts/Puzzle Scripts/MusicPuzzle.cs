using UnityEngine;
using UnityEngine.UI;

public class PuzzleMechanism : MonoBehaviour
{
    public AudioSource audioSource; // Main AudioSource for playing the music and notes
    public AudioClip musicSegment; // The main music segment
    public AudioClip[] noteClips; // Audio clips for each note
    public Button playButton; // Button to start the music segment
    public Button[] noteButtons; // Buttons for playing individual notes

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
}
