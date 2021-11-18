using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankIAMovement : MonoBehaviour
{
    
    // Variables

    private float freq = 0.0f;
    public Moves moves;
    public Transform target;
    public Transform shellSpawner;
    public Rigidbody shell;
    public float shellVelocity = 10.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        freq += Time.deltaTime;
        if (freq > 0.5)
        {
            freq -= 0.5f;
            moves.Wander();

            //ShootShell();
        }

        
    }

    void ShootShell()
    {
        float angle = (shellVelocity * shellVelocity) - Mathf.Sqrt(Mathf.Pow(shellVelocity, 4) - 9.8f * (9.8f * Mathf.Pow(target.position.x, 2) + 2 * target.position.y * Mathf.Pow(shellVelocity, 2)) / 9.8f * transform.position.x);
        Vector3 axis = new Vector3(1.0f, 1.0f, 0.0f);
        Quaternion shellAngle = Quaternion.AngleAxis(angle, axis);
     
        Rigidbody shellBullet = Instantiate(shell, shellSpawner.position, shellAngle) as Rigidbody;
        shellBullet.velocity = shellVelocity * this.transform.forward;
    }
}
