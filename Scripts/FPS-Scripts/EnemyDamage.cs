using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float health;
    public GameObject powerUp;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;   
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            int rand = Random.Range(0, 3);
            SpawnPowerUp();
            Destroy(this.gameObject);
        }
    }

    public void Hit(float dmg)
    {
        health -= dmg;
        StartCoroutine("DamageEffect");
    }

    IEnumerator DamageEffect()
    {
        Color temp = GetComponent<MeshRenderer>().material.color;
        GetComponent<MeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        GetComponent<MeshRenderer>().material.color = temp;
    }

    void SpawnPowerUp()
    {
        Instantiate(powerUp, transform.position, Quaternion.identity);
    }
}
