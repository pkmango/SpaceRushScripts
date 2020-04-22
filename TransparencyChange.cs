using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparencyChange : MonoBehaviour
{
    public float changeTime = 0.4f;
    public string toDestroy = "no"; // Нужно ли унчичтожить объект после отработки эффекта?

    private Color myColor = Color.white;

    void Start()
    {

        StartCoroutine(SmoothChange(0f, 1f));
    }

    public IEnumerator SmoothChange(float from, float to)
    {

        float t = 0.0f;

        myColor.a = from;

        while (t < 1.0f)
        {
            t += Time.deltaTime / changeTime;

            myColor.a = Mathf.Lerp(from, to, t);
            gameObject.GetComponent<Renderer>().material.color = myColor;

            yield return null;
        }

        if(toDestroy == "yes")
        {
            Destroy(gameObject);
        }

    }

}
