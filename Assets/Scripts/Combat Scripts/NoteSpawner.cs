using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    //template note object that will be cloned
    public GameObject notePrefab;

    //the attack bar
    public GameObject folderObject;

    //player position
    public GameObject player;

    //enemy position
    public GameObject enemy;

    //the attack hit point (where you are supposed to hit the note)
    public GameObject hitPointObject;

    //beatmanager
    private BeatManager beatManager;

    //how many beats it take for note to move from spawn point to the hit point (effectively it controls speed of how fast the note is moving)
    public float attackBeatsToHitPoint = 4;
    private float noteSpeed;

    void Start()
    {
        //find beatmanager
        beatManager = FindObjectOfType<BeatManager>(); // Get reference to BeatManager
        if (beatManager == null)
        {
            Debug.LogError("BeatManager not found in the scene!");
        }
    }

    //before spawning notes, beat manager will read the song's BPM and use this function to calculate the note speed
    public void setNoteSpeed()
    {
        float crotchet = beatManager.getCrotchet(); 
        float timeToHitPoint = attackBeatsToHitPoint * crotchet;

        // Calculate distance from spawn to hit point
        float spawnX = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x + 1;
        float hitPointX = hitPointObject.transform.position.x;
        float distanceToHitPoint = spawnX - hitPointX;

        // Calculate note speed to ensure precise arrival
        noteSpeed = distanceToHitPoint / timeToHitPoint;
    }

    public Note SpawnNote(float beat)
    {
            //Debug.Log("spawning attack mode notes");
            //set the spawn point, x equal some arbitary spawn point (adjust), y is just the y value of the attack bar object
            Vector2 folderPosition = folderObject.transform.position;
            float spawnX = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x + 1;
            Vector2 spawnPosition = new Vector2(spawnX, folderPosition.y - 1.12f);

            //instantiate the note
            GameObject newNote = Instantiate(notePrefab, spawnPosition, Quaternion.identity);
            Note note = newNote.GetComponent<Note>();
            note.Initialize(1, beat, hitPointObject.transform.position, noteSpeed, beatManager);
            return note; 
    }

    public Note SpawnDefendNote(float beat)
    {
        // Debug.Log("spawning defend mode notes");

        // Reference the character's position directly (assuming you have a character object, e.g., "playerObject")
        Vector2 characterPosition = enemy.transform.position; // Reference to the character's position

        // Define the radius for the curve around the character
        float radius = 2f;
        float curveHeightOffset = 1f; // Height offset to ensure the curve is centered around the character

        // Calculate the angle for the curve (simple sine wave pattern)
        float angle = Mathf.PI * (beat % 8); // Use modulo to ensure the beat stays within 8 for idle duration

        // Calculate the X and Y positions based on sine and cosine for a curved pattern
        float xPosition = characterPosition.x + Mathf.Cos(angle) * radius;
        float yPosition = characterPosition.y + curveHeightOffset + Mathf.Sin(angle) * radius;

        Vector2 spawnPosition = new Vector2(xPosition, yPosition);

        // Instantiate the note
        GameObject newNote = Instantiate(notePrefab, spawnPosition, Quaternion.identity);
        Note note = newNote.GetComponent<Note>();
        note.Initialize(2, beat, player.transform.position, 0, beatManager);

        return note;
    }
}
