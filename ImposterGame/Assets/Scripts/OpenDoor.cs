using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private bool playerDetected;
    public Transform doorPosition;
    public float doorWidth;
    public float doorHeight;
    private PlayerInputActions inputActions;
    SwitchScenes sceneTransition;
    private void Start()
    {
        sceneTransition = FindObjectOfType<SwitchScenes>();
    }
    private void Update()
    {
        playerDetected = Physics2D.OverlapBox(doorPosition.position, new Vector2(doorWidth, doorHeight), 0);

        if (playerDetected)
        {
            inputActions = new PlayerInputActions();
            inputActions.Enable();

            if (inputActions.Player.EnterExitDoor.triggered)
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
