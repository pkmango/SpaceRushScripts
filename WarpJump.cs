using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpJump : MonoBehaviour
{
    public float startWait;
    public float jumpWait; // Время между прыжками
    public float jumpTime; // Время между исчезновением корабля и началом эффекта появления корабля - afterJumpVFX
    public float jumpTimeAppearance; // Время между началом эффекта afterJumpVFX и появлением корабля
    public GameObject jumpVFX;
    public GameObject afterJumpVFX;

    private SpaceSize spaceSize; // Класс отвечает за определение размеров области отображения
    private Vector3 halfSize;

    void Start()
    {
        spaceSize = new SpaceSize();
        halfSize = GetComponent<Collider>().bounds.extents;
        StartCoroutine(Jump());
    }

    IEnumerator Jump()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            // Исчезает корабль с эффектом jumpVFX
            Instantiate(jumpVFX, transform.position, transform.rotation);
            GetComponentInChildren<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            GetComponentInChildren<ParticleSystem>().Stop();

            // Через время jumpTime пересчитываются новые координаты и включается эффект появления afterJumpVFX
            yield return new WaitForSeconds(jumpTime);
            transform.position = new Vector3(Random.Range(spaceSize.bottomLeft.x + halfSize.x, spaceSize.topRight.x - halfSize.x), transform.position.y, transform.position.z);
            Instantiate(afterJumpVFX, transform.position, transform.rotation);

            // Через время jumpTimeAppearance корабль появляется в новом месте
            yield return new WaitForSeconds(jumpTimeAppearance);
            GetComponentInChildren<MeshRenderer>().enabled = true;
            GetComponent<Collider>().enabled = true;
            GetComponentInChildren<ParticleSystem>().Play();

            yield return new WaitForSeconds(jumpWait);
        }
    }
}
