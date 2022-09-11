using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController GameInstance;

    private PlayerMovement _player;

    private GameState _state;

    private void Awake()
    {
        if(GameInstance == null)
        {
            GameInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _player = GetComponentInChildren<PlayerMovement>();

        SceneManager.sceneLoaded += SceneChange;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= SceneChange;
    }

    private void SceneChange(Scene scene, LoadSceneMode mode)
    {
        var startPosition = scene.GetRootGameObjects().Where(x => x.name.Contains("StartPosition")).FirstOrDefault();
        if (startPosition != null)
        {
            _player.transform.position = startPosition.transform.position;

        }
        else
        {
            Debug.LogWarning("No start position on this scene.");
            return;
        }
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
