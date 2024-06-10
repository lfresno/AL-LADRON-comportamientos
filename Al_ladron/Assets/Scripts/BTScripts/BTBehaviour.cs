using System.Collections;
using System.Collections.Generic;
using Panda;
using UnityEngine;
using UnityEngine.AI;

public class BTBehaviour : MonoBehaviour
{
    //I'm using PandaBT to create the BT logic
    //this script is used to define the actions that each node must do. 

    //Player
    public GameObject player;
    public PlayerMovement playerMovement;
    private Vector3 distanceToPlayer0;
    private Vector3 distanceToPlayer;

    void Start()
    {
        distanceToPlayer0 = transform.position - player.transform.position;
        distanceToPlayer = distanceToPlayer0;
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    //HIDING
    [Task]
    bool isPlayerLooking(){ //check if player is looking
        return playerMovement.lookingBack;
    }

    [Task]
    bool isObstacleCloserThan( float num){  //check if there is any obstacle close
        return true;
    }

    [Task]
    void HideObstacle(){    //hide behind an obstacle

    }

    [Task]
    void HideInvisible(){   //hide by making invisible

    }

    //STEALING
    [Task]
    bool isPlayerCloserThan(float num){ //check if player is close enough to steal
        return (Mathf.Abs(distanceToPlayer.z) <= num);
    }

    [Task]
    void Steal(){       

    }

 
    //CHASING
    [Task]
    void Chase(){

    }
    
}
