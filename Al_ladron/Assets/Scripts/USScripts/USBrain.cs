using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class USBrain : MonoBehaviour
{

    //aquí voy a mirar las considerations, puntuar las acciones y elegir la acción a realizar
    private USBehaviour usEnemy;


    //considerations


    //actions
    private enum Action {
        Chase,
        Steal,
        Hide,
        RunAway
    }
    

    void Start()
    {
        usEnemy = GetComponent<USBehaviour>();
    }

    void Update()
    {
        
    }

    // public void ScoreAction(Action a){

    // }

    // public void SelectAction(Action[] a){

    // }
}
