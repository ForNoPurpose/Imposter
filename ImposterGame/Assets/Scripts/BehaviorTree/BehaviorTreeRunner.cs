using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTreeRunner : MonoBehaviour
{
    BehaviorTree tree;

    private void Start()
    {
        tree = ScriptableObject.CreateInstance<BehaviorTree>();

        var log = ScriptableObject.CreateInstance<DebugLogNode>();
        log.message = "Hello World! 111";
        
        var log2 = ScriptableObject.CreateInstance<DebugLogNode>();
        log2.message = "Hello World! 222";

        var log3 = ScriptableObject.CreateInstance<DebugLogNode>();
        log3.message = "Hello World! 333";

        var wait = ScriptableObject.CreateInstance<WaitNode>();

        var sequence = ScriptableObject.CreateInstance<SequenceNode>();
        sequence.Children.Add(log);
        sequence.Children.Add(wait);
        sequence.Children.Add(log2);
        sequence.Children.Add(wait);
        sequence.Children.Add(log3);
        sequence.Children.Add(wait);

        var loop = ScriptableObject.CreateInstance<RepeatNode>();
        loop.Child = sequence;

        tree.RootNode = loop;
    }

    private void Update()
    {
        tree.Update();
    }
}
