using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    public GameObject shield;
    public int health;
    public int scoreValue;
    public AudioSource hitSound;

    private bool objectDestroyed; // Уничтожен ли объект? защита от двойного инициирования OnTriggerEnter
    private GameController gameController;

    void Start()
    {
        objectDestroyed = false;
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            //Debug.Log("Cannot find 'GameController' script");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy") || other.CompareTag("Bolt") || other.CompareTag("Bonus") || other.CompareTag("Laser"))
        {
            return;
        }

        if (CompareTag("Bolt") && other.CompareTag("PlayerBolt"))
        {
            return;
        }

        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().weaponID = 0;
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            other.gameObject.SetActive(false);
            for (int i = 0; i < gameController.Lifes.Length; i++)
            {
                if (gameController.Lifes[i].activeSelf)
                {
                    gameController.Lifes[i].SetActive(false);

                    if (i == 2)
                    {
                        gameController.CanvasGroupActivation(gameController.pauseButton, false);
                        gameController.Invoke("GameOver", 1.5f);
                    }
                    else
                    {
                        gameController.Rebirth(other.gameObject);
                        return;
                    }
                    break;
                }
            }
            
        }

        if (shield != null)
        {
            Destroy(shield);
        }
        else
        {
            if (health > 1)
            {
                health--;
                if (hitSound != null)
                {
                    hitSound.Play();
                }
                
            }
            else
            {
                if (!objectDestroyed)
                {
                    objectDestroyed = true;

                    if (explosion != null)
                    {
                        Instantiate(explosion, transform.position, transform.rotation);
                    }

                    if (!CompareTag("Bolt") && (Random.value < gameController.bonusChance))
                    {
                        Instantiate(gameController.bonuses[Random.Range(0, gameController.bonuses.Length)], new Vector3(transform.position.x, 0, transform.position.z), Quaternion.Euler(0.0f, 0.0f, 0.0f));
                    }
                    
                    gameController.AddScore(scoreValue);
                }

                Destroy(gameObject);
            }  
        }

        Destroy(other.gameObject);
    }
}
