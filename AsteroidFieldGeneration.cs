using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidFieldGeneration : MonoBehaviour
{
    public GameObject[] asteroidsBG; //Массив с астероидами для фона
    public Vector2 size; //Диапазон размеров астероидов
    public Vector2 spawnInterval; //Диапазон интервалов появления нового астероида
    public Vector2 spawnWidth; //Крайние точки появления объектов по оси x

    void Start()
    {
        StartCoroutine(SpawnObject());
    }

    IEnumerator SpawnObject()
    {
        while (true)
        {
            GameObject currentObject = asteroidsBG[Random.Range(0, asteroidsBG.Length)]; //Выбираем случайный объект из массива
            float randomSize = Random.Range(size.x, size.y); //Выбираем случайное значение размера объекта
            currentObject.transform.localScale = new Vector3(randomSize, randomSize, randomSize); 
            Vector3 currentObjectPosition = new Vector3(Random.Range(spawnWidth.x, spawnWidth.y), transform.localPosition.y, transform.localPosition.z);
            Instantiate(currentObject, currentObjectPosition, transform.localRotation);
            yield return new WaitForSeconds(Random.Range(spawnInterval.x, spawnInterval.y));
        }

    }

}
