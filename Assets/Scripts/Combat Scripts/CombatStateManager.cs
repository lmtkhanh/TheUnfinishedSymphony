using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatStateManager : MonoBehaviour
{
    public int gameState = 0; // 0 = not started, 1 = attack mode, 2 = defend mode
    public double lastCheckedTime = -1.0; // Track last checked DSP time
    public BeatManager beatManager;
    public GameObject attackBar;

    // Just for testing
    public TMP_Text modeText;

    void Start()
    {
    }

    void Update()
    {
        if (beatManager.songStarted)
        {
            double currentTime = AudioSettings.dspTime;

            // Update the UI text for debugging
            if (modeText != null)
            {
                modeText.text = "Mode: " + GetModeText();
            }

            if (currentTime > lastCheckedTime) // Ensure we only check once per frame
            {
                lastCheckedTime = currentTime;
                CheckModeSwitch(currentTime);
            }
        }
        
    }

    void CheckModeSwitch(double currentTime)
    {
        // Check if it's time to switch to attack mode
        if (beatManager.currentSong.attackModeBeats.Count > 0)
        {
            double attackModeTime = beatManager.GetDspTimeForBeat(beatManager.currentSong.attackModeBeats[0]);
            if (currentTime >= attackModeTime)
            {
                gameState = 1; // Attack mode
                attackBar.SetActive(true);
                Debug.Log("Switched to Attack Mode!");
                beatManager.currentSong.attackModeBeats.RemoveAt(0); // Remove processed beat
            }
        }

        // Check if it's time to switch to defend mode
        if (beatManager.currentSong.defendModeBeats.Count > 0)
        {
            double defendModeTime = beatManager.GetDspTimeForBeat(beatManager.currentSong.defendModeBeats[0]);
            if (currentTime >= defendModeTime)
            {
                gameState = 2; // Defend mode
                attackBar.SetActive(false);
                Debug.Log("Switched to Defend Mode!");
                beatManager.currentSong.defendModeBeats.RemoveAt(0); // Remove processed beat
            }
        }
    }

    string GetModeText()
    {
        switch (gameState)
        {
            case 1:
                return "Attack Mode";
            case 2:
                return "Defend Mode";
            default:
                return "Waiting...";
        }
    }
}
