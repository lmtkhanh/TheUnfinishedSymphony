using UnityEditor;
using UnityEngine;

public class Note : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // Reference to the note's sprite renderer

    private int mode;

    private Vector2 targetPosition;
    private float speed;

    private BeatManager beatManager;
    private AudioManager audioManager;

    float hitTolerance = 0.2f; // Total time window to register a hit
    float perfectHitThreshold = 0.1f; // Smaller window for a perfect hit
    private double targetHitTime;

    private bool isHit = false; // Track if the note has been hit already

    // Initialize method
    public void Initialize(int mode, float beat, Vector2 targetPosition, float speed, BeatManager beatManager)
    {
        this.mode = mode;
        this.targetPosition = targetPosition;
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

    public int checkIfHit()
    {
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
                return 2;
            }
            else
            {
                spriteRenderer.color = Color.red; // In range but slightly off
                return 1;
            }
        }
        else
        {
            spriteRenderer.color = Color.white; // Out of range
        }
        return 0;
    }

    void Update()
    {
        if (mode == 1)
        {
            attackNoteUpdate();
        }
        else if (mode == 2)
        {
            defendNoteUpdate();
        }
    }

    void attackNoteUpdate()
    {
        // Move the note towards the target position (based on x and y)
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPosition.x, transform.position.y, transform.position.z), speed * Time.deltaTime);

        // Destroy if it reaches the end point (missed note)
        if (transform.position.x <= targetPosition.x - 5) // Adjust end point as needed
        {
            Destroy(gameObject); // Remove the note when it reaches this point
        }
    }

    void defendNoteUpdate()
    {
        // Move the note instantly towards the target position
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPosition.x, targetPosition.y, transform.position.z), 50 * Time.deltaTime);

        // Optionally, you can destroy the note right after it reaches the target (if you want to remove it immediately after reaching)
        //Destroy(gameObject);
    }
}

/** FOR TESTING
       double currentDspTime = AudioSettings.dspTime;
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
       }
       else
       {
           spriteRenderer.color = Color.white; // Out of range
       }
       */