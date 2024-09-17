using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Panda;
using Unity.VisualScripting;
public class BTenemy : NPCBase
{
    //Using PandaBT to create the BT logic
    //this script is used to define the actions that each node must do. 
    PandaBehaviour pandaBT;

    
    public override void Start()
    {
        pandaBT = GetComponent<PandaBehaviour>();

        distanceToPlayer0 = transform.position - player.transform.position;
        distanceToPlayer = distanceToPlayer0;
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    public override void Update()
    {
        pandaBT.Tick();
        pandaBT.Reset();
    }


    //HIDE
    [Task]
    bool checkPlayerLooking() {
        return playerMovement.lookingBack;
    }

    [Task]
    void HideBT(){
        if(GameManager.Instance.State != GameState.GameState)  return;

        npcCurrent = NPCState.Hide;

        distanceToPlayer = distanceToPlayer + (new Vector3(0.05f, 0.3f, 0.2f));
        transform.position = player.transform.position + distanceToPlayer;
    }

    //STEAL
    [Task]
    bool checkPlayerClose() {
        return Mathf.Abs(distanceToPlayer.z) <= 4.5f;
    }


    [Task]
    void StealBT(){
        if(GameManager.Instance.State != GameState.GameState)  return;

        npcCurrent = NPCState.Steal;

        //finished stealing, comes back to initial distance 
        //changes to CHASE state
        if (Mathf.Abs(distanceToPlayer.z) <= 0.2f)
        {
            distanceToPlayer = distanceToPlayer0;
            transform.position = player.transform.position + distanceToPlayer;
            npcCurrent = NPCState.Chase;
        }

        //steal
        distanceToPlayer = distanceToPlayer + (new Vector3(0, 0, 0.1f));
        transform.position = player.transform.position + distanceToPlayer;
        return;
    }

    //CHASE
    [Task]
    void ChaseBT(){
        if(GameManager.Instance.State != GameState.GameState)  return;

        //to ensure the enemy gets back to initial place after hiding
        if(npcCurrent == NPCState.Hide) {
            distanceToPlayer = distanceToPlayer0;
            transform.position = player.transform.position + distanceToPlayer;
        }

        npcCurrent = NPCState.Chase;

        //enemy gets progrisevely closer to the player
        distanceToPlayer = distanceToPlayer + (new Vector3(0, 0, 0.01f));
        transform.position = player.transform.position + distanceToPlayer;
        return;
    }

}
