using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public SwitchScenes _endGame;
    public string sceneName;
    private EnemyController _enemyController;

    private void Awake()
    {
        _enemyController = GetComponent<EnemyController>();
    }

    private void OnDisable()
    {
            _endGame.SwitchScene(sceneName);
    }
}
