using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpicLaserController : MonoBehaviour
{
    public ParticleSystem laser;
    public ParticleSystem sparks;
    public AudioSource preparingSound;
    public AudioSource shotSound;
    public float delay;
    public float duration;



    void Start()
    {
        preparingSound.Play();
        StartCoroutine(MakeShot());
        
    }

    IEnumerator MakeShot()
    {
        yield return new WaitForSeconds(delay);
        GetComponent<Collider>().enabled = true;
        shotSound.Play();
        laser.gameObject.SetActive(true);
        laser.Play();
        sparks.gameObject.SetActive(true);
        sparks.Play();

        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

}
