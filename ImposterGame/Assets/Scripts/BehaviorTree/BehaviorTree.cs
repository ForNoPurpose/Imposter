using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class BehaviorTree : ScriptableObject
{
    public Node RootNode { get; set; }
    public NodeState TreeState { get; set; } = NodeState.Running;

    public NodeState Update()
    {
        if (RootNode.State == NodeState.Running)
        {
            TreeState = RootNode.Update();
        }
        return TreeState;
    }
}
