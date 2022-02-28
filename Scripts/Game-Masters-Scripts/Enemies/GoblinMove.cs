using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class GoblinMove : MonoBehaviour
{
    public GameObject player;
    public float baseSpeed;
    public float speed;
    public Vector3 target;
    public float targetDist;
    public float targetAngle;
    public float followDist;
    public float attackDist;
    public float tempDist;
    public float followDir;

    void Start()
    {
        speed = baseSpeed;
        tempDist = followDist;
        player = GetComponent<EnemyAwareness>().player;
    }
    void Update()
    {
        target = player.transform.position - transform.position;
        targetDist = Mathf.Sqrt(Mathf.Pow(target.x, 2) + Mathf.Pow(target.y, 2));
        targetAngle = Mathf.Atan2(-target.x, target.y) * Mathf.Rad2Deg;

        followDir = (targetDist - tempDist) / Mathf.Abs(targetDist - tempDist);
        if (Mathf.Abs(targetDist - tempDist) < .2f)
            followDir = 0;

        Vector3 oldPos = transform.position;

        if (speed != 0)
        {
            transform.rotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);
            transform.Translate(speed * Vector3.up * Time.deltaTime);
            GetComponent<Animator>().SetBool("moving", true);
        }
        else
        {
            if (gameObject.GetComponent<Pace>().isPacing == false)
            GetComponent<Animator>().SetBool("moving", false);
        }

        Vector3 newPos = transform.position;
        if (oldPos.x - newPos.x > 0)
        gameObject.GetComponent<SpriteRenderer>().flipX = true;
        if (oldPos.x - newPos.x < 0)
            gameObject.GetComponent<SpriteRenderer>().flipX = false;

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
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.one;
        speed = baseSpeed * 1.5f;
    }
    public void Still()
    {
        speed = 0;
    }
}
