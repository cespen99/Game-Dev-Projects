using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject menumanager;

    void Start()
    {
	MainMenuButton();
    }

    public void NewGameButton()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString("NewPlayer", "yes");
        PlayerPrefs.SetInt("LevelIndex", 1);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Forest");
    }

    public void Continue()
    {
        if(PlayerPrefs.GetInt("LevelIndex") > 0)
            UnityEngine.SceneManagement.SceneManager.LoadScene(PlayerPrefs.GetInt("LevelIndex"));
    }

    // Update is called once per frame
    public void MainMenuButton()
    {
        menumanager.SetActive(true);
    }

    public void QuitButton()
    {
	Application.Quit();
    }
}


