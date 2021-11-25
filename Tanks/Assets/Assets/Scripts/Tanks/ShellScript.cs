using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellScript : MonoBehaviour
{
    public GameObject parent;
    public TankHP tankHP;
    public GameObject particle;

    // Start is called before the first frame update
    void Start()
    {
        tankHP = parent.GetComponent<Moves>().target.GetComponent<TankHP>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == parent.GetComponent<Moves>().target)
        {
            tankHP.TakeDamage();
        }

        if (collision.gameObject != parent)
        {
            particle.GetComponent<AudioSource>().Play();
            particle.transform.parent = null;
            particle.GetComponent<ParticleSystem>().Play();
            ParticleSystem.MainModule module = particle.GetComponent<ParticleSystem>().main;
            GameObject.Destroy(particle, module.duration);
            GameObject.Destroy(this.gameObject);
            Debug.Log(collision.gameObject);
        }
    }
}
