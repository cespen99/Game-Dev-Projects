using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject spawnPoint;
    public GameObject[] SP;
    public GameObject zombie;
    public Material Red, Black;
    public int num;
    // Start is called before the first frame update
    void Start()
    {
        num = 60;
        SP = new GameObject[num];
        for (int i = 0; i < num; i++)
        {
            //value = offset + ()
            float xValue = 49.2f + (100 * Mathf.Cos(Mathf.Deg2Rad * (i * (360 / num))));
            float zValue = 79.1f + (100 * Mathf.Sin(Mathf.Deg2Rad * (i * (360 / num))));
            SP[i] = Instantiate(spawnPoint, new Vector3(xValue, 40, zValue), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Spawn(int i)
    {
        int rand = Random.Range(0, num - 1);
        while (SP[rand].transform.position.y < 0)
        {
            rand = Random.Range(0, num - 1);
        }
        if (i < 0 || i >= 3)
        {
            Debug.Log("invalid spawn");
            return;
        }
        if (i == 1)
        {
            GameObject zom = Instantiate(zombie, SP[rand].transform.position, Quaternion.identity);
            switch (rand % 3)
            {
                case 0:
                    zom.GetComponent<ZombieAttack>().damage = 20;
                    zom.GetComponentInChildren<EnemyDamage>().health = 100;
                    break;
                case 1:
                    zom.GetComponent<ZombieAttack>().damage = 30;
                    zom.GetComponent<EnemyDamage>().health = 200;
                    zom.GetComponentInChildren<MeshRenderer>().material = Red;
                    break;
                case 2:
                    zom.GetComponent<ZombieAttack>().damage = 40;
                    zom.GetComponent<EnemyDamage>().health = 200;
                    zom.GetComponentInChildren<MeshRenderer>().material = Black;
                    break;
            }
        }

    }
}
