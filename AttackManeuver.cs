using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManeuver : MonoBehaviour
{
    public float startWait;
    public float attackSpeed;
    public float tilt;
    public float turnSpeed;
    public bool turnOn;

    private Rigidbody rb;
    private float speed;
    private Vector3 direction;
    private Transform player;
    private Quaternion rotation;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(startWait);
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.GetComponent<Transform>();
            direction = GetComponent<Rigidbody>().position - player.position;
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            rotation = Quaternion.AngleAxis(angle, Vector3.up);
        }
        else
        {
            direction = new Vector3(0, 0, 10);
        }

        speed = attackSpeed;
    }

    private void Update()
    {
        //transform.LookAt(player);
        if (turnOn)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);
        }
        else
        {
            GetComponent<Rigidbody>().rotation = Quaternion.Euler(0f, 0f, GetComponent<Rigidbody>().velocity.x * -tilt);
        }
        
        
        rb.AddForce(direction.normalized * speed, ForceMode.Force);
    }
}
