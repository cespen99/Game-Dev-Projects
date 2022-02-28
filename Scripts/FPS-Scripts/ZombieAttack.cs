using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAttack : MonoBehaviour
{
    public Animator anim;
    public AudioSource zombieVoice;
    [SerializeField] private float dist2Atk, dist2Dmg;
    [SerializeField] private Transform target;
    private bool attacking;
    public float spd;
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        attacking = false;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        spd = GetComponent<NavMeshAgent>().speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(target.position, transform.position) < dist2Atk && !attacking)
        {
            StartCoroutine("Attack");
        }
        if (Vector3.Distance(target.position, transform.position) < dist2Dmg && attacking && target.GetComponent<PlayerDamage>().timer < 0)
        {
            target.GetComponent<PlayerDamage>().health -= damage;
            target.GetComponent<PlayerDamage>().timer = 2;
        }
    }

    IEnumerator Attack()
    {
        anim.SetTrigger("Attack");
        zombieVoice.Play();
        attacking = true;
        GetComponent<NavMeshAgent>().speed = 0;
        yield return new WaitForSeconds(0.25f);//prep
        GetComponent<NavMeshAgent>().speed = spd*10;
        yield return new WaitForSeconds(0.75f);//charge
        GetComponent<NavMeshAgent>().speed = 0;
        yield return new WaitForSeconds(0.75f);//recharge
        GetComponent<NavMeshAgent>().speed = spd;
        yield return new WaitForSeconds(2f);//reset
        attacking = false;
    }
}
