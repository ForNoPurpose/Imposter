using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldLayer : MonoBehaviour
{
    public enum LayerNumber { 
        Background = 1,
        Middleground = 2,
        Foreground = 3
    }

    public LayerNumber layerNumber;
}
