using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Canvas HUD, menu;
    public GameObject player, manager, gameSpeaker, pauseSpeaker;
    public bool paused, gameOver;
    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        paused = true;
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (paused && Input.GetKeyDown(KeyCode.Return))
        {
            if (gameOver)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            else
                StartGame();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!paused)
                PauseGame();
            else
                QuitGame();
        }
    }

    public void StartGame()
    {
        HUD.gameObject.SetActive(true);
        gameSpeaker.SetActive(true);
        pauseSpeaker.SetActive(false);
        player.gameObject.GetComponent<PlayerShoot>().enabled = true;
        manager.gameObject.SetActive(true);
        menu.gameObject.SetActive(false);
        Time.timeScale = 1;
        paused = false;
    }

    public void PauseGame()
    {
        HUD.gameObject.SetActive(false);
        gameSpeaker.SetActive(false);
        pauseSpeaker.SetActive(true);
        player.gameObject.GetComponent<PlayerShoot>().enabled = false;
        manager.gameObject.SetActive(false);
        menu.gameObject.SetActive(true);
        Time.timeScale = 0;
        paused = true;
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
         Application.Quit();
        #endif
    }

}
