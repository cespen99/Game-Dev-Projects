using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadPrefs : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 1;
        if (SceneManager.GetActiveScene().name != "Forest")
        {
            PlayerPrefs.SetInt("LevelIndex", SceneManager.GetActiveScene().buildIndex);
            GetComponent<Damage>().health = PlayerPrefs.GetFloat("PlayerHealth");
            GetComponent<PlayerLevel>().xp = PlayerPrefs.GetFloat("PlayerXP");
            GetComponent<PlayerLevel>().points = PlayerPrefs.GetFloat("PlayerPoints");
            GetComponent<WizardAttack>().selectSpell = PlayerPrefs.GetInt("SelectSpell");
        }
        else
        {
            GetComponent<Damage>().health = 100;
            GetComponent<PlayerLevel>().xp = 0;
            GetComponent<PlayerLevel>().points = 0;
            GetComponent<WizardAttack>().selectSpell = -1;
        }
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("Menu Screen");
    }

    public void Reload()
    {
        GetComponent<Damage>().health = PlayerPrefs.GetFloat("PlayerHealth");
        GetComponent<PlayerLevel>().xp = PlayerPrefs.GetFloat("PlayerXP");
        GetComponent<PlayerLevel>().points = PlayerPrefs.GetFloat("PlayerPoints");
        GetComponent<WizardAttack>().selectSpell = PlayerPrefs.GetInt("SelectSpell");
        SceneManager.LoadScene(PlayerPrefs.GetInt("LevelIndex"));
    }

    public void SaveAll()
    {
        PlayerPrefs.SetFloat("PlayerHealth", GetComponent<Damage>().health);
        PlayerPrefs.SetFloat("PlayerXP", GetComponent<PlayerLevel>().xp);
        PlayerPrefs.SetFloat("PlayerPoints", GetComponent<PlayerLevel>().points);
        PlayerPrefs.SetInt("SpellSelect", GetComponent<WizardAttack>().selectSpell);

        for (int i = 0; i < GetComponent<WizardAttack>().spellsKnown.Count; i++)
        {
            if(GetComponent<WizardAttack>().spellName[GetComponent<WizardAttack>().spellsKnown[i]] != null)
                PlayerPrefs.SetString("PlayerSpell" + i, GetComponent<WizardAttack>().spellName[GetComponent<WizardAttack>().spellsKnown[i]]);
        }
    }


}
