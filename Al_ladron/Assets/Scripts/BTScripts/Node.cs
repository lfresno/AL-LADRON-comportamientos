using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node 
{
   private NodeState state;
   public Node parent;
   public List<Node> children;

   //constructors
   public Node()
   {
        parent = null;
   }

   public Node(List<Node> children)
   {
        foreach(Node child in children){
            Attach(child);
        }
   }

    //this method is used to attach nodes and their parents or and children
   private void Attach(Node node)
   {
        node.parent = this;
        children.Add(node);
   }

   //virtual functions that will have to be implemented in other classes derived from Node
   public virtual NodeState Evaluate() => NodeState.Failure;
}

public enum NodeState
{
    Running, 
    Success,
    Failure
}
