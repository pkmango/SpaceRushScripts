using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpMover : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 endPosition;
    public float speed;
    private float progress = 0;

    void Start()
    {
        transform.localPosition = startPosition;
    }

    void FixedUpdate()
    {
        if (progress <= 0.999)
        {
            progress += speed * (1 - progress);
            //Debug.Log(progress);
            transform.localPosition = Vector3.Lerp(startPosition, endPosition, progress);
        }
   
    }
}
