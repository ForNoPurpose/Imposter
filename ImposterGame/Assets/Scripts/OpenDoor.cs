using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpenDoor : MonoBehaviour
{
    private bool playerDetected;
    public Transform doorPosition;
    public float doorWidth;
    public float doorHeight;
    private PlayerInputActions inputActions;
    SwitchScenes sceneTransition;
    public LayerMask whatIsPlayer;
    private void Start()
    {
        sceneTransition = FindObjectOfType<SwitchScenes>();
    }
    private void Update()
    {
        playerDetected = Physics2D.OverlapBox(doorPosition.position, new Vector2(doorWidth, doorHeight), 0, whatIsPlayer);

        if (playerDetected)
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
