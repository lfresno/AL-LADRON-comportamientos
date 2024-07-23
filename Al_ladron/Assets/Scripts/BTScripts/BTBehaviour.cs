using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

using Panda;
//using System.Numerics;

public class BTBehaviour : MonoBehaviour
{
    //I'm using PandaBT to create the BT logic
    //this script is used to define the actions that each node must do. 
    PandaBehaviour pandaBT;

    //Player
    public GameObject player;
    public PlayerMovement playerMovement;
    private Vector3 distanceToPlayer0;
    private Vector3 distanceToPlayer;

    //we will use a queue to store the last 3 obstacles the enemy has collided with.
    //in this case, a queue is better option than an array, so that the elements that colided first can be
    //removed easily
    private Queue<GameObject> obstacles = new Queue<GameObject>();
    private Vector3 hidePos;
    private Vector3 distanceToObstacle;

    [SerializeField] private Material material;
    private Color initialColor;

    void Start()
    {

        pandaBT = GetComponent<PandaBehaviour>();

        distanceToPlayer0 = transform.position - player.transform.position;
        distanceToPlayer = distanceToPlayer0;
        playerMovement = player.GetComponent<PlayerMovement>();

        material = this.gameObject.GetComponent<Renderer>().material;
        initialColor = material.color;
    }

    void Update(){
        if(GameManager.Instance.State != GameState.GameState)  return;

        pandaBT.Tick();
        pandaBT.Reset();
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
        if(obstacles.Count() == 0) return false;
        GameObject o = obstacles.Peek();
        Debug.Log("             POSICIÓN "+ o.transform.position);

        //the enemy won't be able to ide behind an obstacle ahead of the player or an obstacle too far away
        //we check this condition and store the position of the closest obstacle that succeeds in it
        if ((o.transform.position.z < player.transform.position.z) && (Mathf.Abs(o.transform.position.z - this.transform.position.z) < 50.0f))
        {
            hidePos = o.transform.position;
            distanceToObstacle = hidePos - this.transform.position;
            Debug.Log("A ESCONDERSEEEEEEEE");
            return true;
        }

        GameObject o2 = obstacles.ElementAt<GameObject>(1);
        if ((o2.transform.position.z < player.transform.position.z) && (Mathf.Abs(o2.transform.position.z - this.transform.position.z) < 50.0f))
        {
            hidePos = o.transform.position;
            distanceToObstacle = hidePos - this.transform.position;
            return true;
        }

        GameObject o3 = obstacles.ElementAt<GameObject>(1);
        if ((o3.transform.position.z < player.transform.position.z) && (Mathf.Abs(o3.transform.position.z - this.transform.position.z) < 50.0f))
        {
            hidePos = o.transform.position;
            distanceToObstacle = hidePos - this.transform.position;
            return true;
        }

        return false;
    }

    [Task]
    void HideObstacle()
    {    //hide behind an obstacle
        if(GameManager.Instance.State != GameState.GameState)  return;

        //finished hiding, comes back to initial distance from player 
        if(!playerMovement.lookingBack){
            distanceToPlayer = distanceToPlayer0;
            transform.position = player.transform.position + distanceToPlayer;
            return;
        }

        //hide
        if (Mathf.Abs(distanceToObstacle.z) >= 0.2f) 
        {
            Debug.Log("ESCONDERSE YAAAAA");

            // transform.position = Vector3.MoveTowards(transform.position, hidePos, 15.0f*Time.deltaTime);

            distanceToObstacle = distanceToObstacle + new Vector3(0, 0, 0.1f);
            transform.position = player.transform.position + distanceToObstacle;
        }
        //reached position, wait until player stops looking Back

    }

    [Task]
    void HideInvisible()
    {   //hide by making invisible
        if(GameManager.Instance.State != GameState.GameState)  return;

        //finished hiding, comes back to initial distance from player 
        if (!playerMovement.lookingBack)
        {
            distanceToPlayer = distanceToPlayer0;
            transform.position = player.transform.position + distanceToPlayer;
            material.SetColor("_Color", initialColor);
            return;
        }

        //the enemy will get more and more transparent
        if(material.color.a >= 0.3){
            Color old = material.color;
            float oldAlbedo = old.a;
            Color newColor = new Color(old.r, old.g, old.b, oldAlbedo-0.2f);
            material.SetColor("_Color", newColor);
            //material.color = newColor;
        }
    }

    //STEALING
    [Task]
    bool isPlayerCloserThan(float num){ //check if player is close enough to steal
        return (Mathf.Abs(distanceToPlayer.z) <= num);
    }

    [Task]
    void Steal()
    {
        if(GameManager.Instance.State != GameState.GameState)  return;

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
        if(GameManager.Instance.State != GameState.GameState)  return;

        Debug.Log("BT chase " + distanceToPlayer.z);

        //enemy gets progrisevely closer to the player
        distanceToPlayer = distanceToPlayer + (new Vector3(0, 0, 0.01f));
        transform.position = player.transform.position + distanceToPlayer;
        return;
    }


    //other methods
    void OnTriggerEnter(Collider collision){
        //we are only interested in collisions with obstacles
        if(!collision.gameObject.tag.Equals("Obstacle")) return;

        Debug.Log("ME CHOCO CON  " + collision.gameObject.name);
        //we only want to know the 3 last collisions
        if(obstacles.Count > 3){
            obstacles.Dequeue();
        }
        obstacles.Enqueue(collision.gameObject);
    }

    void OnCollisionEnter(Collision collider) { //no lo pilla
        Debug.Log("CHOCO CON COLLIDER "+ collider.gameObject.name);
    }
    
}
