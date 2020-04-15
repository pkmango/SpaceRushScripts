using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPosition : MonoBehaviour
{
    public Vector3 requiredPosition;

    void Awake()
    {
        // Если x равен 0 то оставляем случайное значение x
        if(requiredPosition.x != 0)
        {
            transform.position = requiredPosition;
        }
        else
        {
            transform.position = new Vector3(transform.position.x, requiredPosition.y, requiredPosition.z);
        }
    }

}
