using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefNPC : NPC
{
    public Dialogue repeatDialogue;
    public UIManager uiManager;

    public override void Interact() //trigger dialogue
    {   
        isInteracting = true;
        if (!hasInteracted)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue, portraitSprite, this);
        }
        else
        {
            FindObjectOfType<DialogueManager>().StartDialogue(repeatDialogue, portraitSprite, this);
        }
        
    }

    public override void CompleteInteraction()
    {
        isInteracting = false;
        hasInteracted = true;
        uiManager.ShowShopUI();
    }
}
