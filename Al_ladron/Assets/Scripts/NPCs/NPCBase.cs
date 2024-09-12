
using UnityEngine;

public abstract class NPCBase: MonoBehaviour
{
    public NPCState npcCurrent;

    //Player
    public GameObject player;
    public PlayerMovement playerMovement;
    public Vector3 distanceToPlayer0;
    public Vector3 distanceToPlayer;


    public abstract void Start();
    public abstract void Update();


    //ACTIONS (all NPCs will do the same actions)
    public void Chase(){

        npcCurrent = NPCState.Chase;

        //enemy gets progrisevely closer to the player
        distanceToPlayer = distanceToPlayer + (new Vector3(0, 0, 0.01f));
        transform.position = player.transform.position + distanceToPlayer;
        return;
    }

    public void Steal(){

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

    public void Hide(){

        npcCurrent = NPCState.Hide;

        //this enemy will be represented by a bird, so their way to hide is fly away
        distanceToPlayer = distanceToPlayer + (new Vector3(0.05f, 0.3f, 0.2f));
        transform.position = player.transform.position + distanceToPlayer;

    }
    
}

public enum NPCState
{
    Chase,
    Steal,
    Hide
}
