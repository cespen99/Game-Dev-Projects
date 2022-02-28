using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBlast : MonoBehaviour
{
    [SerializeField] private float damage;
    void Start()
    {
        Destroy(gameObject, 0.5f);
    }

    void Update()
    {
        transform.localScale += new Vector3(5f, 5f, 5f) * Time.deltaTime;
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
        }
    }
}
