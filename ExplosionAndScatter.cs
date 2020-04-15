using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAndScatter : MonoBehaviour
{
    public float delay;  // Время до взрыва
    public int splintersAmount;  // Количество осколков
    public GameObject splinter;  // Осколок
    public float sector;  // Сектор разлета
    public GameObject effect;  // Эффект при взрыве

    void Start()
    {
        StartCoroutine(Scatter());
    }

    IEnumerator Scatter()
    {
        yield return new WaitForSeconds(delay);

        for (int i=0; i < splintersAmount; i++)
        {
            float step = sector / (splintersAmount - 1) * i - sector / 2 + 180;
            Instantiate(splinter, transform.position, Quaternion.Euler(0, step, 0));
        }

        if (effect != null)
        {
            Instantiate(effect);
        }

        Destroy(gameObject);
    }
}
