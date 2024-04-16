using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //code source : https://docs.unity3d.com/ScriptReference/CharacterController.Move.html 

    private CharacterController controller;
    private Vector3 playerVelocity;
    
    [SerializeField]
    private float playerSpeed = 2.0f;
    private float speedIncrement = 1.0f;
    private float lastIncrement;
    private int playerTrack = 0;    //there are 4 tracks (0-3) in which the player can choose to run
    private bool groundedPlayer;
    private float gravityValue = -9.81f;

    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        lastIncrement = Time.time;
    }

    void Update()
    {   
        Debug.Log(Time.time);
        //speed is incremented every 0.5 seconds
        //the increment in speed is greater at the beggining of the game, so that it gets difficult fast, but not too much
        if(Time.time - lastIncrement > 0.5f) {
            if(Time.time < 2.0f){
            speedIncrement += 0.05f;
            } else {
                speedIncrement += 0.035f;
            }
        }
        
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        //player will have a constant movement forward and will only be able to move left or right 
        //if the player uses left or right keys, they will move to the corresponding track, if possible
        if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
            if(playerTrack < 3) {
                playerTrack++;
                controller.Move(new Vector3(5.0f, 0, 0));
            }

        } else if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
            if(playerTrack > 0) {
                playerTrack--;
                controller.Move(new Vector3(-5.0f, 0, 0));
            }

        }

        Vector3 direction = new Vector3(0, 0, 1);

        controller.Move(direction * Time.deltaTime * (playerSpeed + speedIncrement));

        if (direction != Vector3.zero)
        {
            gameObject.transform.forward = direction;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
