using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject notePrefab;
    public GameObject folderObject;
    public GameObject hitPointObject; // Reference to hit point sprite

    private BeatManager beatManager;

    public float beatsToHitPoint = 4; // Notes should reach hit point after 4 beats
    private float noteSpeed;

    void Start()
    {
        beatManager = FindObjectOfType<BeatManager>(); // Get reference to BeatManager
        if (beatManager == null)
        {
            Debug.LogError("BeatManager not found in the scene!");
        }
    }

    public void setNoteSpeed()
    {
        float crotchet = beatManager.getCrotchet(); // Get beat duration from BeatManager
        float timeToHitPoint = beatsToHitPoint * crotchet;

        // Calculate distance from spawn to hit point
        float spawnX = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x + 1;
        float hitPointX = hitPointObject.transform.position.x;
        float distanceToHitPoint = spawnX - hitPointX;

        // Calculate note speed to ensure precise arrival
        noteSpeed = distanceToHitPoint / timeToHitPoint;
    }

    public void SpawnNote()
    {
        Vector2 folderPosition = folderObject.transform.position;
        float spawnX = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x + 1;
        Vector2 spawnPosition = new Vector2(spawnX, folderPosition.y - 1.12f);

        GameObject newNote = Instantiate(notePrefab, spawnPosition, Quaternion.identity);

        Note note = newNote.GetComponent<Note>();
        note.Initialize(hitPointObject.transform.position.x, noteSpeed, beatManager);
    }
}
