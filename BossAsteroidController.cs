using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAsteroidController : MonoBehaviour
{
    public float startWait;
    public float shakeTime;
    public float shakeRate;
    public float firstManeuverTime;
    public float maneuverRate;
    public float maneuverTime;
    public float maneuverPosition1;
    public float maneuverPosition2;
    public GameObject energyShield;


    void Start()
    {
        
        StartCoroutine(AddingShield());
    }

    IEnumerator AddingShield()
    {
        yield return new WaitForSeconds(startWait);
        Instantiate(energyShield, transform.position, Quaternion.identity);
        
    }


}
