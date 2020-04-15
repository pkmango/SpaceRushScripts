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

    void Start()
    {
        startPosition = transform.position;
        //startPosition = new Vector3(transform.position.x, transform.position.y, 0);
        //newPosition = 0;
        zeroTime = Time.time;
    }

    void Update()
    {
        newPosition = Mathf.Repeat((Time.time - zeroTime) * scrollSpeed, tileSizeZ);
        transform.position = startPosition + Vector3.forward * newPosition;
    }
}
