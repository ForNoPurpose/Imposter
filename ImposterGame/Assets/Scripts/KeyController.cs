using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour, IInteractable
{
    OpenDoor levelOneDoor;
    public void Interact()
    {
        gameObject.SetActive(false);
        levelOneDoor.playerHasKey = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        levelOneDoor = GameObject.FindGameObjectWithTag("LevelOneDoor").GetComponent<OpenDoor>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
