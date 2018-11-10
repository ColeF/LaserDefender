using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {

    static MusicPlayer instance = null;

    public AudioClip startGame;
    public AudioClip Game;
    public AudioClip endGame;

    private AudioSource music;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
            music = GetComponent<AudioSource>();
            music.clip = startGame;
            music.loop = true;
            music.Play();
        }

        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("MusicPlayer: loaded level " + scene.buildIndex);
        music.Stop();
        if (scene.buildIndex == 0) { music.clip = startGame; }
        else if (scene.buildIndex == 1) { music.clip = Game; }
        else if (scene.buildIndex == 2) { music.clip = endGame; }
        music.loop = true;
        music.Play();
    }
}
