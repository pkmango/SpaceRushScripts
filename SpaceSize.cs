using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceSize
{
    public float widthSpace;
    public float heightSpace;
    public Camera cam;
    public Vector3 bottomLeft;
    public Vector3 topRight;

    public SpaceSize()
    {
        cam = Camera.main;
        bottomLeft = cam.ScreenToWorldPoint(new Vector2(0, 0));  // Получаем мировые координаты левой нижней точки
        topRight = cam.ScreenToWorldPoint(new Vector2(cam.pixelWidth, cam.pixelHeight)); // Получаем мировые координаты правой верхней точки
        widthSpace = Mathf.Abs(bottomLeft.x) + Mathf.Abs(topRight.x);  // Ширина области отображения в мировых единицах (мировая система координат)
        heightSpace = Mathf.Abs(bottomLeft.y) + Mathf.Abs(topRight.y);  // Высота области отображения в мировых единицах (мировая система координат)
        //Debug.Log("widthSpace: " + widthSpace);
        //Debug.Log("heightSpace: " + heightSpace);
        //Debug.Log("Ширина экрана: " + cam.pixelWidth);
        //Debug.Log("Высота экрана: " + cam.pixelHeight);
        //Debug.Log("bottomLeft: " + bottomLeft);
        //Debug.Log("topRight: " + topRight);
    }
}