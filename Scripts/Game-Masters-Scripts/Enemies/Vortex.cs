using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vortex : MonoBehaviour
{
    public float interval, duration, offset;
    public GameObject hitbox;
    void Start()
    {
        StartCoroutine("Strike");
        Destroy(gameObject, duration);
    }

    public IEnumerator Strike()
    {
        yield return new WaitForSeconds(offset);
        GameObject box = Instantiate(hitbox, new Vector3(0, -4, 0), Quaternion.identity);
        box.GetComponent<AttackBox>().SetDamage(10);
        box.transform.localScale = new Vector3(2, 2, 0);
        box.GetComponent<SpriteRenderer>().enabled = true;
        box.GetComponent<BoxCollider2D>().enabled = true;
        box.GetComponent<AttackBox>().targetEnemy = 0;
        box.GetComponent<AttackBox>().enabled = true;
        Destroy(box, .5f);
        yield return new WaitForSeconds(interval-offset);
        StartCoroutine("Strike");
    }
}
