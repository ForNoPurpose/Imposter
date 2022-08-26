using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayMenuController : MonoBehaviour
{
    private string _levelToLoad;
    [SerializeField] private GameObject noSavedGameDialog = null;
    [SerializeField] private GameObject deactivatePreviousMenu = null;

    public void NewGameButtonSelected()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void LoadGameButtonSelected()
    {
        if(PlayerPrefs.HasKey("SavedLevel"))
        {
            _levelToLoad = PlayerPrefs.GetString("SavedLevel");
            SceneManager.LoadScene(_levelToLoad);
        }
        else
        {
            deactivatePreviousMenu.SetActive(false);
            noSavedGameDialog.SetActive(true);
        }
    }
}
