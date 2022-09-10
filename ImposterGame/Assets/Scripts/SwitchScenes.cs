using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour
{
    private void Start()
    {
        
    }
    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
