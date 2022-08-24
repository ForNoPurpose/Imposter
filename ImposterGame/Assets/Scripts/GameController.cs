using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameController : MonoBehaviour
{
    private Grid gameGrid;
    private Tilemap[] gameTilemaps;

    private PlayerMovement player;

    private void Awake()
    {
        gameTilemaps = GetComponentsInChildren<Tilemap>();
        player = GetComponentInChildren<PlayerMovement>();
    }

    private void Update()
    {
        DisplayTilemaps();
    }

    private void DisplayTilemaps()
    {
        foreach(Tilemap tilemap in gameTilemaps)
        {
            if(player.worldPosition != tilemap.GetComponent<WorldLayer>().layerNumber)
            {
                //Debug.Log("layers should go inactive.");
                tilemap.gameObject.SetActive(false);
            }
            else
            {
                tilemap.gameObject.SetActive(true);
            }
        }
    }
}
