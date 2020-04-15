using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAnimator : MonoBehaviour
{
    public float destroyTime;
    private float startTime;
    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        if (Time.time - startTime > destroyTime)
        {
            Destroy(GetComponent<Animator>());
        }
    }
}
