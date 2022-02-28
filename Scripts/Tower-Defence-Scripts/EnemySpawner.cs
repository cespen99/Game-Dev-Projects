using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy1, enemy2, enemy3;
    [SerializeField] private int hoard = 0;
    [SerializeField] private int wave = 0;
    [SerializeField] private float waitTIme = 0.5f;
    [SerializeField] private float timer = 5f;
    [SerializeField] private float spawnTimer = 15f;
    [SerializeField] private int[] fib;
    [SerializeField] private bool enemiesSpawning = false;
    [SerializeField] private Text waveTxt;
    [SerializeField] private Text enemiesTxt;
    [SerializeField] private int total;

    void Update()
    {
        if (timer <= 0 && !enemiesSpawning)
        {
            timer = spawnTimer;
            enemiesSpawning = true;
            if (wave == 0)
            {
                fib = new int[3];
                fib[0] = fib[1] = 1;
            }
            fib[2] = fib[1];
            fib[1] = fib[0];
            fib[0] = fib[1] + fib[2];
            total = fib[0];
            wave++;
            waveTxt.text = "Wave: " + wave;
            
            StartCoroutine("SpawnWave");
        }
        int enemiesLeft = GameObject.FindGameObjectsWithTag("Enemy").Length;
        enemiesTxt.text = "Enemies: " + enemiesLeft;
        if (!enemiesSpawning && enemiesLeft == 0)
            timer -= Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < 10; i++)
        {
            int rand = Random.Range(1, 4);
            SpawnEnemy(rand);
            yield return new WaitForSeconds(waitTIme);
        }
        enemiesSpawning = false;
        /*
        while (total > 0)
        {
            if (total <= 10)
                hoard = total;
            else
                hoard = Random.Range(1, 11);
            for (int i = 0; i < hoard; i++)
            {
                int rand = Random.Range(1, 100 - wave);//1-100 but harder to get over 60 as levels progress
                if (wave < 6)                                         
                    SpawnEnemy(rand + 50);
                else if (wave < 11)
                    SpawnEnemy(rand + 25);
                else                                                  
                    SpawnEnemy(rand);
                yield return new WaitForSeconds(waitTIme);
            }
            total -= hoard;
        }
        enemiesSpawning = false;
        */
    }

    private void SpawnEnemy(int n)
    {
        if(n == 1)
            Instantiate(enemy3, transform.position, Quaternion.identity);
        else if(n  == 2)
            Instantiate(enemy2, transform.position, Quaternion.identity);
        else
            Instantiate(enemy1, transform.position, Quaternion.identity);
    }
}
