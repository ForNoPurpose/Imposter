using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Node : ScriptableObject
{
    public NodeState State { get; private set; } = NodeState.Running;
    public bool Started { get; private set; } = false;

    public NodeState Update()
    {
        if (!Started)
        {
            OnStart();
            Started = true;
        }

        State = OnUpdate();
        
        if (State == NodeState.Failure || State == NodeState.Success)
        {
            OnStop();
            Started = false;
        }

        return State;
    }


    protected abstract void OnStart();
    protected abstract void OnStop();
    protected abstract NodeState OnUpdate();

}

public enum NodeState
{
    Running,
    Failure,
    Success
}
