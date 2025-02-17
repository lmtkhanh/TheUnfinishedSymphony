using UnityEngine;

public class Note : MonoBehaviour
{
    private float hitPointX;
    private float speed;
    private BeatManager beatManager;

    private SpriteRenderer spriteRenderer; // Reference to the note's sprite renderer

    public float hitTolerance = 0.5f; // Tolerance for when the note is near the hit point (in seconds)
    private double targetHitTime;

    // Initialize method
    public void Initialize(float hitPointX, float speed, BeatManager beatManager)
    {
        this.hitPointX = hitPointX; // The X position of the hit point
        this.speed = speed; // The speed at which the note will move
        this.beatManager = beatManager; // Reference to the BeatManager for DSP timing

        spriteRenderer = GetComponent<SpriteRenderer>(); // Get reference to the sprite renderer

        // Calculate the target hit time based on the beat manager's DSP time
        targetHitTime = beatManager.GetDspTimeForBeat(6); // Use 6 as an example for now (can be dynamic based on beats)
    }

    void Update()
    {
        // Move the note to the left based on speed and time passed
        transform.position += Vector3.left * speed * Time.deltaTime;

        // Check if the note is within the tolerance range of the target DSP time (timing-based hit)
        double currentDspTime = AudioSettings.dspTime;
        Debug.Log("currentDsptime: " + currentDspTime);
        Debug.Log("target Hit Time: " + targetHitTime);

        // If the current DSP time is within tolerance of the target hit time, change color to red
        if (Mathf.Abs((float)(currentDspTime - targetHitTime)) <= hitTolerance)
        {
            spriteRenderer.color = Color.red; // Change color to red when near the hit point
        }

        // Destroy if it reaches the end point (missed note)
        if (transform.position.x <= hitPointX - 2) // Adjust end point as needed
        {
            Debug.Log("Destroying note at dspTime: " + currentDspTime);
            Destroy(gameObject); // Remove the note when it reaches this point
        }
    }
}
