using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public void QuitButtonSelected()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
