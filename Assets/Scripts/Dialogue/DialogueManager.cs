using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public PlayerManager player; // ref to player
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public Animator animator;

    private Queue<string> sentences;
    private Queue<bool> isPlayerSpeakingQueue;
    private string npcName;
    void Start()
    {
        sentences = new Queue<string>();
        isPlayerSpeakingQueue = new Queue<bool>(); //check what sentences the player is speaking
        
    }

    public void StartDialogue(Dialogue dialogue){
        player.StopPlayerMovement(); //prevent player from moving while dialogue is running

        animator.SetBool("isOpen", true);
        nameText.text = dialogue.npcName;
        npcName = dialogue.npcName;

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

        nameText.text = isPlayerSpeaking ? "Lucien" : npcName; //change character name

        StopAllCoroutines();
        StartCoroutine(TypeSentence (sentence));

    }

    IEnumerator TypeSentence (string sentence){
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray()){
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue(){
        animator.SetBool("isOpen", false);
        player.StartPlayerMovement();
        
    }



}
