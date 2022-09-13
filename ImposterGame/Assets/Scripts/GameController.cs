using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using CustomUtilities;

public sealed class GameController : MonoBehaviour
{
    public static GameController GameInstance;

    private PlayerMovement _player;
    private Vector3 _startPosition;
    private Vector3 _exitPosition;

    private GameState _state;

    private Dictionary<string, Utils.SceneState> _sceneList = new();

    private void Start()
    {
        if(GameInstance == null)
        {
            GameInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        _player = GetComponentInChildren<PlayerMovement>();

        _sceneList = Utils.GetSceneList();
        _sceneList[SceneManager.GetActiveScene().name].AddVisit();

        SceneManager.sceneLoaded += SceneChange;
        SceneManager.sceneUnloaded += GetExitPosition;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= SceneChange;
        SceneManager.sceneUnloaded -= GetExitPosition;
    }

    private void SceneChange(Scene scene, LoadSceneMode mode)
    {
        
        var startObject = scene.GetRootGameObjects().Where(x => x.name.Contains("StartPosition")).FirstOrDefault();
        if (startObject != null)
        {
            if(_sceneList[scene.name].visitCount > 0)
            {
                _player.transform.position = _sceneList[scene.name].exitPos;
            }
            else
            {

                _player.transform.position = startObject.transform.position;
            }
        }
        else
        {
            Debug.LogWarning("No start position on this scene.");
            return;
        }
        if (_sceneList.ContainsKey(scene.name))
        {
            _sceneList[scene.name].AddVisit();
            Debug.Log(_sceneList[scene.name].visitCount);
        }
    }

    private void GetExitPosition(Scene scene)
    {
        _sceneList[scene.name].SetExitPos(_player.transform.position);
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
