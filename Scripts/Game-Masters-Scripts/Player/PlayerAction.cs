using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerAction : MonoBehaviour
{
    public int playerClass;
    public int state = 0; //(0=dead, 1=idle, 2=attacking, 3=shielding, 4=evading)
    public bool paused, gameOver;
    public GameObject spellScreen, fade, inputHints, txtInfo, gameOverText;

    private void Start()
    {
        fade.SetActive(true);
        paused = false;
    }
    void Update()
    {
        if (GetComponent<Damage>().health <= 0)
        {
            gameOver = true;
            gameOverText.SetActive(true);
            OnPause();
        }
    }
    public void OnPrimary(InputValue value)
    {
        if(!paused && !gameOver)
            GetComponent<FighterAttack>().Attack();
    }

    public void OnSecondary(InputValue value)
    {
        if (!paused && !gameOver)
            GetComponent<WizardAttack>().Spell();
    }

    public void OnClassSelect(InputValue value)
    {
        if (!paused && !gameOver)
            GetComponent<WizardAttack>().SelectSpell(int.Parse(value.Get().ToString()));
    }

    public void OnPause()
    {
        if (paused && !gameOver)
        {
            paused = false;
            Time.timeScale = 1;
            spellScreen.SetActive(false);
            inputHints.SetActive(true);
            txtInfo.SetActive(true);
        }
        else
        {
            paused = true;
            Time.timeScale = 0;
            spellScreen.SetActive(true);
            inputHints.SetActive(false);
            txtInfo.SetActive(false);
        }
    }
}
