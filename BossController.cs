using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private GameController gameController;
    private bool isShuttingDown;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();

            gameController.bossHelthBar.gameObject.SetActive(true);
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    void OnApplicationQuit()
    {
        isShuttingDown = true;
    }

    private void OnDestroy()
    {
        gameController.bossHelthBar.gameObject.SetActive(false);

        GameObject[] allLasers = GameObject.FindGameObjectsWithTag("Laser");
        for (int i = 0; i < allLasers.Length; i++)
        {
            Destroy(allLasers[i]);
        }

        if (!isShuttingDown && !gameController.isShuttingDown)
        {
            gameController.CanvasGroupActivation(gameController.pauseButton, false);
            gameController.Invoke("LevelCompleted", 1.5f);
        }  
    }
}
