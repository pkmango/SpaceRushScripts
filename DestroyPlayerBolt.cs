using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlayerBolt : MonoBehaviour
{
    private SpaceSize spaceSize;
    private Vector3 halfSize;

    void Start()
    {
        halfSize = GetComponent<Collider>().bounds.extents;
        spaceSize = new SpaceSize();
    }

    void Update()
    {
        if (GetComponent<Rigidbody>().position.z + halfSize.z > spaceSize.topRight.z)
        {
            Destroy(gameObject);
        }
    }
}
