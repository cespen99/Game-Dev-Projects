using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBehavior : MonoBehaviour
{
    public enum State { IDLE, MOVING, ATTACKING, CASTING };
    public State BossState;
    public GameObject player;
    public float targetDist, attackDist;
    public Vector3 targetDistXY;
    public bool canAttack;
    public GameObject spell, hitbox;
    public BossMove move;
    public float spellTimer, spellDelay;
    public Vector3 targetAngle;
    public GameObject gameOverMenu;
    public Text gameOverText;
    public bool deadAsHell;

    void Start()
    {
        move = GetComponent<BossMove>();
        BossState = State.IDLE;
        move.Follow();
        spellTimer = spellDelay;
        canAttack = true;
        deadAsHell = false;
    }

    void Update()
    {
        targetDist = Vector2.Distance(player.transform.position,  transform.position);
        
        spellTimer -= Time.deltaTime;
        if (canAttack && !deadAsHell)
        {
            if (spellTimer < 0)
                StartCoroutine("Cast");
            else if (targetDist < attackDist)
                StartCoroutine("Attack");
        }

        if (GetComponent<Damage>().health <= 0 && !deadAsHell)
        {
            deadAsHell = true;
            StartCoroutine("WinGame");
        }

        
    }

    public IEnumerator WinGame()
    {
        GetComponent<Animator>().SetTrigger("death");
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(0.9f);
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        gameOverMenu.SetActive(true);
        gameOverText.color = Color.green;
        gameOverText.text = "YOU WIN!!!";
        gameOverText.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

        public IEnumerator Attack()
        {
        canAttack = false;
        GetComponent<Damage>().invincible = true;
        gameObject.GetComponent<Animator>().SetTrigger("attack");
        move.Still();
        BossState = State.ATTACKING;
        yield return new WaitForSeconds(0.5f);

        /*Box(1f, 2f, 30, 20);
        yield return new WaitForSeconds(.03f);
        Box(2f, 2.5f, 0, 20);
        yield return new WaitForSeconds(.03f);
        Box(2f, 3f, -30, 20);
        yield return new WaitForSeconds(.03f);
        Box(1.5f, 2.5f, -60, 20);
        yield return new WaitForSeconds(.03f);
        Box(1f, 2f, -90, 20);*/
        Box(3f, 2, -90, 20);

        BossState = State.IDLE;
        yield return new WaitForSeconds(.2f);
        GetComponent<Damage>().invincible = false;
        yield return new WaitForSeconds(.5f);
        move.Follow();
        yield return new WaitForSeconds(.5f);
        canAttack = true;
    }

    public IEnumerator Cast()
    {
        canAttack = false;
        gameObject.GetComponent<Animator>().SetBool("cast", true);
        BossState = State.CASTING;
        move.Still();
        Vector3 target = player.transform.position;
        yield return new WaitForSeconds(0.5f);

        GameObject vortex = Instantiate(spell, target + new Vector3(0, 4, 0), Quaternion.identity);
        vortex.SetActive(true);
        vortex.transform.localScale = new Vector3(10, 10, 0);
        yield return new WaitForSeconds(0.5f);

        BossState = State.IDLE;
        yield return new WaitForSeconds(.5f);

        move.Follow();
        spellTimer = spellDelay;
        yield return new WaitForSeconds(.5f);
        canAttack = true;
    }

    public void Box(float size, float distance, float degree, float damage)
    {
        targetAngle = (player.transform.position-transform.position).normalized * distance;
        GameObject box = Instantiate(hitbox, transform.position + targetAngle, transform.rotation);
        box.GetComponent<AttackBox>().SetDamage(damage);
        box.transform.localScale = new Vector3(size, size, 0);
        box.GetComponent<SpriteRenderer>().enabled = true;
        box.GetComponent<BoxCollider2D>().enabled = true;
        box.GetComponent<AttackBox>().targetEnemy = 0;
        box.GetComponent<AttackBox>().enabled = true;
        Destroy(box, .2f);
    }
}
