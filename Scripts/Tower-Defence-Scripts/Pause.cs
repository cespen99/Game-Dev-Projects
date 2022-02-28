using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public bool paused, gameOver;
    public GameObject playSong, pauseSong, pauseMenu;
    void Start()
    {
        paused = false;
        gameOver = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && gameOver)
            {
                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                #else
                    Application.Quit();
                #endif
            }
                if (paused)
            {
                Time.timeScale = 1;
                pauseSong.GetComponent<AudioSource>().Pause();
                playSong.GetComponent<AudioSource>().Play();
                pauseMenu.SetActive(false);
                paused = false;
            }
            else
            {
                Time.timeScale = 0;
                pauseSong.GetComponent<AudioSource>().Play();
                playSong.GetComponent<AudioSource>().Pause();
                pauseMenu.SetActive(true);
                if (!gameOver)
                    paused = true;
            }
        }

    }
}
