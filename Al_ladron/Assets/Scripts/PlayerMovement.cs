using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //code source : https://docs.unity3d.com/ScriptReference/CharacterController.Move.html 

    private CharacterController controller;
    
    [SerializeField]
    private float playerSpeed = 2.0f;
    private float speedIncrement = 1.0f;
    private float lastIncrement;
    private int playerTrack = 0;    //there are 4 tracks (0-3) in which the player can choose to run

    private bool lookingBack = false;

    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        lastIncrement = Time.time;
    }

    void Update()
    {   
        //if player reaches the end of the created path, they will be redirected to the beggining 
        //this is only used in the prototype to test NPCs, but not in the final game
        if(gameObject.transform.position.z >= 1400) {
            controller.Move(new Vector3(0, 0, -1000.0f));
        }

        
        if(Input.GetKeyDown(KeyCode.Space) || lookingBack) {
            //if space is pressed, the player will look back to ckeck on the thieves. 
            //in this moment, the player will stop moving and the camera will move so that the player can check on the animals (they will try to hide)

            //this will throw a coroutine
            speedIncrement -= 0.03f;
            StartCoroutine(LookBack());

        } else {
            //speed is incremented every 0.5 seconds
            //the increment in speed is greater at the beggining of the game, so that it gets difficult fast, but not too much
            if(Time.time - lastIncrement > 0.02f) {
                if(Time.time < 2.0f){
                    speedIncrement += 0.1f;
                } else {
                    speedIncrement += 0.07f;
                }

                lastIncrement = Time.time;
            }

        }

        //player will have a constant movement forward and will only be able to move left or right 
        //if the player uses left or right keys, they will move to the corresponding track, if possible
        if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
            if(playerTrack < 3) {
                controller.Move(new Vector3(5.0f, 0, 0));
                // gameObject.transform.position = new Vector3(playerTrack * 5, gameObject.transform.position.y, gameObject.transform.position.z);
                playerTrack++;
            }

        } else if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
            if(playerTrack > 0) {
                controller.Move(new Vector3(-5.0f, 0, 0));
                // gameObject.transform.position = new Vector3(playerTrack * 5, gameObject.transform.position.y, gameObject.transform.position.z);
                playerTrack--;
            }

        }

        Vector3 move = new Vector3(0, 0, playerSpeed + speedIncrement);

        controller.Move(move * Time.deltaTime);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        //if player couldn't move properly between tracks and ends up between two of them, move them to the one they were before
        if(gameObject.transform.position.x % 5 != 0) {
            //TODO!
        }

    }

    private IEnumerator LookBack() {       
        lookingBack = true;
        float t = Time.realtimeSinceStartup;
        Time.timeScale = 0f;
        while(Time.realtimeSinceStartup - t < 3.0f){
            yield return 0;
        }
        lookingBack = false;
        Time.timeScale = 1.0f;
    }
}
