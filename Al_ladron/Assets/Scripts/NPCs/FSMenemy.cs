using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMenemy : NPCBase
{

    public override void Start()
    {
        //set a start position and store its distance from the player
        distanceToPlayer0 = transform.position - player.transform.position;
        distanceToPlayer = distanceToPlayer0;
        playerMovement = player.GetComponent<PlayerMovement>();

        npcCurrent = NPCState.Chase;
    }

    public override void Update()
    {
        if(GameManager.Instance.State != GameState.GameState) return;

        switch(npcCurrent)
        {
            case NPCState.Chase:
                //check if we have to change state
                if(checkPlayerClose() || checkPlayerLooking()) break;

                Chase();
                break;

            case NPCState.Steal:
                //check if we have to hide
                if(checkPlayerLooking()) break;
                
                Steal();
                break;

            case NPCState.Hide:
                //check if we can chase
                if(checkPlayerStoppedLooking()) break;

                Hide();
                break;

            default:
                break;
        }

    }

    //PERCEPTIONS (will cause transitions between states )

    //checks if  the player is close enough to steal from them.
    //changes to steal state if they are
    bool checkPlayerClose()
    {
        if (Mathf.Abs(distanceToPlayer.z) <= 4.5f) 
        { 
            npcCurrent = NPCState.Steal; 
            return true;
        }
        return false;
    }

    //check if the player is looking towards the enemies
    //changes to hide state 
    bool checkPlayerLooking()
    {
        if (playerMovement.lookingBack)
        {
            distanceToPlayer = distanceToPlayer0;
            npcCurrent = NPCState.Hide;
            return true;
        }
        return false;
    }

    //check if player stopped looking towards the enemies
    //changes the state to chase
    bool checkPlayerStoppedLooking()
    {
        if (!playerMovement.lookingBack) 
        {
            distanceToPlayer = distanceToPlayer0;
            transform.position = player.transform.position + distanceToPlayer;
            npcCurrent = NPCState.Chase;
            return true;
        }
        return false;
    }

}
