using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{

    public float scrollSpeed;
    public float tileSizeZ;

    private Vector3 startPosition;
    private float newPosition;
    private float zeroTime;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = rb.position;
        zeroTime = Time.time;
    }

    void Update()
    {
        newPosition = Mathf.Repeat((Time.time - zeroTime) * scrollSpeed, tileSizeZ);
        rb.position = startPosition + Vector3.forward * newPosition;
    }
}
