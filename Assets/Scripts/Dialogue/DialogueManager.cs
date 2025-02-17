using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DialogueManager : MonoBehaviour
{
    public PlayerController player; // ref to player
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Image characterPortrait;
    public Sprite playerPortrait; //player portrait
    private Sprite npcPortrait;  //npc portrait
    private NPC currentNPC;

    public Animator animator;

    private Queue<string> sentences;
    private Queue<bool> isPlayerSpeakingQueue;
    private string npcName;
    void Start()
    {
        sentences = new Queue<string>();
        isPlayerSpeakingQueue = new Queue<bool>(); //check what sentences the player is speaking
        
    }

    public void StartDialogue(Dialogue dialogue, Sprite npcPortraitSprite, NPC npc){
        Debug.Log("NPC Portrait Assigned: " + npcPortraitSprite.name);
        currentNPC = npc;
        player.StopPlayerMovement(); //prevent player from moving while dialogue is running

        animator.SetBool("isOpen", true);
        nameText.text = dialogue.npcName;
        npcName = dialogue.npcName;
        npcPortrait = npcPortraitSprite;

        sentences.Clear();
        isPlayerSpeakingQueue.Clear();

       for (int i = 0; i < dialogue.sentences.Length; i++)
        {
            sentences.Enqueue(dialogue.sentences[i]);
            isPlayerSpeakingQueue.Enqueue(dialogue.isPlayerSpeaking[i]);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence(){
        if (sentences.Count == 0){
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        bool isPlayerSpeaking = isPlayerSpeakingQueue.Dequeue();

        // Change character name and portrait dynamically
        if (isPlayerSpeaking)
        {
            nameText.text = "Lucien"; // Player's name
            characterPortrait.sprite = playerPortrait;
        }
        else
        {
            nameText.text = npcName;
            characterPortrait.sprite = npcPortrait;
        }

        StopAllCoroutines();
        StartCoroutine(TypeSentence (sentence));

    }

    IEnumerator TypeSentence (string sentence){
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray()){
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.008f);
        }
    }

    void EndDialogue(){
        animator.SetBool("isOpen", false);
        player.StartPlayerMovement();

        if (currentNPC != null)
        {
            currentNPC.CompleteInteraction();
        }
        
    }



}
