using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitNode : ActionNode
{
    public float waitTime = 1;
    private float startTime;
    protected override void OnStart()
    {
        startTime = Time.time;
    }

    protected override void OnStop()
    {
    }

    protected override NodeState OnUpdate()
    {
        if (Time.time - startTime > waitTime)
        {
            return NodeState.Success;
        }
        return NodeState.Running;
    }
}
