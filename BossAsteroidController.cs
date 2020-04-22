using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAsteroidController : MonoBehaviour
{
    public float startWait;
    //public float shakeTime;
    //public float shakeRate;
    //public float firstManeuverTime;
    public float maneuverRate;
    public float maneuverTime;
    public float maneuverPositionTop; //Позиция сверху по оси Z, которую нужно занять после выполнения маневра
    public float maneuverPositionBottom; // Нижняя позиция по оси Z
    public float bottomRotation;
    public GameObject energyShield;
    public AddingRotation currentRotation;   

    private Vector3 currentPosition;
    private GameObject newShield;
    private Color myColor = Color.white;

    void Start()
    {
        //maneuverPositionBottom = transform.position.z;
        StartCoroutine(Maneuver());
    }

    IEnumerator Maneuver()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            if (currentPosition.z != maneuverPositionTop)
            {
                newShield = Instantiate(energyShield, transform.position, Quaternion.identity);
                StartCoroutine(ChangePosition(maneuverPositionBottom, maneuverPositionTop, maneuverTime));
                currentRotation.rotationSpeed.y = 0.5f;
            }
            else
            {
                StartCoroutine(ChangePosition(maneuverPositionTop, maneuverPositionBottom, maneuverTime));
                StartCoroutine(newShield.GetComponent<TransparencyChange>().SmoothChange(1f, 0f)); //Запуск эффекта затухания
                newShield.GetComponent<TransparencyChange>().toDestroy = "yes"; // Команда на уничтожение после отработки эффекта
                currentRotation.rotationSpeed.y = bottomRotation;
            }
            
            yield return new WaitForSeconds(maneuverRate);
        }
        
    }

    IEnumerator ChangePosition(float position1, float position2, float timer)
    {
        float t = 0.0f;
        currentPosition.z = position1;

        while (t < 1.0f)
        {
            t += Time.deltaTime / timer;

            currentPosition.z = Mathf.Lerp(position1, position2, t);
            transform.position = currentPosition;
            if (newShield != null)
            {
                newShield.transform.position = currentPosition;
            }

            yield return null;
        }
    }


}
