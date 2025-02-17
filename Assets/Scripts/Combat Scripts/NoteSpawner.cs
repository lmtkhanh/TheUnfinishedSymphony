using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    //template note object that will be cloned
    public GameObject notePrefab;

    //the attack bar
    public GameObject folderObject;

    //the attack hit point (where you are supposed to hit the note)
    public GameObject hitPointObject;

    //beatmanager
    private BeatManager beatManager;

    //how many beats it take for note to move from spawn point to the hit point (effectively it controls speed of how fast the note is moving)
    public float beatsToHitPoint = 4;
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
        float timeToHitPoint = beatsToHitPoint * crotchet;

        // Calculate distance from spawn to hit point
        float spawnX = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x + 1;
        float hitPointX = hitPointObject.transform.position.x;
        float distanceToHitPoint = spawnX - hitPointX;

        // Calculate note speed to ensure precise arrival
        noteSpeed = distanceToHitPoint / timeToHitPoint;
    }

    public Note SpawnNote(float beat)
    {
        //set the spawn point, x equal some arbitary spawn point (adjust), y is just the y value of the attack bar object
        Vector2 folderPosition = folderObject.transform.position;
        float spawnX = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x + 1;
        Vector2 spawnPosition = new Vector2(spawnX, folderPosition.y - 1.12f);

        //instantiate the note
        GameObject newNote = Instantiate(notePrefab, spawnPosition, Quaternion.identity);
        Note note = newNote.GetComponent<Note>();
        note.Initialize(beat, hitPointObject.transform.position.x, noteSpeed, beatManager);
        return note;
    }
}
