using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMBehaviour : MonoBehaviour
{

    //Player
    public GameObject player;
    public PlayerMovement playerMovement;
    private Vector3 distanceToPlayer0;
    private Vector3 distanceToPlayer;

    //FSM
    [SerializeField] private State state = State.Chase;

    void Start()
    {
        distanceToPlayer0 = transform.position - player.transform.position;
        distanceToPlayer = distanceToPlayer0;
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        switch(state) {
            case State.Chase:

                //if enemy is close enough, changes to STEAL state 
                if(distanceToPlayer.z <= 1.5f) { 
                    distanceToPlayer = distanceToPlayer0;
                    state = State.Steal;
                    break;
                }

                //if player looks back, change to HIDE state
                if(playerMovement.lookingBack == true) {
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
                //steal
                break;

            case State.Hide:
                //hide
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

