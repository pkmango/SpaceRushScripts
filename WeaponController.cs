using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public bool useLasers; // Использовать лазеры?
    public GameObject shot;
    public Transform[] shotSpawns;
    public float fireRateLaser; // Скорострельность для лазеров
    public float delayLaser;
    public bool useRockets; // Использовать ракеты?
    public GameObject rocket;
    public Transform[] rocketSpawns;
    public float delayRocket;
    public float fireRateRocket; // Скорострельность для ракет 
    public bool shootDuringManeuver; // Можно ли стрелять во время совершения маневра?

    private Rigidbody rb;
    private AudioSource audioSource;
    private SpaceSize spaceSize; // Класс отвечает за определение размеров области отображения

    void Start()
    {
        spaceSize = new SpaceSize();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        if (useLasers)
        {
            StartCoroutine(Fire(delayLaser, fireRateLaser, shotSpawns, shot));
        }

        if (useRockets)
        {
            StartCoroutine(Fire(delayRocket, fireRateRocket, rocketSpawns, rocket));
        }
    }

    IEnumerator Fire(float delay, float fireRate, Transform[] spawns, GameObject shot)
    {
        yield return new WaitForSeconds(delay);

        while (true)
        {
            if (transform.position.z < spaceSize.bottomLeft.z)
            {
                yield break;
            }

            foreach (var shotSpawn in spawns)
            {
                if (shootDuringManeuver)
                {
                    MakeShot(shotSpawn, shot);
                }
                else
                {
                    if (rb.rotation.z == 0)
                    {
                        MakeShot(shotSpawn, shot);
                    }
                }
            }
            yield return new WaitForSeconds(fireRate);
        }
    }

    void MakeShot(Transform shotSpawn, GameObject currentShot)
    {
        if (currentShot.tag == "Laser")
        {
            Instantiate(currentShot, shotSpawn.position, shotSpawn.rotation, transform);
        }
        else
        {
            Instantiate(currentShot, shotSpawn.position, shotSpawn.rotation);
        }
        
        if(audioSource != null)
        {
            audioSource.Play();
        }
        
    }
}
