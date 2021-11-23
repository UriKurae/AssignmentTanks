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
    public GameObject shellSpawner;
    public float shellVelocity = 10.0f;

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
    public void Wander()
    {
        Vector3 wanderTarget = Vector3.zero;
        TurnAround();
        
        float wanderRadius = 10.0f;
        float wanderDistance = 10.0f;
        float wanderJitter = 3.0f;

        wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter,
                                        0,
                                        Random.Range(-1.0f, 1.0f) * wanderJitter);
        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        Vector3 targetLocal = wanderTarget + new Vector3(0.0f, 0.0f, wanderDistance);
        Vector3 targetWorld = this.transform.position + targetLocal;

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

            Debug.Log(angle);
            shellSpawner.transform.Rotate(angle, 0.0f, 0.0f, Space.Self);
            //shellSpawner.rotation = Quaternion.AngleAxis(shellSpawner.eulerAngles., new Vector3(0.0f, 1.0f, 0.0f));
            Rigidbody rb = Instantiate(shell.GetComponent<Rigidbody>(), shellSpawner.transform.position, shellSpawner.transform.localRotation);
            rb.velocity = shellSpawner.transform.forward * shellVelocity;
            shellSpawner.transform.Rotate(-angle, 0.0f, 0.0f, Space.Self);
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
        float c = gravity * (gravity * Mathf.Pow(target.transform.position.z, 2) + (2 * target.transform.position.y * Mathf.Pow(shellVelocity, 2)));
        float d = gravity * target.transform.position.z;
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
