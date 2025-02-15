using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//For NPCs, puzzles interactables. Extended to NPC and Puzzle Scripts
//Player can interact and script triggers interact function

public class Interactable : MonoBehaviour
{
    public bool hasInteracted = false;
    public virtual void Interact() //override
    {
        Debug.Log("Interacted with " + gameObject.name);
    }
}
