using UnityEngine;

public class Note : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // Reference to the note's sprite renderer

    private float hitPointX;
    private float speed;
    private BeatManager beatManager;
    private AudioManager audioManager;

    float hitTolerance = 0.3f; // Total time window to register a hit
    float perfectHitThreshold = 0.1f; // Smaller window for a perfect hit
    private double targetHitTime;

    private bool isHit = false; // Track if the note has been hit already

    // Initialize method
    public void Initialize(float beat, float hitPointX, float speed, BeatManager beatManager)
    {
        this.hitPointX = hitPointX; // The X position of the hit point
        this.speed = speed; // The speed at which the note will move
        this.beatManager = beatManager; // Reference to the BeatManager for DSP timing

        spriteRenderer = GetComponent<SpriteRenderer>(); // Get reference to the sprite renderer

        // Get AudioManager reference
        audioManager = FindObjectOfType<AudioManager>();
        if (audioManager == null)
        {
            Debug.LogError("AudioManager not found in the scene!");
        }

        // Calculate the target hit time of this note based on the beat manager's DSP time
        targetHitTime = beatManager.GetDspTimeForBeat(beat);
    }

    void Update()
    {
        // Move the note to the left based on speed and time passed
        transform.position += Vector3.left * speed * Time.deltaTime;

        // Check if the note is within the tolerance range of the target DSP time (timing-based hit)
        double currentDspTime = AudioSettings.dspTime;

        // If the current DSP time is within tolerance of the target hit time, change color to red
        // Check if the note is close to the target hit time
        float timeDifference = Mathf.Abs((float)(currentDspTime - targetHitTime));
        if (timeDifference <= hitTolerance)
        {
            if (timeDifference <= perfectHitThreshold) // Define a smaller threshold for perfect hits
            {
                spriteRenderer.color = Color.green; // Perfect hit zone
            }
            else
            {
                spriteRenderer.color = Color.red; // In range but slightly off
            }

            // Check if the player presses 'A' during this moment and it hasn't already been hit
            if (!isHit && Input.GetKeyDown(KeyCode.A))
            {
                audioManager.playHitSoundA();
                isHit = true; // Mark the note as hit

                Destroy(gameObject); // Destroy the note if 'A' is pressed
            }
        }
        else
        {
            spriteRenderer.color = Color.white; // Out of range
        }

        // Destroy if it reaches the end point (missed note)
        if (transform.position.x <= hitPointX - 5) // Adjust end point as needed
        {
            Destroy(gameObject); // Remove the note when it reaches this point
        }
    }
}