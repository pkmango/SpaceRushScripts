using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SimpleTouchPad : MonoBehaviour
{
    public float smoothing;

    private Vector2 origin;
    private Vector2 direction;
    private Vector2 smoothDirection;
    private bool touched;
    private int pointerID;

    void Awake()
    {
        direction = Vector2.zero;
        //touched = false;
    }

    //public void OnPointerDown(PointerEventData data)
    //{
    //    if (!touched)
    //    {
    //        touched = true;
    //        pointerID = data.pointerId;
    //        origin = data.position;
    //    }
    //}

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch myTouch = Input.GetTouch(0);
            if (myTouch.phase == TouchPhase.Moved)
            {
                Vector2 positionChange = myTouch.deltaPosition;
                //positionChange.y = -positionChange.y;
                //direction = positionChange.normalized;
                direction = positionChange;
            }
            else
            {
                direction = Vector2.zero;
            }
        }
    }

    //-----Этот метод работает с задержкой------------------------

    //public void OnDrag(PointerEventData data)
    //{
    //    if (data.pointerId == pointerID)
    //    {
    //        Vector2 currentPosition = data.position;
    //        Vector2 directionRaw = currentPosition - origin;
    //        direction = directionRaw.normalized;
    //    }
    //}

    //public void OnPointerUp(PointerEventData data)
    //{
    //    if (data.pointerId == pointerID)
    //    {
    //        direction = Vector2.zero;
    //        touched = false;
    //    }
    //}

    public Vector2 GetDirection()
    {
        //smoothDirection = Vector2.MoveTowards(smoothDirection, direction, smoothing);
        //return smoothDirection;

        return direction;
    }
}