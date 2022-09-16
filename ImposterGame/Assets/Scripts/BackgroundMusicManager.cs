using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicManager : MonoBehaviour
{
    static BackgroundMusicManager instance;

    public AudioClip[] musicClips;
    public AudioSource Audio;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoad;
    }
    void OnSceneLoad(Scene scene, LoadSceneMode sceneMode)
    {
        AudioSource source = new AudioSource();

        switch (scene.name)
        {
            case "TitleScene":
                source.clip = musicClips[0];
                break;

            case "NewGameScene":
                source.enabled = false;
                break;

            default:
                source.clip = musicClips[1];
                break;
        }

        if (source.clip != Audio.clip)
        {
            Audio.enabled = false;
            Audio.clip = source.clip;
            Audio.enabled = true;
        }
    }
}
