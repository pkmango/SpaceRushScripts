using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duplicate : MonoBehaviour
{
    public GameObject secondObject;
    public float waitTime;

    private SpaceSize spaceSize; // Класс отвечает за определение размеров области отображения

    void Start()
    {
        spaceSize = new SpaceSize();
        StartCoroutine(InstantiateSecondObj());
        InstantiateSecondObj();
    }

    IEnumerator InstantiateSecondObj()
    {
        yield return new WaitForSeconds(waitTime);
        Vector3 halfSize;
        if (secondObject.GetComponentInChildren<MeshFilter>() != null)
        {
            halfSize = secondObject.GetComponentInChildren<MeshFilter>().sharedMesh.bounds.extents;
        }
        else if (secondObject.GetComponentInChildren<SkinnedMeshRenderer>() != null)
        {
            halfSize = secondObject.GetComponentInChildren<SkinnedMeshRenderer>().bounds.extents;
        }
        else
        {
            halfSize = Vector3.zero;
        }
        Vector3 spawnPosition = new Vector3(Random.Range(spaceSize.bottomLeft.x + halfSize.x, spaceSize.topRight.x - halfSize.x), 0.0f, spaceSize.topRight.z + halfSize.z);
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate(secondObject, spawnPosition, spawnRotation);
    }
}
