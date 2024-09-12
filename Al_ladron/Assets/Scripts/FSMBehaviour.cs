using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class FSMBehaviour : NPCBase
{
    //Player
    // public GameObject player;
    // public PlayerMovement playerMovement;
    // private Vector3 distanceToPlayer0;
    // private Vector3 distanceToPlayer;

    //FSM
    [SerializeField] private State state;

    public override void Start()
    {
        distanceToPlayer0 = transform.position - player.transform.position;
        distanceToPlayer = distanceToPlayer0;
        playerMovement = player.GetComponent<PlayerMovement>();

        state = State.Chase;
    }

    public override void Update()
    {
        if(GameManager.Instance.State != GameState.GameState)  return;

        switch(state) {
            case State.Chase:
                // Debug.Log("CHASE + " + distanceToPlayer.z);

                //if enemy is close enough, changes to STEAL state 
                if(Mathf.Abs(distanceToPlayer.z) <= 4.5f) { 
                    state = State.Steal;
                    break;
                }

                //if player looks back, change to HIDE state
                if(playerMovement.lookingBack) {
                    distanceToPlayer = distanceToPlayer0;
                    state = State.Hide;
                    break;
                }

                //chase player
                //enemy gets progrisevely closer to the player
                distanceToPlayer = distanceToPlayer + (new Vector3(0, 0, 0.01f));
                transform.position = player.transform.position + distanceToPlayer;
                break;

            case State.Steal:
                // Debug.Log("STEAL");

                //finished stealing, comes back to initial distance and changes to CHASE state
                if(Mathf.Abs(distanceToPlayer.z) <= 0.2f) {
                    distanceToPlayer = distanceToPlayer0;
                    transform.position = player.transform.position + distanceToPlayer;
                    state = State.Chase;
                    break;
                }

                //steal
                distanceToPlayer = distanceToPlayer + (new Vector3(0, 0, 0.1f));
                transform.position = player.transform.position + distanceToPlayer;
                break;

            case State.Hide:
                // Debug.Log("HIDE");

                //finished hiding, comes back to initial distance and changes to CHASE state
                if(!playerMovement.lookingBack) {
                    distanceToPlayer = distanceToPlayer0;
                    transform.position = player.transform.position + distanceToPlayer;
                    state = State.Chase;
                    break;
                }

                //hide
                //this enemy will be represented by a bird, so their way to hide is fly away
                distanceToPlayer = distanceToPlayer + (new Vector3(0.05f, 0.3f, 0.2f));
                transform.position = player.transform.position + distanceToPlayer;
                break;
                
            default:
                break;
        }
    }
}

//STATES
public enum State   //the NPC will chase the player, if they are close enough, it will steal them. If the player turns around to check on it, it will hide.
{
    Chase,
    Steal,
    Hide
}

