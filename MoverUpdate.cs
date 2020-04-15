using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverUpdate : MonoBehaviour
{
    public float speed;

    private Vector3 startPosition;
    private float zeroTime;

    void Start()
    {
        startPosition = transform.position;
        zeroTime = Time.time;
    }

    void Update()
    {
        transform.position = startPosition + Vector3.forward * (Time.time - zeroTime) * speed;
    }
}
