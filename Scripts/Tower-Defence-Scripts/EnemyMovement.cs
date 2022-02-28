using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float damage = 1f;
    public GameObject speaker;
    private GameObject prev, next;
    private float timer;

    void Start()
    {
        prev = PathGenerator.path[0];
        transform.position = prev.transform.position;
        timer = 0;
    }

    void Update()
    {
        if (Convert.ToInt32(prev.name) < PathGenerator.path.Count - 1)
        {
            next = PathGenerator.path[Convert.ToInt32(prev.name) + 1];
            transform.position += (next.transform.position - transform.position).normalized * Time.deltaTime * speed;
            Quaternion r = Quaternion.LookRotation((next.transform.position - transform.position).normalized);
            transform.rotation = Quaternion.Euler(r.x * 360, 90f, -90f);
            if (Vector3.Distance(transform.position, next.transform.position) <= 0.01f)
                prev = next;
        }
        else
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameInfo>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = 5f;
            GameObject.Find("SpiderSound").GetComponent<AudioSource>().Play();
        }

    }
}
