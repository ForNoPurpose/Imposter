using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public sealed class Dialogue
{
    public string Name;
    [TextArea(3, 10)]
    public List<string> Lines;
}
