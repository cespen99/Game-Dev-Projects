using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShoot : MonoBehaviour
{
    [SerializeField] private LineRenderer laser;
    [SerializeField] private GameObject bullet, missile;
    [SerializeField] private Transform fireLocation;
    [SerializeField] private Transform towerHead;
    private TowerInfo tower;
    public GameObject target;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        tower = GetComponent<TowerInfo>();
        timer = tower.firerate;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        LockOn();
        if (target == null)
        {
            laser.enabled = false;
            return;
        }
        Shoot();
    }
    private void LockOn()
    {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        float minDist = Mathf.Infinity;
        int selectEnemy = -1;
        for (int i = 0; i < allEnemies.Length; i++)
        {
            float dist = Vector3.Distance(transform.position, allEnemies[i].transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                selectEnemy = i;

            }
            if (selectEnemy != -1 && minDist <= tower.range)
            {
                target = allEnemies[selectEnemy];
                if(towerHead != null)
                    towerHead.transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position, Vector3.back);
            }
            else
                target = null;
        }
    }
    private void Shoot()
    {
        if (timer <= 0)
        {
            switch (tower.towerType)
            {
                case TOWER_TYPE.LASER:
                    ShootLaser();
                    break;
                case TOWER_TYPE.GUN:
                    laser.enabled = false;
                    ShootGun();
                    break;
                case TOWER_TYPE.MISSILE:
                    laser.enabled = false;
                    ShootMissile();
                    break;
            }
            timer = tower.firerate;
        }
    }

   

    private void ShootLaser()
    {
        laser.enabled = true;
        laser.SetPosition(0, fireLocation.position);
        laser.SetPosition(1, target.transform.position);
        target.GetComponent<EnemyHealth>().TakeDamage(tower.damage);
        GameObject.Find("LaserSound").GetComponent<AudioSource>().Play();
    }

    private void ShootGun()
    {
        GameObject shot = Instantiate(bullet, fireLocation.position, Quaternion.identity);
        shot.GetComponent<Bullet>().SetDamage(tower.damage);
        shot.GetComponent<Rigidbody>().AddForce((target.transform.position - shot.transform.position) * 200);
    }

    private void ShootMissile()
    {
        GameObject blast = Instantiate(missile, fireLocation.position, Quaternion.identity);
        blast.GetComponent<Missile>().SetDamage(tower.damage);
        blast.GetComponent<Rigidbody>().AddForce((target.transform.position - blast.transform.position) * 100);
    }
}
