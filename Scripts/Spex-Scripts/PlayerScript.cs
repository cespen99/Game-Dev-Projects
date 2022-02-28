using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerScript : MonoBehaviour {

    //public GameObject laser, bullet, greenHealth, redHealth;
    public Vector2 playerSize, playerPosition;
    public float bCoolDown, lCoolDown, rotateSpeed, currentAngle, topSpeed;
    private float bTimer, lTimer;
    private Rigidbody2D rb;
    public float speed, drag0;
    public int health, defense;
    public SpriteRenderer mBoost, pBoost, sbBoost;
    public GameObject healthBar, hpbar;

    void Start()
    {
        health = 100;
        defense = 1;
        playerSize = GetComponent<Renderer>().bounds.size;
        bTimer = 0f;
        lTimer = 0f;
        rb = GetComponent<Rigidbody2D>();
        drag0 = rb.drag;
        healthBar = Instantiate(hpbar, playerPosition + new Vector2(0, 1), Quaternion.identity);
        healthBar.GetComponent<HealthBar>().player = this.gameObject;
        healthBar.GetComponent<HealthBar>().enabled = true;
    }

    void Boost()
    {
        mBoost.enabled = true;
        rb.AddForce(new Vector2(Mathf.Cos(Mathf.Deg2Rad * currentAngle), Mathf.Sin(Mathf.Deg2Rad * currentAngle)));
        if (rb.velocity.magnitude > topSpeed)
            rb.velocity = rb.velocity.normalized * topSpeed;
    }
    void Brake()
    {
        rb.drag += .05f;
        if (rb.drag > 10)
            rb.drag = 10;
    }

    void Update()
    {
        mBoost.enabled = false;
        pBoost.enabled = false;
        sbBoost.enabled = false;
        //greenHealth.transform.localScale = new Vector2(health * .06f, 6);     HEALTH UPDATE
        //redHealth.transform.localScale = new Vector2((100-health) * .06f, 6);
        playerPosition = transform.position;
        currentAngle = transform.rotation.eulerAngles.z;

        if (Input.GetKey(KeyCode.W))
        {
            Boost();
        }
        if (Input.GetKey(KeyCode.S))
        {
            Brake();
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            rb.drag = drag0;
        }
        if (Input.GetKey(KeyCode.A))
        {
            sbBoost.enabled = true;
            transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            pBoost.enabled = true;
            transform.Rotate(Vector3.back * rotateSpeed * Time.deltaTime);
        }

        if (Input.GetKey("left shift"))
        {
            if (lTimer <= 0)
            {
                lTimer = lCoolDown;
                //Instantiate(laser, new Vector2(playerPosition.x + (Mathf.Cos(Mathf.Deg2Rad * (currentAngle - 5))), playerPosition.y + (Mathf.Sin(Mathf.Deg2Rad * (currentAngle - 5)))), Quaternion.identity);
            }
            lTimer -= Time.deltaTime;
        }


    }

    void FixedUpdate()
    {
        

    }

    private void LateUpdate()
    {

        if (Input.GetKey("space"))
        {
            if (bTimer <= 0)
            {
                bTimer = bCoolDown;
                //Instantiate(bullet, new Vector2(playerPosition.x + (Mathf.Cos(Mathf.Deg2Rad * (currentAngle - 45)) * .65f), playerPosition.y + (Mathf.Sin(Mathf.Deg2Rad * (currentAngle - 45)) * .65f)), Quaternion.identity);
                //Instantiate(bullet, new Vector2(playerPosition.x + (Mathf.Cos(Mathf.Deg2Rad * (currentAngle + 45)) * .65f), playerPosition.y + (Mathf.Sin(Mathf.Deg2Rad * (currentAngle + 45)) * .65f)), Quaternion.identity);
            }
            bTimer -= Time.deltaTime;
        }
    }
}
