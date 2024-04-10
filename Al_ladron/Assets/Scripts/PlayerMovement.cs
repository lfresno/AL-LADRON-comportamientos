using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //code source : https://docs.unity3d.com/ScriptReference/CharacterController.Move.html 

    private CharacterController controller;
    private Vector3 playerVelocity;
    private float playerSpeed = 2.0f;
    private int playerTrack = 0;    //there are 4 tracks (0-3) in which the player can choose to run
    private bool groundedPlayer;
    private float gravityValue = -9.81f;

    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
    }

    void Update()
    {   
        
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

        Vector3 move = new Vector3(0, 0, playerSpeed);

        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
