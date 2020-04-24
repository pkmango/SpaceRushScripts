using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public WeaponSettings[] weapons; // Коллекция возможных вооружений
    public int weaponID; // Номер оружия
    public float zMax;  // ограничтель верхней границы полета
    public GameObject engine;

    private SpaceSize spaceSize;
    private float nextFire;
    private Quaternion calibrationQuaternion;
    private Vector3 halfSize;

    void Start()
    {
        //halfSize = GetComponent<MeshFilter>().mesh.bounds.extents;
        halfSize = GetComponent<Collider>().bounds.extents;
        spaceSize = new SpaceSize();
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + weapons[weaponID].fireRate;
            foreach (var shotSpawn in weapons[weaponID].shotSpawns)
            {
                Instantiate(weapons[weaponID].shot, shotSpawn.position, shotSpawn.rotation);
            }
                
            GetComponent<AudioSource>().Play();
        }

        if(Input.touchCount > 0)
        {
            Touch myTouch = Input.GetTouch(0);
            if (myTouch.phase == TouchPhase.Moved)
            {
                float deltaPosWidthPercent = myTouch.deltaPosition.x / Screen.width * 100.0f;  // Какой процент составляет deltaPosition.x от ширины экрана
                float deltaPosHeightPercent = myTouch.deltaPosition.y / Screen.height * 100.0f;  // Какой процент составляет deltaPosition.y от ширины экрана
                float correctionX = spaceSize.widthSpace * deltaPosWidthPercent / 100.0f;  // Вычисляем коррекцию по оси x
                float correctionZ = spaceSize.heightSpace * deltaPosHeightPercent / 100.0f;  // Вычисляем коррекцию по оси z
                GetComponent<Rigidbody>().position = new Vector3
                (
                    Mathf.Clamp(GetComponent<Rigidbody>().position.x + correctionX, spaceSize.bottomLeft.x + halfSize.x, spaceSize.topRight.x - halfSize.x),
                    0.0f,
                    Mathf.Clamp(GetComponent<Rigidbody>().position.z + correctionZ, spaceSize.bottomLeft.z + halfSize.z, spaceSize.topRight.z - halfSize.z - zMax)
                );
            }
        }
    }
}