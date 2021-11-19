/*  Source: 
    Artificial Intelligence for Beginners
    Penny de Byl
    https://learn.unity.com/course/artificial-intelligence-for-beginners
*/

﻿using UnityEngine;
using UnityEngine.AI;

public class Moves : MonoBehaviour
{
    public GameObject target;
    public Collider floor;
    GameObject[] hidingSpots;
    public GameObject[] patrolPoints;
    public int patrolIndex = 0;
    NavMeshAgent agent;

    public int ammo = 0;

    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        patrolPoints = GameObject.FindGameObjectsWithTag("PatrolPoint");
       // hidingSpots = GameObject.FindGameObjectsWithTag("Hide");
    }

    public void Seek(Vector3 location)
    {
        agent.SetDestination(location);
    }

    public void Flee(Vector3 location)
    {
        Vector3 fleeVector = location - this.transform.position;
        agent.SetDestination(this.transform.position - fleeVector);
    }

    public void Pursue()
    {
        Vector3 targetDir = target.transform.position - this.transform.position;

        float relativeHeading = Vector3.Angle(this.transform.forward, this.transform.TransformVector(target.transform.forward));

        float toTarget = Vector3.Angle(this.transform.forward, this.transform.TransformVector(targetDir));

//        if ((toTarget > 90 && relativeHeading < 20) || ds.currentSpeed < 0.01f)
        if ((toTarget > 90 && relativeHeading < 20))
        {
            Seek(target.transform.position);
            return;
        }

//        float lookAhead = targetDir.magnitude / (agent.speed + ds.currentSpeed);
        float lookAhead = targetDir.magnitude / (agent.speed);
        Seek(target.transform.position + target.transform.forward * lookAhead);
    }

    public void Evade()
    {
        Vector3 targetDir = target.transform.position - this.transform.position;
//        float lookAhead = targetDir.magnitude / (agent.speed + ds.currentSpeed);
        float lookAhead = targetDir.magnitude / agent.speed;
        Flee(target.transform.position + target.transform.forward * lookAhead);
    }

    public void Patrol()
    {
        patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
        Seek(patrolPoints[patrolIndex].transform.position);
    }
    Vector3 wanderTarget = Vector3.zero;
    public void Wander()
    {
        float wanderRadius = 10.0f;
        float wanderDistance = 10.0f;
        float wanderJitter = 3.0f;

        wanderTarget += new Vector3(Random.Range(-20.0f, 20.0f) * wanderJitter,
                                        0,
                                        Random.Range(-20.0f, 20.0f) * wanderJitter);
        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        Vector3 targetLocal = wanderTarget + new Vector3(0.0f, 0.0f, wanderDistance);
        Vector3 targetWorld = this.gameObject.transform.InverseTransformVector(targetLocal);

        if (!floor.bounds.Contains(targetWorld))
        {
            targetWorld = -transform.position * 0.1f;

        };

        Seek(targetWorld);
    }

    public void Reload()
    {
        ammo = 5;
    }
    public void RunToBase(Vector3 location)
    {
        agent.SetDestination(location);
    }
    public void Shoot()
    {

    }
    public void Hide()
    {
        float dist = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;
        Vector3 chosenDir = Vector3.zero;
        GameObject chosenGO = hidingSpots[0];

        for (int i = 0; i < hidingSpots.Length; i++)
        {
            Vector3 hideDir = hidingSpots[i].transform.position - target.transform.position;
            Vector3 hidePos = hidingSpots[i].transform.position + hideDir.normalized * 100;

            if (Vector3.Distance(target.transform.position, hidePos) < dist)
            {
                chosenSpot = hidePos;
                chosenDir = hideDir;
                chosenGO = hidingSpots[i];
                dist = Vector3.Distance(this.transform.position, hidePos);
            }
        }

        Collider hideCol = chosenGO.GetComponent<Collider>();
        Ray backRay = new Ray(chosenSpot, -chosenDir.normalized);
        RaycastHit info;
        float distance = 250.0f;
        hideCol.Raycast(backRay, out info, distance);


        Seek(info.point + chosenDir.normalized);

    }
}