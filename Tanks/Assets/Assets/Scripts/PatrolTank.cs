using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolTank : MonoBehaviour
{
    public GameObject[] wayPoints;
    public NavMeshAgent agent;
    int patrolWP = 0;

    // Start is called before the first frame update
    void Start()
    {
        wayPoints = GameObject.FindGameObjectsWithTag("PatrolPoint");
       //agent.SetDestination(wayPoints[0].transform.position);
    }

    // Update is called once per frame
    void Update()
    {
       // if (!agent.pathPending && agent.remainingDistance < 0.5f) Patrol();
        
    }

    void Patrol()
    {
        patrolWP = (patrolWP + 1) % wayPoints.Length;
        Seek(wayPoints[patrolWP].transform.position);
    }

    void Seek(Vector3 position)
    {
        agent.SetDestination(position);
        agent.destination = position;
    }
}
