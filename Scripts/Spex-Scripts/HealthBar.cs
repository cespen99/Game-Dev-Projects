using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject player, green, red;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + new Vector3(0f, 1f, 0f);
        green.transform.localScale = new Vector3((float)player.GetComponent<PlayerScript>().health / 100, 1f, 1f);
        green.transform.localPosition = new Vector3(((float)player.GetComponent<PlayerScript>().health - 50) * (0.6f / 50), 0f, 0f);
    }
}
