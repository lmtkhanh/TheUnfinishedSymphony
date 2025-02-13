using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private const float playerSpeed = 13f;
    private Vector3 moveDir;
    private Rigidbody2D playerRigidbody2D;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

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
}
