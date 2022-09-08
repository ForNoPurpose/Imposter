using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OpenDoor : MonoBehaviour
{
    private bool playerDetected;
    public Transform doorPosition;
    public float doorWidth;
    public float doorHeight;
    private PlayerInputActions inputActions;
    SwitchScenes sceneTransition;
    public LayerMask whatIsPlayer;
    private bool doorIsLocked = true;

    [SerializeField] private GameObject lockSprite;
    private void Start()
    {
        sceneTransition = FindObjectOfType<SwitchScenes>();
        lockSprite.SetActive(false);
    }
    private void Update()
    {
        playerDetected = Physics2D.OverlapBox(doorPosition.position, new Vector2(doorWidth, doorHeight), 0, whatIsPlayer);

        if (playerDetected && doorIsLocked)
        {
            lockSprite.SetActive(true);
        }
        else
        {
            lockSprite.SetActive(false);
        }
        if (playerDetected && !doorIsLocked)
        {
            inputActions = new PlayerInputActions();
            inputActions.Enable();

            if (Input.GetKeyDown(KeyCode.E))
            {
                sceneTransition.SwitchScene();
            }
        }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(doorPosition.position, new Vector3(doorWidth, doorHeight, 1));
    }
}
