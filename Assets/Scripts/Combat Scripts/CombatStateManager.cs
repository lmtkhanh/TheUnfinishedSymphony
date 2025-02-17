using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStateManager : MonoBehaviour
{
    public int gameState = 0; // 0 = not started, 1 = player attack, 2 = player defense
    public int currentBeat;
    public int lastLoggedBeat = -1; // Track last logged beat
    public BeatManager beatManager;

    // Start is called before the first frame update
    void Start()
    {
        currentBeat = beatManager.getCurrentBeat();
        lastLoggedBeat = currentBeat; // Initialize with starting beat
    }

    // Update is called once per frame
    void Update()
    {
        currentBeat = beatManager.getCurrentBeat();

        if (currentBeat != lastLoggedBeat) // Only log when the beat changes
        {
            Debug.Log("Current beat: " + currentBeat);
            lastLoggedBeat = currentBeat; // Update last logged beat
        }
    }
}