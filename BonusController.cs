using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusController : MonoBehaviour
{
    public int weaponID;
    public GameObject bonusExplosion;

    void Start()
    {
 
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //Debug.Log(other.gameObject.GetComponent<PlayerController>().weaponID);
            other.gameObject.GetComponent<PlayerController>().weaponID = weaponID;
            Instantiate(bonusExplosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
