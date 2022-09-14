using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System;

public class OpenDoor : MonoBehaviour, IInteractable
{
    private bool _playerDetected;
    public Transform doorPosition;
    public float doorWidth;
    public float doorHeight;
    SwitchScenes sceneTransition;
    public LayerMask whatIsPlayer;
    public bool doorIsLocked = false;

    [SerializeField] private string _sceneName;
    [SerializeField] private GameObject _lockSprite;
    [SerializeField] private GameObject _unlockedSprite;

    [SerializeField] private KeyType _keyType;

    public static event Action<KeyType> OnUnlock;

    KeyHolder keyHolder;

    private void Start()
    {
        keyHolder = FindObjectOfType<KeyHolder>();
        sceneTransition = FindObjectOfType<SwitchScenes>();
        _lockSprite.SetActive(false);
        _unlockedSprite.SetActive(false);
    }
    private void Update()
    {
        _playerDetected = Physics2D.OverlapBox(doorPosition.position, new Vector2(doorWidth, doorHeight), 0, whatIsPlayer);

        if (_playerDetected && doorIsLocked)
        {
            _lockSprite.SetActive(true);
            if(Input.GetKeyDown(KeyCode.E))
            {
                AudioManager.instance.PlaySound("ErrorSound");
            }
        }
        else
        {
            _lockSprite.SetActive(false);
        }
        if (_playerDetected && !doorIsLocked)
        {
            _unlockedSprite.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                AudioManager.instance.PlaySound("DoorSound");
                sceneTransition.SwitchScene(_sceneName);
            }
        }
        else
        {
            _unlockedSprite.SetActive(false);
        }

    }
    public void Interact()
    {
        Debug.Log("You interacted");
        if(keyHolder.ContainsKey(_keyType))
        {
            doorIsLocked = false;
            OnUnlock?.Invoke(_keyType);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(doorPosition.position, new Vector3(doorWidth, doorHeight, 1));
    }
}
