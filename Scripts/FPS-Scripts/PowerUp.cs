using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public int power;
    public Material h, a, n, s;
    public float respawn;
    // Start is called before the first frame update
    void Start()
    {
        if (power == 0)
        {
            int rand = Random.Range(1, 100);
            if (rand < 80)
            {
                Destroy(this.gameObject);
            }
            if (rand >= 80)
            {
                GetComponent<MeshRenderer>().material = a;
                power = 1;
            }
            if (rand > 90)
            {
                GetComponent<MeshRenderer>().material = h;
                power = 2;
            }
            if (rand > 95)
            {
                GetComponent<MeshRenderer>().material = s;
                power = 3;
            }
            if (rand > 98)
            {
                GetComponent<MeshRenderer>().material = n;
                power = 4;
            }
            respawn = 45;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(respawn < 50)
            respawn -= Time.deltaTime;
        if (respawn < 0)
            Destroy(this.gameObject);
    }
}
