using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameController : MonoBehaviour
{
    public static GameController GameInstance;

    private PlayerMovement _player;

    private GameState _state;

    private void Awake()
    {
        GameInstance = this;

        _player = GetComponentInChildren<PlayerMovement>();
    }

    private void Update()
    {
        
    }

    public void SetState(GameState state)
    {

    }

    public enum GameState
    {
        NewGame,
        LoadGame,
        GameOver
    }
}
