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
    public GameObject[] patrolPoints;
    public int patrolIndex = 0;
    NavMeshAgent agent;

    private float freq = 5.0f;
    public int ammo = 0;

    public GameObject shell;
    public Transform shellSpawner;
    public float shellVelocity = 40.0f;

    public GameObject turret;
    float angleToRotate = 0.0f;

    float gravity;

    void Start()
    {
        gravity = -(Physics.gravity.magnitude);
        agent = this.GetComponent<NavMeshAgent>();
        patrolPoints = GameObject.FindGameObjectsWithTag("PatrolPoint");
    }

    public void Seek(Vector3 location)
    {
        agent.SetDestination(location);
    }

    public void Patrol()
    {
        patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
        Seek(patrolPoints[patrolIndex].transform.position);
    }
    Vector3 wanderTarget = Vector3.zero;
    public void Wander()
    {
        TurnAround();
        
        float wanderRadius = 100.0f;
        float wanderDistance = 50.0f;
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
        turret.transform.LookAt(location, new Vector3(0.0f, 1.0f, 0.0f));
        agent.SetDestination(location);
    }
    public void Shoot()
    {
        if (freq >= 1.0f)
        {
            float angle = ShootingAngle();

            Quaternion prevRotation = shellSpawner.transform.rotation;
            shellSpawner.transform.localRotation = Quaternion.AngleAxis(angle, shellSpawner.worldToLocalMatrix * transform.right);
            Debug.Log(angle);
            Rigidbody rb = Instantiate(shell.GetComponent<Rigidbody>(), shellSpawner.position, shellSpawner.localRotation);
            rb.velocity = shellSpawner.worldToLocalMatrix * shellSpawner.forward * shellVelocity;
            shellSpawner.transform.rotation = prevRotation;
            freq = 0.0f;
            ammo--;
        }
        else
        {
            turret.transform.LookAt(target.transform, new Vector3(0.0f, 1.0f, 0.0f));
            freq += Time.deltaTime;
        }

    }

    private float ShootingAngle()
    {
        float a = Mathf.Pow(shellVelocity, 2);
        float b = Mathf.Pow(shellVelocity, 4);
        float c = gravity * (gravity * Mathf.Pow(transform.position.z, 2) + (2 * transform.position.y * Mathf.Pow(shellVelocity, 2)));
        float d = gravity * transform.position.z;
        float shootingAngle = (a - (Mathf.Sqrt(b - c))) / d;

        return Mathf.Rad2Deg * (Mathf.Atan(shootingAngle));
    }

    public void TurnAround()
    {
        if (freq > 1.0f)
        {
            angleToRotate = Random.Range(-180.0f, 180.0f);
            freq = 0.0f;
        }
        else
        {
            Quaternion rotationTurret;
            Vector3 axisRotation = new Vector3(0.0f, 1.0f, 0.0f);
            rotationTurret = Quaternion.AngleAxis(angleToRotate * Time.deltaTime, axisRotation);
            turret.transform.rotation *= rotationTurret;
            freq += Time.deltaTime;
        }
    }
}
