using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SwitchScenes : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    private void Start()
    {
    }
    public void SwitchScene(string sceneName)
    {
        StartCoroutine(LoadScene(sceneName));
    }
    IEnumerator LoadScene(string sceneName)
    {
        transition.SetTrigger("SceneTransition");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneName);
    }
}
