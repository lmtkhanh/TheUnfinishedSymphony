using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefNPC : NPC
{
    public Dialogue repeatDialogue;

    public override void Interact() //trigger dialogue
    {   
         if (!hasInteracted)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue, portraitSprite, this);
        }
        else
        {
            FindObjectOfType<DialogueManager>().StartDialogue(repeatDialogue, portraitSprite, this);
        }
        
    }
}
