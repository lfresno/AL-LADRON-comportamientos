using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class USBehaviour : MonoBehaviour
{
    //aquí defino qué hace cada acción y la ejecuto

    private NavMeshAgent navmesh;
    public USBrain usBrain;
    public Action[] actionsAvailable;


    void Start()
    {
        navmesh = GetComponent<NavMeshAgent>();
        usBrain = GetComponent<USBrain>();

    }

    void Update()
    {
        
    }

    public void MoveTo (Vector3 pos) 
    {
        navmesh.destination = pos;
    }
}
