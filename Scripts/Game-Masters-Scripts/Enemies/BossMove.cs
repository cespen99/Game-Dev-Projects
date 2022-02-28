using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    public GameObject player;
    public float baseSpeed;
    public float speed;
    public Vector3 playerDist, moveVector;

    void Start()
    {
        speed = baseSpeed;
    }
    void FixedUpdate()
    {
        if(Mathf.Abs(transform.position.z) > 1)
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        Vector2 oldPos = transform.position;

        if (speed != 0 && !GetComponent<BossBehavior>().deadAsHell)
        {
            transform.LookAt(player.transform);
            playerDist = (transform.position - player.transform.position).normalized;
            moveVector = new Vector3(playerDist.x * speed * Time.deltaTime, -playerDist.y * speed * Time.deltaTime, 0);
            //transform.Translate(moveVector);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            GetComponent<Animator>().SetBool("moving", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("moving", false);
        }

        Vector2 newPos = transform.position;
        if (oldPos.x - newPos.x > 0)
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        if (oldPos.x - newPos.x < 0)
            gameObject.GetComponent<SpriteRenderer>().flipX = true;

        transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
    }

    public void Follow()
    {
        speed = baseSpeed;
    }
    public void Retreat()
    {
        speed = -baseSpeed;
    }

    public void Charge()
    {
        speed = baseSpeed * 1.5f;
    }
    public void Still()
    {
        speed = 0;
    }
}
