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
    public GameObject leftTrail;
    public GameObject rightTrail;

    private float freq = 5.0f;
    private float freqWander = 1.5f;
    private float freqShoot = 1.5f;
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
        StartTrail();
        agent.SetDestination(location);
    }

    public void Patrol()
    {
        patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
        Seek(patrolPoints[patrolIndex].transform.position);
    }
    public void Wander()
    {
        if (freqWander >= 1.75f)
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

           // Vector3 targetLocal = wanderTarget + new Vector3(0.0f, 0.0f, wanderDistance);
            Vector3 targetWorld = this.transform.position + wanderTarget;

            Seek(targetWorld);
            freqWander = 0.0f;
        }
        else
            freqWander += Time.deltaTime;
    }

    public void Reload()
    {
        ammo = 5;
    }
    public void RunToBase(Vector3 location)
    {
        StartTrail();

        turret.transform.LookAt(location, new Vector3(0.0f, 1.0f, 0.0f));
        agent.SetDestination(location);
    }
    public void Shoot()
    {
        if (target)
        {
            StopTrail();

            if (freqShoot >= 1.0f)
            {
                float angle = ShootingAngle();

                Vector3 euler = shellSpawner.transform.localRotation.eulerAngles;
                shellSpawner.transform.localRotation = Quaternion.identity;

                shellSpawner.transform.localRotation = Quaternion.Euler(angle, euler.y, euler.z);

                Rigidbody rb = Instantiate(shell.GetComponent<Rigidbody>(), shellSpawner.transform.position, shellSpawner.transform.rotation);
                rb.velocity = shellSpawner.transform.forward * shellVelocity;
                rb.gameObject.GetComponent<ShellScript>().parent = this.gameObject;

                freqShoot = 0.0f;
                ammo--;
            }
            else
            {
                turret.transform.LookAt(target.transform, new Vector3(0.0f, 1.0f, 0.0f));
                freqShoot += Time.deltaTime;
            } 
        }

    }

    private float ShootingAngle()
    {
        float a = Mathf.Pow(shellVelocity, 2);
        float b = Mathf.Pow(shellVelocity, 4);
        float targetY = 0;
        float targetZ = Vector3.Distance(shellSpawner.transform.worldToLocalMatrix * target.transform.position, shellSpawner.transform.localPosition);
        float c = gravity * (gravity * Mathf.Pow(targetZ, 2) + (2 * targetY * Mathf.Pow(shellVelocity, 2)));
        float d = gravity * targetZ;
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

    private void StartTrail()
    {
        rightTrail.GetComponent<ParticleSystem>().Play();
        leftTrail.GetComponent<ParticleSystem>().Play();
    }
    private void StopTrail()
    {
        rightTrail.GetComponent<ParticleSystem>().Stop();
        leftTrail.GetComponent<ParticleSystem>().Stop();
    }
}
