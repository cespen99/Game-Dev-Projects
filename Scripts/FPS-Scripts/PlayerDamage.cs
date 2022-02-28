using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    public float health, timer;
    public Text healthIndicator;
    public GameObject menu;
    // Start is called before the first frame update
    void Start()
    {
        timer = 2;
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        healthIndicator.text = "HEALTH: " + health;
        if (health <= 0)
        {
            Debug.Log("knocked");
            if (GetComponent<PlayerAbility>().HasShield())
            {
                GetComponent<PlayerAbility>().BreakShield();
                health = 50;
            }
            else
                GameOver();
        }
    }

    public void GameOver()
    {
        menu.GetComponent<MenuController>().gameOver = true;
        menu.GetComponent<MenuController>().PauseGame();
    }

    public void OnCollisionEnter(Collision collision)
    {
        EnemyDamage ed = collision.gameObject.GetComponent<EnemyDamage>();
        if (ed)
            health -= 20;
    }
}
