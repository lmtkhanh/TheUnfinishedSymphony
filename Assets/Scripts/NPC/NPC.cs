using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    public Sprite portraitSprite;
    public Dialogue dialogue;

    public override void Interact() //trigger dialogue
    {   
        isInteracting = true;
        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueManager.StartDialogue(dialogue, portraitSprite, this);

    }

    public void CompleteInteraction()
    {
        isInteracting = false;
        hasInteracted = true;
    }

}
