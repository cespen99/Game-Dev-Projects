using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinBehaviour : MonoBehaviour
{
    public int state;  //0=idle 1=follow 2=charge 3=attack 4=retreat
    public GameObject target;
    public float targetDist;
    public float damage;
    public float atkDistance;
    public Vector3 targetDistXY;
    public bool canAttack;
    public GameObject hitbox;
    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        GetComponent<GoblinMove>().Still();
        target = GetComponent<EnemyAwareness>().player;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<EnemyAwareness>().dist2Player < GetComponent<GoblinMove>().attackDist && canAttack)
        {
            state = 3;
            canAttack = false;
            StartCoroutine("Attack");
        }
        if (GetComponent<EnemyAwareness>().dist2Player > GetComponent<EnemyAwareness>().sightRadius * 4)
            state = 0;

        if (state == 0)
        {
            GetComponent<GoblinMove>().Still();
        }
        if (state==1)
        {
            GetComponent<GoblinMove>().Follow();
        }
        if (state == 2)
        {
            GetComponent<GoblinMove>().Charge();
            GetComponent<GoblinMove>().tempDist = GetComponent<GoblinMove>().attackDist;
            if (GetComponent<EnemyAwareness>().dist2Player < GetComponent<GoblinMove>().attackDist)
                state = 1;
            
        }
        if (state == 4)
        {

            GetComponent<GoblinMove>().tempDist = GetComponent<GoblinMove>().followDist;
            if (GetComponent<GoblinMove>().targetDist < GetComponent<GoblinMove>().followDist)
                GetComponent<GoblinMove>().Retreat();
            else
                state = 1;
        }
    }
    public IEnumerator Agro()
    {
        GetComponent<GoblinMove>().player = target;
        GetComponent<GoblinMove>().enabled = true;
        GetComponent<GoblinMove>().tempDist = GetComponent<GoblinMove>().followDist;
        state = 1;
        yield return new WaitForSeconds(2f);
        state = 2;
        canAttack = true;
    }
    public IEnumerator Attack()
    {
     
        gameObject.GetComponent<Animator>().SetBool("atk1", true);
        
        GetComponent<GoblinMove>().Still();
        yield return new WaitForSeconds(.55f);
        gameObject.GetComponent<AudioSource>().Play();
        Vector3 tarAng = target.transform.position - transform.position;
       // float targetAngle = Mathf.Atan2(-tarAng.x, tarAng.y) * Mathf.Rad2Deg;
       // print(targetAngle);
        //gameObject.GetComponent<Animator>().SetBool("attacking1", false);
        Box(.5f, atkDistance, tarAng.normalized, damage, 100);
        yield return new WaitForSeconds(.5f);
        state = 4;
        yield return new WaitForSeconds(2f);
        StartCoroutine("Agro");
    }

    public void Box(float size, float distance, Vector3 degree, float damage, float accuracy)
    {
        print(degree.x);
        print(transform.position);
        print(Mathf.Cos(degree.x));
        print(transform.position.x + 3f * Mathf.Cos(degree.x));
        print( 3f * Mathf.Cos(degree.x));
        // GameObject box = Instantiate(hitbox, transform.position + new Vector3(-Mathf.Sin(Mathf.Deg2Rad * (transform.eulerAngles.z + degree)) * distance, Mathf.Cos(Mathf.Deg2Rad * (transform.eulerAngles.z + degree)) * distance, 0f), transform.rotation);
        Vector3 enemyPos = new Vector3(transform.position.x + .6f *  degree.x, transform.position.y + 1f * Mathf.Sin(degree.y), transform.position.z);
        GameObject box = Instantiate(hitbox, enemyPos + degree, transform.rotation);
        box.GetComponent<AttackBox>().SetDamage(damage);

        box.GetComponent<AttackBox>().SetChance(accuracy);
        box.transform.localScale = new Vector3(size, size, 0);
        box.GetComponent<BoxCollider2D>().enabled = true;
        //box.GetComponent<SpriteRenderer>().enabled = true;
        box.GetComponent<AttackBox>().targetEnemy = 0;
        box.GetComponent<AttackBox>().enabled = true;
        Destroy(box, .2f);
    }
}
