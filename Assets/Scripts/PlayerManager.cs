using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    private const float playerSpeed = 13f;
    private Vector3 moveDir;
    private Rigidbody2D playerRigidbody2D;

    private GameManager gameManager;

    public float interactRange = 1f;
    public KeyCode interactKey = KeyCode.F; //F to interact
    public TextMeshProUGUI interactionText; //Press F text
    private Interactable currentInteractable; //Track nearest interactable

    void Start()
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();

         if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        HandleMovement();
        DetectInteractable();

        if (Input.GetKeyDown(interactKey) && currentInteractable != null)
        {
            currentInteractable.Interact();
        }
        
    }

    //----------------Movement Code-----------------------------------------
    private void HandleMovement(){
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)){
            moveY = +1f;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)){
            moveY = -1f;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
            moveX = -1f;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
            moveX = +1f;
        }

        moveDir = new Vector3(moveX, moveY).normalized; //update move direction
    }

    private void FixedUpdate(){ //updates player position with moveDir
        if(gameManager.currentState == GameManager.GameState.Game)
        playerRigidbody2D.MovePosition(transform.position + moveDir * playerSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider){
        Debug.Log("Trigger!");
    }

    //----------------Interact Code-----------------------------------------

    void DetectInteractable()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactRange);
        Interactable closestInteractable = null;
        float closestDistance = float.MaxValue;

        foreach (Collider2D collider in colliders)
        {
            Interactable interactable = collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestInteractable = interactable;
                }
            }
        }

        currentInteractable = closestInteractable;

        // interaction text
        if (interactionText != null)
        {
            if (currentInteractable != null)
            {
                //interactionText.text = "Press F to interact";
                interactionText.gameObject.SetActive(true);
            }
            else
            {
                interactionText.gameObject.SetActive(false);
            }
        }
    }
}
