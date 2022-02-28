using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessCharacterController : MonoBehaviour
{
    public float runSpeed;
    public float turnSpeed;
    private void Awake()
    {
        
    }
    void Start()
    {

    }
    void Update()
    {

    }

    void FixedUpdate()
    {
        gameObject.transform.position += new Vector3(0, 0, runSpeed / 10);
        Debug.Log("Update - Position - " + gameObject.transform.position + " - Time - " + Time.time);
    }
}
