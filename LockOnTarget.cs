using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnTarget : MonoBehaviour
{
    public float turnSpeed;

    private Vector3 direction;
    private Transform player;
    private Quaternion rotation;

    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.GetComponent<Transform>();
        }
    }
    
    void Update()
    {
        if (transform.rotation.eulerAngles.z == 0)
        {
            //transform.LookAt(player);
            if (player != null)
            {
                //Debug.Log(GetComponent<Transform>().position);
                //direction = GetComponent<Rigidbody>().position - player.position;
                direction = transform.position - player.position;
            }
            else
            {
                direction = Vector3.zero;
            }
        
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            rotation = Quaternion.AngleAxis(angle, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);
        }
        
    }
}
