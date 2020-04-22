using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparencyChange : MonoBehaviour
{
    public float changeTime;

    private Color myColor = Color.white;

    void Start()
    {
        StartCoroutine(SmoothChange(0f, 1f, changeTime));
    }

    IEnumerator SmoothChange(float from, float to, float timer)
    {

        float t = 0.0f;

        myColor.a = from;

        while (t < 1.0f)
        {
            t += Time.deltaTime / timer;

            myColor.a = Mathf.Lerp(from, to, t);
            gameObject.GetComponent<Renderer>().material.color = myColor;

            yield return null;
        }


    }

}
