using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddingRotation : MonoBehaviour
{
    public Vector3 rotationSpeed;

    void Start()
    {
        //GetComponent<Rigidbody>().angularVelocity = rotationSpeed;
    }

    void FixedUpdate()
    {
        //transform.eulerAngles += rotationSpeed;
        transform.Rotate(rotationSpeed);
    }

}