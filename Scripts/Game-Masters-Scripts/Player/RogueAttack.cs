using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueAttack : MonoBehaviour
{
    public GameObject hitbox;
    public float damage, accuracy;
    public int meleeNum, rangeNum;
    Dictionary<int, int> weaponDamage, weaponAccuracy;
    public bool canAttack;
    void Start()
    {
        meleeNum = 0;
        weaponDamage = new Dictionary<int, int>();
        weaponAccuracy = new Dictionary<int, int>();
        weaponDamage.Add(1, 3);
        weaponAccuracy.Add(1, 60);
        weaponDamage.Add(2, 1);
        weaponAccuracy.Add(2, 100);
        //sword
        weaponDamage.Add(3, 1);
        weaponAccuracy.Add(3, 75);
        //spear
        weaponDamage.Add(4, 3);
        weaponAccuracy.Add(4, 60);
        canAttack = true;
    }

    // Update is called once per frame

    public void Attack()
    {
        if (canAttack)
        {
            canAttack = false;
            switch (meleeNum)
            {
                case 1://combat1
                    StartCoroutine("Stab");
                    StartCoroutine("Pause", .1f);
                    break;
                case 2://support1
                    StartCoroutine("Shoot");
                    StartCoroutine("Pause", 1);
                    break;
            }
        }
    }

    public void Box(float size, float distance, float degree)
    {
        Movement mv = GetComponent<Movement>();
        GameObject box = Instantiate(hitbox, transform.position + new Vector3(-Mathf.Sin(Mathf.Deg2Rad * (mv.targetAngle + degree)) * distance, Mathf.Cos(Mathf.Deg2Rad * (mv.targetAngle + degree)) * distance, 0f), Quaternion.Euler(0, 0, mv.targetAngle));
        box.GetComponent<AttackBox>().SetDamage(damage);
        box.GetComponent<AttackBox>().SetChance(accuracy);
        box.transform.localScale = new Vector3(size, size, 0);
        box.GetComponent<BoxCollider2D>().enabled = true;
        box.GetComponent<SpriteRenderer>().enabled = true;
        box.GetComponent<AttackBox>().targetEnemy = 1;
        box.GetComponent<AttackBox>().damage = damage;
        box.GetComponent<AttackBox>().hitChance = accuracy;
        box.GetComponent<AttackBox>().enabled = true;
        Destroy(box, .2f);
    }

    public void Box(float size, float distance, float degree, float speed, float timer)
    {
        Movement mv = GetComponent<Movement>();
        GameObject box = Instantiate(hitbox, transform.position + new Vector3(-Mathf.Sin(Mathf.Deg2Rad * (mv.targetAngle + degree)) * distance, Mathf.Cos(Mathf.Deg2Rad * (mv.targetAngle + degree)) * distance, 0f), Quaternion.Euler(0, 0, mv.targetAngle));        //box.GetComponent<AttackBox>().SetColor(col);
        box.GetComponent<AttackBox>().SetDamage(damage);
        box.GetComponent<AttackBox>().SetChance(accuracy);
        box.transform.localScale = new Vector3(size, size, 0);
        box.GetComponent<BoxCollider2D>().enabled = true;
        box.GetComponent<SpriteRenderer>().enabled = true;
        box.GetComponent<AttackBox>().targetEnemy = 1;
        box.GetComponent<AttackBox>().enabled = true;
        box.GetComponent<AttackBox>().SetProjectile(speed);
        Destroy(box, timer);
    }

    public IEnumerator Pause(float time)
    {
        yield return new WaitForSeconds(time);
        canAttack = true;
    }

    public IEnumerator Stab()
    {
        yield return new WaitForSeconds(.03f);
        Box(1.5f, 1.25f, 0);
    }

    public IEnumerator Shoot()
    {
        yield return new WaitForSeconds(.5f);
        Box(.5f, 1f, 0, 10, 2);
    }
}
