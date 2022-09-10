using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        gameObject.SetActive(false);
        GetComponent<OpenDoor>().playerHasKey = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("LevelOneDoor");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
