using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellScript : MonoBehaviour
{
    public GameObject parent;
    public Rigidbody rb;
    public float shellVelocity = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
       // rb.velocity = shellVelocity * parent.transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Tank") GameObject.Destroy(collision.gameObject);
    }
}
