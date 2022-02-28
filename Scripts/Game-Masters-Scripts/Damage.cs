using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int isEnemy; //0=player, 1=enemy
    public float health;
    public Color col;
    public bool invincible;

    void Start()
    {
        col = GetComponentInChildren<SpriteRenderer>().color;
        invincible = false;
    }

    public void Dam(float damage)
    {
        //GetComponent<Animator>().SetTrigger("hit");
        if (!invincible)
        {
            health -= damage;
            StartCoroutine("hitEffect");
        }
    }

    private IEnumerator hitEffect()
    {
        invincible = true;
        GetComponentInChildren<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(.2f);
        GetComponentInChildren<SpriteRenderer>().color = col;
        invincible = false;
    }

}
