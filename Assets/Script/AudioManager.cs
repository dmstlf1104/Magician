using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private GameObject[] musics;

    public AudioClip[] musicClips;

    private int currentClipIndex = -1;

    private void Awake()
    {
        musics = GameObject.FindGameObjectsWithTag("Music");

        if (musics.Length >= 2)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(transform.gameObject);
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int sceneIndex = scene.buildIndex;

        if (sceneIndex == 0 || sceneIndex == 1 || sceneIndex == 2)
        {
            PlayMusic(0);
        }
        else if (sceneIndex == 3)
        {
            PlayMusic(1);
        }
        else if (sceneIndex == 4)
        {
            PlayMusic(2);
        }
        else if (sceneIndex == 5)
        {
            PlayMusic(3);
        }
    }

    private void PlayMusic(int musicClip)
    {
        if (musicClip == currentClipIndex)
        {
            return;
        }

        AudioSource audioSource = GetComponent<AudioSource>();

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        audioSource.clip = musicClips[musicClip];
        audioSource.Play();

        currentClipIndex = musicClip;
    }
}
