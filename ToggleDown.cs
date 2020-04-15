using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToggleDown : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<Toggle>().OnPointerClick();
    }

    private void OnMouseDown()
    {
        Debug.Log("привет");
    }

    //void OnGUI()
    //{
    //    Debug.Log("привет2");
    //}

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("привет3");
    }

    public void OnPointerClick(PointerEventData data)
    {
        Debug.Log("OnPointerClick called.");
    }

}
