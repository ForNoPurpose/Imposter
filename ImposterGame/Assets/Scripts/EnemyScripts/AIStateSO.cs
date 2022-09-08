using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIStateSO : ScriptableObject
{
    public abstract void State(Controller controller);
}
