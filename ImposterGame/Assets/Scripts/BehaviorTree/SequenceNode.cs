using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceNode : CompositeNode
{
    private int current;
    private Node child;

    protected override void OnStart()
    {
        current = 0;
    }

    protected override void OnStop()
    {
    }

    protected override NodeState OnUpdate()
    {
        child = Children[current];

        switch (child.Update())
        {
            case NodeState.Running:
                return NodeState.Running;
            case NodeState.Failure:
                return NodeState.Failure;
            case NodeState.Success:
                current++;
                break;
        }

        return current == Children.Count ? NodeState.Success : NodeState.Running;
    }
}