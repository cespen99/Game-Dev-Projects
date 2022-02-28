using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] GameObject blast;
    public GameObject speaker1, speaker2;
    void Start()
    {
        GameObject.Find("ShootSound").GetComponent<AudioSource>().Play();
        Destroy(gameObject, 2f);
    }
    public void SetDamage(float x)
    {
        damage = x;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Board")
        {
            GameObject.Find("BombSound").GetComponent<AudioSource>().Play();
            GameObject boom = Instantiate(blast, transform.position, Quaternion.identity);
            boom.GetComponent<MissileBlast>().SetDamage(damage);
            Destroy(gameObject);
        }
    }
}
