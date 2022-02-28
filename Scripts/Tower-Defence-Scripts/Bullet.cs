using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float damage;
    public GameObject speaker;
    void Start()
    {
        GameObject.Find("ShootSound").GetComponent<AudioSource>().Play();
        Destroy(gameObject, 1f);
    }

    public void SetDamage(float x)
    {
        damage = x;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
