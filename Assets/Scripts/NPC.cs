using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    public Sprite portraitSprite;
    public Dialogue dialogue;

    public override void Interact() //trigger dialogue
    {   
        hasInteracted = true;
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, portraitSprite);
        
    }

}
