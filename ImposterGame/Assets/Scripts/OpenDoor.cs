using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OpenDoor : MonoBehaviour
{
    private bool _playerDetected;
    public Transform doorPosition;
    public float doorWidth;
    public float doorHeight;
    private PlayerInputActions _inputActions;
    SwitchScenes sceneTransition;
    public LayerMask whatIsPlayer;
    public bool doorIsLocked = false;
    public bool playerHasKey = false;

    [SerializeField] private string _sceneName;

    [SerializeField] private GameObject _lockSprite;
    [SerializeField] private GameObject _unlockedSprite;
    private void Start()
    {
        sceneTransition = FindObjectOfType<SwitchScenes>();
        _lockSprite.SetActive(false);
        _unlockedSprite.SetActive(false);
    }
    private void Update()
    {
        _playerDetected = Physics2D.OverlapBox(doorPosition.position, new Vector2(doorWidth, doorHeight), 0, whatIsPlayer);

        if (_playerDetected && !playerHasKey)
        {
            doorIsLocked = true;
            _lockSprite.SetActive(true);
        }
        else
        {
            _lockSprite.SetActive(false);
        }
        if (_playerDetected && playerHasKey)
        {
            doorIsLocked = false;
            _unlockedSprite.SetActive(true);
            _inputActions = new PlayerInputActions();
            _inputActions.Enable();

            if (Input.GetKeyDown(KeyCode.E))
            {
                sceneTransition.SwitchScene(_sceneName);
            }
        }
        else
        {
            _unlockedSprite.SetActive(false);
        }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(doorPosition.position, new Vector3(doorWidth, doorHeight, 1));
    }
}
