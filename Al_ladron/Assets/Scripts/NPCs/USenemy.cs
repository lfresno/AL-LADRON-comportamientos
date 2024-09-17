using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class USenemy : NPCBase
{
    //utility of each action 
    [SerializeField] double stealingUtility = 0;
    [SerializeField] double chasingUtility = 0;
    [SerializeField] double hidingingUtility = 0;

    //utility curves
    private double security;
    private double insecurity;
    private double fear;

    //inertia
    float lastChanged;
    NPCState previousState;

    public override void Start()
    {
        //set a start position and store its distance from the player
        distanceToPlayer0 = transform.position - player.transform.position;
        distanceToPlayer = distanceToPlayer0;
        playerMovement = player.GetComponent<PlayerMovement>();

        npcCurrent = NPCState.Chase;
        previousState = NPCState.Chase;
        lastChanged = Time.time;
    }

    public override void Update()
    {
        if(GameManager.Instance.State != GameState.GameState) return;

        //curves
        security = (distanceToPlayer.magnitude / distanceToPlayer0.magnitude);
        insecurity = 1/(1 + Math.Exp(-10*((distanceToPlayer.magnitude / distanceToPlayer0.magnitude) - 0.5)));
        fear = playerMovement.lookingBack ? 1 : 0;


        //utility functions
        //the results are clamped between 0 and 1
        stealingUtility = Math.Clamp(1 / (1 + Math.Exp(13*(distanceToPlayer.magnitude / distanceToPlayer0.magnitude) - 0.4)), 0, 1);   //this is also the curve for the factor ease to steal
        chasingUtility = Math.Clamp(0.65*security + 0.35*fear, 0, 1);
        hidingingUtility = Math.Clamp(0.3*insecurity + 0.7*fear, 0, 1);

        ChooseAction(stealingUtility, chasingUtility, hidingingUtility);
    }

    void ChooseAction(double stealUt, double chaseUt, double hideUt) {

        if(Time.time - lastChanged >= 0.2f) {

            if((chaseUt >= stealUt) && (chaseUt >= hideUt)) { 
                Chase();
            } else if((stealUt > chaseUt) && (stealUt >= hideUt)) {
                Steal();
            } else {
                Hide();
            }

            if(previousState != npcCurrent) {
                if (previousState == NPCState.Hide)
                {
                    //before finishing this action, enemy goes back to initial position
                    distanceToPlayer = distanceToPlayer0;
                    transform.position = player.transform.position + distanceToPlayer;
                }
                previousState = npcCurrent;
                lastChanged = Time.time;
            }
        } else {
            
            switch(npcCurrent) {
                case NPCState.Chase:
                    Chase();
                    break;
                
                case NPCState.Steal:
                    Steal();
                    break;

                case NPCState.Hide:
                    Hide();
                    break;

                default: break;
            }
        }

        return;
    }
}
