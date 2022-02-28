using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRaise : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > 4 || transform.position.y < 0)
            speed *= -1;
        transform.position += new Vector3(0, speed * 0.01f, 0);
    }
}
