using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class АddingPieces : MonoBehaviour
{
    public GameObject[] pieces; //Массив из осколков
    public float sector; // Сектор разлета осколков

    private float step; // Шаг через который меняется угол разлета
    private bool isShuttingDown;
    private GameController gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }

        isShuttingDown = false;

        if (pieces.Length == 1)
        {
            sector = 0;
            step = 0;
        }
        else
        {
            step = sector / (pieces.Length - 1);
        }
    }

    void OnApplicationQuit()
    {
        isShuttingDown = true;
    }

    private void OnDestroy()
    {
        if (!isShuttingDown && !gameController.isShuttingDown)
        {
            for (int i = 0; i < pieces.Length; i++)
            {
                Instantiate(pieces[i], this.transform.position, Quaternion.Euler(0, -sector/2 + i*step, 0));
            }
        }
    }
}
