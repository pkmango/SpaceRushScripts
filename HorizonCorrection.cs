using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizonCorrection : MonoBehaviour
{
    void Start()
    {
        GetComponent<Rigidbody>().position = new Vector3(GetComponent<Rigidbody>().position.x, 0.0f, GetComponent<Rigidbody>().position.z);
    }
}
