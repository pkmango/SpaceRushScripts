using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingAsteroids : MonoBehaviour
{
    public float waitTime;
    public float fireRate;
    public int attacksAmount;
    public float destroyTime;
    public Vector2 spawnWidth; //Крайние точки появления объектов по оси x
    public GameObject[] attackingObjects;

    private SpaceSize spaceSize; // Класс отвечает за определение размеров области отображения

    void Start()
    {
        spaceSize = new SpaceSize();
        StartCoroutine(SpawnAttackingObjects());
    }

    IEnumerator SpawnAttackingObjects()
    {
        yield return new WaitForSeconds(waitTime);

        for (int i=0; i<attacksAmount; i++)
        {
            GameObject currentObject = attackingObjects[Random.Range(0, attackingObjects.Length)]; //Выбираем случайный объект из массива
            Vector3 halfSize;
            if (currentObject.GetComponentInChildren<MeshFilter>() != null)
            {
                // Делаем перемножаем размеры sharedMesh и localScale чтобы получить истинный размер
                halfSize = Vector3.Scale(currentObject.GetComponentInChildren<MeshFilter>().sharedMesh.bounds.extents, currentObject.GetComponentInChildren<MeshFilter>().gameObject.transform.localScale);
            }
            else if (currentObject.GetComponentInChildren<SkinnedMeshRenderer>() != null)
            {
                halfSize = currentObject.GetComponentInChildren<SkinnedMeshRenderer>().bounds.extents;
            }
            else
            {
                halfSize = Vector3.zero;
            }
            Vector3 currentObjectPosition = new Vector3(Random.Range(spawnWidth.x, spawnWidth.y), 0.0f, spaceSize.topRight.z + halfSize.z);
            Instantiate(currentObject, currentObjectPosition, Quaternion.identity);
            //Debug.Log(i);
            yield return new WaitForSeconds(fireRate);
        }

        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }

}
