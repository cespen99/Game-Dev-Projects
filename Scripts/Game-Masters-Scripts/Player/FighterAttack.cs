using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FighterAttack : MonoBehaviour
{
    public GameObject hitbox, bubble;
    public bool shield;
    public float timer, speed, damage, blockSpeedMod;
    private int combo, scombo;
    public bool canAttack;
    // Start is called before the first frame update
    public AudioSource swordSound1, swordSound2;
    void Start()
    {
        damage = 10;
        speed = 0.5f;
        timer = 1;
        combo = 0;
        scombo = 0;
        canAttack = true;
        shield = false;
        blockSpeedMod = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
        else
            combo = 0;

        if (shield)
        {
            bubble.SetActive(true);
            canAttack = false;
            if (GetComponent<WizardAttack>().mana < 5)
            {
                shield = false;
                GetComponent<Movement>().speed = GetComponent<Movement>().moveSpeed;
                GetComponent<Damage>().invincible = false;
                canAttack = true;
            }
            else
            {
                GetComponent<WizardAttack>().mana -= 20 * Time.deltaTime;
            }
        }
        else
            bubble.SetActive(false);
    }
    public void Attack()
    {
        if (canAttack)
        {
            canAttack = false;
            SwordAttack();
        }
    }

    public void Special()
    {
        if (canAttack)
        {
            canAttack = false;
            SwordSpecial();
        }
    }

    public void SwordAttack()
    {
        switch (combo)
        {
            case 0://warrior, sword, slash
                swordSound1.Play();
                StartCoroutine("Pause", .5f);
                StartCoroutine("Slash");
                timer = 1f;
                combo = 1;
                break;
            case 1://warrior, sword, backslash
                swordSound1.Play();
                StartCoroutine("Pause", .5f);
                StartCoroutine("BackSlash");
                timer = 1f;
                combo = 2;
                break;
            case 2://warrior, sword, sweep
                swordSound2.Play();
                StartCoroutine("Pause", 1f);
                StartCoroutine("Sweep");
                timer = 1f;
                combo = 0;
                break;
        }
    }

    public void SwordSpecial()
    {
        switch (scombo)
        {
            case 0://warrior, sword, slash
                swordSound1.Play();
                StartCoroutine("Pause", .5f);
                StartCoroutine("Fury");
                scombo = 1;
                break;
            case 1://warrior, sword, backslash
                swordSound1.Play();
                StartCoroutine("Pause", .5f);
                StartCoroutine("Cleave");
                scombo = 2;
                break;
            case 2://warrior, sword, sweep
                swordSound2.Play();
                StartCoroutine("Pause", 1f);
                StartCoroutine("Spin");
                scombo = 0;
                break;
        }
    }

    public void OnClassAction(InputValue value)
    {
        Debug.Log("BLOCK");
        shield = !shield;
        bubble.SetActive(shield);
        GetComponent<Movement>().animator.SetBool("IdleBlock", shield);

        if (shield)
        {
            canAttack = false;
            GetComponent<Movement>().speed = GetComponent<Movement>().moveSpeed / 2;
            GetComponent<Damage>().invincible = true;
        }
        else
        {
            canAttack = true;
            GetComponent<Movement>().speed = GetComponent<Movement>().moveSpeed;
            GetComponent<Damage>().invincible = false;
        }

    }

    public void Box(float size, float distance, float degree)
    {
        Movement mv = GetComponent<Movement>();
        GameObject box = Instantiate(hitbox, transform.position + new Vector3(-Mathf.Sin(Mathf.Deg2Rad * (mv.targetAngle + degree)) * distance, Mathf.Cos(Mathf.Deg2Rad * (mv.targetAngle + degree)) * distance, 0f), Quaternion.AngleAxis(mv.targetAngle, Vector3.zero));
        box.GetComponent<AttackBox>().SetDamage(damage);
        box.transform.localScale = new Vector3(size, size, 0);
        box.GetComponent<BoxCollider2D>().enabled = true;
        box.GetComponent<AttackBox>().targetEnemy = 1;
        box.GetComponent<AttackBox>().damage = damage;
        box.GetComponent<AttackBox>().enabled = true;
        Destroy(box, .2f);
    }

    public IEnumerator Pause(float time)
    {
        canAttack = false;
        yield return new WaitForSeconds(time);
        canAttack = true;
    }

    public IEnumerator Slash()
    {
        GetComponent<Movement>().animator.SetTrigger("Attack1");
        Box(1f, 1f, -90);
        yield return new WaitForSeconds(.03f);
        Box(1.5f, 1.25f, -45);
        yield return new WaitForSeconds(.03f);
        Box(2f, 1.5f, 0);
    }
    public IEnumerator BackSlash()
    {
        GetComponent<Movement>().animator.SetTrigger("Attack2");
        Box(1f, 1f, 90);
        yield return new WaitForSeconds(.03f);
        Box(1.5f, 1.25f, 45);
        yield return new WaitForSeconds(.03f);
        Box(2f, 1.5f, 0);
    }
    public IEnumerator Sweep()
    {
        GetComponent<Movement>().animator.SetTrigger("Attack3");
        Box(1f, 1.5f, -70);
        yield return new WaitForSeconds(.03f);
        Box(1.5f, 1.5f, -45);
        yield return new WaitForSeconds(.03f);
        Box(2f, 1.75f, 0);
        yield return new WaitForSeconds(.03f);
        Box(1.5f, 1.5f, 45);
        yield return new WaitForSeconds(.03f);
        Box(1f, 1.5f, 70);
    }

    public IEnumerator Swipe()
    {
        GetComponent<Movement>().animator.SetTrigger("Attack1");
        Box(1f, 2f, 30);
        yield return new WaitForSeconds(.03f);
        Box(2f, 2.5f, 0);
        yield return new WaitForSeconds(.03f);
        Box(2f, 3f, -30);
        yield return new WaitForSeconds(.03f);
        Box(1.5f, 2.5f, -60);
        yield return new WaitForSeconds(.03f);
        Box(1f, 2f, -90);
    }
    public IEnumerator BackSwipe()
    {
        GetComponent<Movement>().animator.SetTrigger("Attack1");
        Box(1f, 2f, -30);
        yield return new WaitForSeconds(.03f);
        Box(2f, 2.5f, 0);
        yield return new WaitForSeconds(.03f);
        Box(2f, 3f, 30);
        yield return new WaitForSeconds(.03f);
        Box(1.5f, 2.5f, 60);
        yield return new WaitForSeconds(.03f);
        Box(1f, 2f, 90);
    }
    public IEnumerator Stab()
    {
        GetComponent<Movement>().animator.SetTrigger("Attack1");
        Box(1f, 1.5f, 0);
        yield return new WaitForSeconds(.03f);
        Box(1f, 2.5f, 0);
        yield return new WaitForSeconds(.03f);
        Box(1f, 4f, 0);
        yield return new WaitForSeconds(.03f);
        Box(1f, 5.5f, 0);
    }

    public IEnumerator Fury()
    {
        GetComponent<Movement>().animator.SetTrigger("Attack1");
        Box(1f, 1f, -90);
        yield return new WaitForSeconds(.02f);
        Box(1.5f, 1.25f, -45);
        yield return new WaitForSeconds(.02f);
        Box(2f, 1.5f, 0);

        yield return new WaitForSeconds(.2f);
        GetComponent<Movement>().animator.SetTrigger("Attack1");
        Box(1f, 1f, 90);
        yield return new WaitForSeconds(.02f);
        Box(1.5f, 1.25f, 45);
        yield return new WaitForSeconds(.02f);
        Box(2f, 1.5f, 0);

        yield return new WaitForSeconds(.2f);
        GetComponent<Movement>().animator.SetTrigger("Attack1");
        Box(1f, 1.5f, -60);
        yield return new WaitForSeconds(.02f);
        Box(1.5f, 1.75f, -30);
        yield return new WaitForSeconds(.02f);
        Box(2f, 2f, 0);

        yield return new WaitForSeconds(.2f);
        GetComponent<Movement>().animator.SetTrigger("Attack1");
        Box(1f, 1.5f, 60);
        yield return new WaitForSeconds(.02f);
        Box(1.5f, 1.75f, 30);
        yield return new WaitForSeconds(.02f);
        Box(2f, 2f, 0);
    }
    public IEnumerator Cleave()
    {
        GetComponent<Movement>().animator.SetTrigger("Attack2");
        Box(1f, 1f, 0);
        yield return new WaitForSeconds(.05f);
        Box(1.5f, 2f, 0);
        yield return new WaitForSeconds(.25f);
        Box(2.5f, 3f, 0);
    }
    public IEnumerator Spin()
    {
        GetComponent<Movement>().animator.SetTrigger("Attack3");
        Box(1f, 1f, -45);
        yield return new WaitForSeconds(.06f);
        Box(1.5f, 1.25f, -90);
        yield return new WaitForSeconds(.055f);
        Box(1.5f, 1.5f, -135);
        yield return new WaitForSeconds(.05f);
        Box(2f, 1.5f, -180);
        yield return new WaitForSeconds(.03f);
        Box(2f, 1.5f, 135);
        yield return new WaitForSeconds(.03f);
        Box(2f, 1.75f, 90);
        yield return new WaitForSeconds(.03f);
        Box(2.5f, 1.75f, 45);
        yield return new WaitForSeconds(.03f);
        Box(3f, 2f, 0);
    }

}
