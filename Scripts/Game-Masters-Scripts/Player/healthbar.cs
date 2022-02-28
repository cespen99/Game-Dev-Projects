using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthbar : MonoBehaviour
{
    public Slider healthBar;
    public Damage playerHealth;
    public Slider manaBar;
    public WizardAttack playerMana;

    public void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Damage>();
        playerMana = GameObject.FindGameObjectWithTag("Player").GetComponent<WizardAttack>();

        healthBar.maxValue = 100;
	    healthBar.value = playerHealth.health;

        manaBar.maxValue = playerMana.manaMax;
        manaBar.value = playerMana.mana;
    }

    public void Update()
    {
        healthBar.value = playerHealth.health;
        manaBar.value = playerMana.mana;
    }
}
