using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System;

public class OpenDoor : MonoBehaviour, IInteractable
{
    private bool _playerDetected;
    public Transform entrancePosition;
    public float entranceWidth;
    public float entranceHeight;
    SwitchScenes sceneTransition;
    public LayerMask whatIsPlayer;
    public bool isStaircase;
    public bool isDoorway;
    public bool entranceIsLocked = false;

    [SerializeField] private string _sceneName;
    [SerializeField] private GameObject _lockSprite;
    [SerializeField] private GameObject _unlockedSprite;

    [SerializeField] private KeyType _keyType;

    public static event Action<KeyType> OnUnlock;

    KeyHolder keyHolder;

    private void Awake()
    {
        keyHolder = FindObjectOfType<KeyHolder>();
        sceneTransition = FindObjectOfType<SwitchScenes>();
        _lockSprite.SetActive(false);
        _unlockedSprite.SetActive(false);
    }
    private void Update()
    {
        _playerDetected = Physics2D.OverlapBox(entrancePosition.position, new Vector2(entranceWidth, entranceHeight), 0, whatIsPlayer);

        if (_playerDetected && entranceIsLocked)
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
        if (_playerDetected && !entranceIsLocked)
        {
            _unlockedSprite.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E) && isDoorway)
            {
                AudioManager.instance.PlaySound("DoorSound");
                sceneTransition.SwitchScene(_sceneName);
            }
            else if (Input.GetKeyDown(KeyCode.E) && isStaircase)
            {
                AudioManager.instance.PlaySound("StairsSound");
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
        if(keyHolder == null) keyHolder = FindObjectOfType<KeyHolder>();
        if(keyHolder.ContainsKey(_keyType))
        {
            entranceIsLocked = false;
            OnUnlock?.Invoke(_keyType);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(entrancePosition.position, new Vector3(entranceWidth, entranceHeight, 1));
    }
}
