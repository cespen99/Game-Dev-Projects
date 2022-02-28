using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBox : MonoBehaviour
{
    //public Sprite circle;
    //public Color color;
    public int targetEnemy; //0=enemy attacking 1=player attacking
    public float size, timer = 0;
    public float hitChance, damage, fire, ice, lightning, poison, bright, dark, rng;
    public float projSpd = 0, growSpd = 0;
    Vector3 baseSize;
    public bool projEnable, growEnable;
    // Start is called before the first frame update
    void Start()
    {
        baseSize = transform.localScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Shield")
        {
            Destroy(this);
        }
        if (collision.gameObject.GetComponent<Damage>() && collision.gameObject.GetComponent<Damage>().isEnemy == targetEnemy)
        {
            collision.gameObject.GetComponent<Damage>().Dam(damage);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement mv = GetComponent<Movement>();

        if (projEnable)
            transform.Translate(projSpd *  Vector3.up * .009f);
        if (growSpd > 0)
            transform.localScale = transform.localScale + (baseSize * growSpd * .005f);
    }
    /*
    void OnCollisionEnter2D(Collision2D collision)
    {
        
        Debug.Log("HIT");
        if(collision.gameObject.GetComponent<BaseStats>(aim) == 0)
        { 
            
        }
        else
            Destroy(this.gameObject);
        
    }
    */
    public void SetProjectile(float ps)
    {
        projSpd = ps;
        projEnable = true;
    }

    public void SetGrow(float gs)
    {
        growSpd = gs;
        growEnable = true;
    }
    public void SetTimer(float ts)
    {
        timer = ts;
    }
    public void SetColor(Color col)
    {
        GetComponent<SpriteRenderer>().color = col;
    }
    public void SetSize(float size)
    {

    }
    public void SetDamage(float dmg)
    {
        damage = dmg;
    }
    public void SetChance(float hc)
    {
        hitChance = hc;
    }


}
