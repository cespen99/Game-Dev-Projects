using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceGenerator : MonoBehaviour {

    public GameObject star0, star1, star2, star3;
    public GameObject moon0, moon1, moon2, moon3, moon4, moon5, moon6, moon7, moon8, moon9;
    public GameObject planet0, planet1, planet2, planet3, planet4, planet5, planet6, planet7, planet8, planet9, planet10, planet11;
    public Vector2 SpawnRange;
    public int starNumber;
    public int moonNumber;
    public int planetNumber;


    private void Start()
    {
        SpawnRange = GetComponent<Renderer>().bounds.size;
        starNumber = (int)(SpawnRange.x * SpawnRange.y);
        moonNumber = (int)(SpawnRange.x * SpawnRange.y / 17);
        planetNumber = (int)(SpawnRange.x * SpawnRange.y / 50);
        GenerateSpace();
    }
    public void GenerateSpace()
    {
        for (int i = 0; i < starNumber; i++)
        {
            var star = Instantiate(PickRandom("star"), new Vector2(Random.Range(0, SpawnRange.x)-(SpawnRange.x/2), Random.Range(0, SpawnRange.y)- (SpawnRange.y / 2)), Quaternion.identity);
        }
        for (int i = 0; i < moonNumber; i++)
        {
            var moon = Instantiate(PickRandom("moon"), new Vector2(Random.Range(0, SpawnRange.x) - (SpawnRange.x / 2), Random.Range(0, SpawnRange.y) - (SpawnRange.y / 2)), Quaternion.identity);
        }
        for (int i = 0; i < planetNumber; i++)
        {
            var planet = Instantiate(PickRandom("planet"), new Vector2(Random.Range(0, SpawnRange.x) - (SpawnRange.x / 2), Random.Range(0, SpawnRange.y) - (SpawnRange.y / 2)), Quaternion.identity);
        }
    }
    public GameObject PickRandom(string obj)
    {
        float rng = Random.Range(0.0f, 10.0f);
        if (obj=="star")
        {
            if (rng < 7.0f)
                return star0;
            else if (rng < 8.0f)
                return star1;
            else if (rng < 9.0f)
                return star2;
            return star3;
        }
        else if (obj == "moon")
        {
            if (rng < 1.8f)
                return moon0;
            else if (rng < 3.6f)
                return moon1;
            else if (rng < 5.4f)
                return moon2;
            else if (rng < 7.2f)
                return moon3;
            else if (rng < 9f)
                return moon4;
            else if (rng < 9.2f)
                return moon5;
            else if (rng < 9.4f)
                return moon6;
            else if (rng < 9.6f)
                return moon7;
            else if (rng < 9.8f)
                return moon8;
            return moon9;
        }
        else if (obj == "planet")
        {
            if (rng < .83f)
                return planet0;
            else if (rng < 1.66f)
                return planet1;
            else if (rng < 2.49f)
                return planet2;
            else if (rng < 3.32f)
                return planet3;
            else if (rng < 4.15f)
                return planet4;
            else if (rng < 4.98f)
                return planet5;
            else if (rng < 5.81f)
                return planet6;
            else if (rng < 6.64f)
                return planet7;
            else if (rng < 7.47f)
                return planet8;
            else if (rng < 8.3f)
                return planet9;
            else if (rng < 9.13f)
                return planet10;
            return planet11;
        }
        return null;
    }
}
