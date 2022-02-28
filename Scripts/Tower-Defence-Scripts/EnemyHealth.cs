using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 1;
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameInfo>().AddMoney(10);
            Destroy(gameObject);
        }
    }
}
