using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInfo : MonoBehaviour
{
    [SerializeField] private float money = 100;
    [SerializeField] private float health = 100;

    [SerializeField] private Text moneyTxt, healthTxt;
    public Text gameOverText;

    void Update()
    {
        healthTxt.text = "Health: " + Mathf.Ceil(health).ToString();
        moneyTxt.text = "Money: " + money.ToString();
    }

    public void AddMoney(float x)
    {
        money += x;
    }

    public void TakeMoney(float x)
    {
        money -= x;
    }

    public bool CanAfford(float cost)
    {
        return money >= cost;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            gameOverText.text = "GAME OVER";
            GetComponent<Pause>().gameOver = true;
        }
    }
}
