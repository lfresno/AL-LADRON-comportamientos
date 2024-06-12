using System.Collections;
using System.Collections.Generic;
using Panda;
using Unity.VisualScripting;
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

    //we will use a queue to store the last 3 obstacles the enemy has collided with.
    //in this case, a queue is better option than an array, so that the elements that colided first can be
    //removed easily
    private Queue<GameObject> obstacles = new Queue<GameObject>();

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

        //to check if there is any obstacle close enough, the collider of this object has been edited
        //to be able to reach the two adyacent tracks. If any colision is found, we will check if that 
        //obstacle is behind of the player, in which case, the enemy will hide behind it
        GameObject o = obstacles.Peek();

        
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
    void Steal()
    {
        Debug.Log("BT steal");

        //finished stealing, comes back to initial distance 
        if (Mathf.Abs(distanceToPlayer.z) <= 0.2f)
        {
            distanceToPlayer = distanceToPlayer0;
            transform.position = player.transform.position + distanceToPlayer;
        }

        //steal
        distanceToPlayer = distanceToPlayer + (new Vector3(0, 0, 0.1f));
        transform.position = player.transform.position + distanceToPlayer;
        return;
    }


    //CHASING
    [Task]
    void Chase(){
        //Debug.Log("BT chase " + distanceToPlayer.z);

        //enemy gets progrisevely closer to the player
        distanceToPlayer = distanceToPlayer + (new Vector3(0, 0, 0.01f));
        transform.position = player.transform.position + distanceToPlayer;
        return;
    }


    //other methods
    void OnTriggerEnter(Collider collision){
        Debug.Log("ME CHOCO CON " + collision.gameObject.name);
        //we are only interested in collisions with obstacles
        if(!collision.gameObject.tag.Equals("Obstacle")) return;

        Debug.Log("CONFIRMADO  " + collision.gameObject.name);
        //we only want to know the 3 last collisions
        if(obstacles.Count > 3){
            obstacles.Dequeue();
        }
        obstacles.Enqueue(collision.gameObject);

        GameObject o = obstacles.Peek();
        Debug.Log("             POSICIÓN "+ o.transform.position+ "?? "+ obstacles.Peek().transform.position);
    }

    void OnCollisionEnter(Collision collider) { //no lo pilla
        Debug.Log("CHOCO CON COLLIDER "+ collider.gameObject.name);
    }
    
}
