using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeBase : MonoBehaviour
{
    public NodeState state;
}

public enum NodeState
{
    Unknown, 
    Running,
    Success,
    Failure
}