using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShow : MonoBehaviour
{
    void Update()
    {
        this.transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f), 10.0f * Time.deltaTime, Space.Self);  
    }
}
