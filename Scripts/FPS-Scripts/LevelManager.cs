using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    int wave;
    bool noMoreSpawns;
    public Text waveIndicator, enemyIndicator;
    public MonsterSpawner spawner;

    void Start()
    {
        noMoreSpawns = false;
        wave = 0;
        StartLevel(++wave);
    }

    void Update()
    {
        enemyIndicator.text = "Enemies Remaining: " + GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (!noMoreSpawns)
            enemyIndicator.text = "Enemies Remaining: ?";
        if (noMoreSpawns && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            noMoreSpawns = false;
            StartLevel(++wave);
        }
    }

    void StartLevel(int num)
    {
        waveIndicator.text = ("WAVE " + wave.ToString());
        if (num > 5)
            num = 5;
        StartCoroutine("Level" + num);
        
    }

    IEnumerator Level1()
    {
        yield return new WaitForSeconds(3);
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(1);
            spawner.Spawn(1);
        }
        noMoreSpawns = true;
    }

    IEnumerator Level2()
    {
        yield return new WaitForSeconds(3);
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(1);
            spawner.Spawn(1);
        }
        noMoreSpawns = true;
    }

    IEnumerator Level3()
    {
        yield return new WaitForSeconds(3);
        for (int i = 0; i < 30; i++)
        {
            while(GameObject.FindGameObjectsWithTag("Enemy").Length >=25)
                yield return new WaitForSeconds(1);
            spawner.Spawn(1);
        }
        noMoreSpawns = true;
    }

    IEnumerator Level4()
    {
        yield return new WaitForSeconds(3);
        for (int i = 0; i < 40; i++)
        {
            while (GameObject.FindGameObjectsWithTag("Enemy").Length >= 25)
                yield return new WaitForSeconds(1);
            yield return new WaitForSeconds(1);
            spawner.Spawn(1);
        }
        noMoreSpawns = true;
    }

    IEnumerator Level5()
    {
        yield return new WaitForSeconds(3);
        for (int i = 0; i < 50; i++)
        {
            while (GameObject.FindGameObjectsWithTag("Enemy").Length >= 30)
                yield return new WaitForSeconds(1);
            yield return new WaitForSeconds(1);
            spawner.Spawn(1);
        }
        noMoreSpawns = true;
    }
}
