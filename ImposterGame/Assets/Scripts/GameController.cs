using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameController : MonoBehaviour
{
    public static GameController gameInstance;
    private Grid gameGrid;
    private List<Tilemap> gameTilemaps = new();
    private List<DoorTile> doorTiles;

    private PlayerMovement player;

    private void Awake()
    {
        gameInstance = this;

        gameTilemaps.AddRange(GetComponentsInChildren<Tilemap>());
        player = GetComponentInChildren<PlayerMovement>();

        doorTiles = GetDoorTiles();
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

    public Direction? CheckPlayerNearDoor(Vector3 playerPos)
    {
        //Tilemap tilemapRef = gameTilemaps.Where(x => x.name == "BackgroundTilemap").FirstOrDefault();
        //TileBase tile = tilemapRef.GetTile(Vector3Int.FloorToInt(playerPos));
        Vector3Int playerGridPos = Vector3Int.FloorToInt(playerPos);
        foreach (var door in doorTiles)
        {
            if(door._position == playerGridPos)
            {
                Debug.Log($"Player is on: {door._name} at Pos: {playerGridPos}");
                return door._direction;
            }
        }
        return null;
    }

    private List<DoorTile> GetDoorTiles()
    {
        //BoundsInt bounds;
        TileBase tempTile;
        List<DoorTile> result = new();
        foreach(Tilemap tilemap in gameTilemaps)
        {
            foreach(var pos in tilemap.cellBounds.allPositionsWithin)
            {
                tempTile = tilemap.GetTile(pos);
                if(tempTile != null && tempTile.name.Contains("Door"))
                {
                    var direction = tilemap.name == "BackgroundTilemap" ? Direction.ToSubLevel : Direction.ToForeLevel;
                    result.Add(new DoorTile(tilemap.name + tempTile.name, pos, direction));
                }
            }
        }
        return result;
    }

    struct DoorTile
    {
        public string _name;
        public Vector3Int _position;
        public Direction _direction;

        public DoorTile(string name, Vector3Int position, Direction direction)
        {
            _name = name;
            _position = position;
            _direction = direction;
        }
    }

    public enum Direction
    {
        ToSubLevel,
        ToForeLevel
    }
}
