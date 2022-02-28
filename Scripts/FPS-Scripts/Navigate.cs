using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigate : MonoBehaviour
{
    NavMeshAgent agent;
    bool setActive;
    [SerializeField] private Transform target;    // Start is called before the first frame update
    void Start()
    {
        setActive = false;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!setActive)
            setActive = true;
    }
}
