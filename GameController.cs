using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public WaveController[] waves; // Волны врагов, каждая настраивается в отдельном объекте
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    public Text levelNumberText;
    public GameObject restartButton;
    public GameObject[] Lifes;
    public GameObject[] bonuses;
    public float bonusChance;
    public string nextSceneName;
    public CanvasGroup pauseCanvasGroup;
    public CanvasGroup gameOverCanvasGroup;
    public CanvasGroup winCanvasGroup;
    public CanvasGroup pauseButton;
    public Toggle toggleMusic;
    public Toggle toggleSound;
    public bool isShuttingDown; // Флаг показывает закрываем ли мы сцену и нужно ли создавать объекты при вызове onDestroy

    private bool gameOver;
    private int score;
    private SpaceSize spaceSize; // Класс отвечает за определение размеров области отображения
    private int count; // счетчик для функции 
    private GameObject player;
    private bool pause; // Нажата ли пауза?
    private SceneController sceneController;
    private AudioController audioController;
    //private float startTime;

    // Стиль и счетчик для отображения номера волны врагов (для дебага)
    private GUIStyle style = new GUIStyle();
    private int waveNumber;

    void Start()
    {
        style.normal.textColor = Color.white;
        style.fontSize = 32;
        style.fontStyle = FontStyle.Bold;

        isShuttingDown = false;
        //startTime = Time.time;
        audioController = FindObjectOfType<AudioController>();
        sceneController = FindObjectOfType<SceneController>();
        pause = false;
        CanvasGroupActivation(pauseCanvasGroup, false);
        CanvasGroupActivation(gameOverCanvasGroup, false);
        CanvasGroupActivation(winCanvasGroup, false);
        SetSoundsToggle();
        SetMusicToggle();
        player = GameObject.FindWithTag("Player");
        spaceSize = new SpaceSize();
        gameOver = false;
        score = PlayerPrefs.GetInt("currentScore", 0);
        UpdateScore();
        StartCoroutine(SpawnWaves());
        StartCoroutine(DestroyLevelNumberText());
    }

    private void OnGUI()
    {
        // Счетчик для отображения номера волны врагов (дебаг)
        GUI.Label(new Rect(200, 10, 100, 34), "Wave: " + waveNumber, style);
    }

    // Вначале показывается текст с номером уровня, потом мы его убиваем
    IEnumerator DestroyLevelNumberText()
    {
        yield return new WaitForSeconds(startWait);
        Destroy(levelNumberText);
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        //while (!gameOver)
        //{
            for (int i = 0; i < waves.Length; i++)
            {
                waveNumber = i + 1; // Номер волны врагов

                for (int j = 0; j < waves[i].hazards.Length; j++)
                {
                    GameObject hazard = waves[i].hazards[j];
                    Vector3 halfSize;
                    if (hazard.GetComponentInChildren<MeshFilter>() != null)
                    {
                        // Делаем перемножаем размеры sharedMesh и localScale чтобы получить истинный размер
                        halfSize = Vector3.Scale(hazard.GetComponentInChildren<MeshFilter>().sharedMesh.bounds.extents, hazard.GetComponentInChildren<MeshFilter>().gameObject.transform.localScale);
                    }
                    else if (hazard.GetComponentInChildren<SkinnedMeshRenderer>() != null)
                    {
                        halfSize = hazard.GetComponentInChildren<SkinnedMeshRenderer>().bounds.extents;
                }
                    else
                    {
                        halfSize = Vector3.zero;
                    }
                    Vector3 spawnPosition = new Vector3(Random.Range(spaceSize.bottomLeft.x + halfSize.x, spaceSize.topRight.x - halfSize.x), 0.0f, spaceSize.topRight.z+ halfSize.z);
                    Quaternion spawnRotation = Quaternion.identity;
                    Instantiate(hazard, spawnPosition, spawnRotation);
                    yield return new WaitForSeconds(spawnWait);
                }
                yield return new WaitForSeconds(waveWait);

                if (gameOver)
                {
                    break;
                }
            }
        //}

    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void Rebirth(GameObject player)
    {
        player.transform.position = Vector3.zero;
        player.SetActive(true);
        player.GetComponent<Collider>().enabled = false;
        count = 0;
        InvokeRepeating("Twinkle", 0.12f, 0.12f);
    }

    void Twinkle()
    {
        if(player != null)
        {
            player.SetActive(!player.activeSelf);
        
            if (count == 7)
            {
                player.GetComponent<Collider>().enabled = true;
                CancelInvoke("Twinkle");
            }
            count++;
        }
        
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        if(PlayerPrefs.GetInt("highScore", 0) < score)
        {
            PlayerPrefs.SetInt("highScore", score);
            sceneController.SetHighScoreValue(score);
        }
        pause = true;
        CanvasGroupActivation(gameOverCanvasGroup, true);
        CanvasGroupActivation(pauseButton, false);
        gameOver = true;
    }

    public void RestartGame()
    {
        isShuttingDown = true;
        Time.timeScale = 1f;
        score = PlayerPrefs.GetInt("currentScore", 0);
        CanvasGroupActivation(pauseCanvasGroup, false);
        CanvasGroupActivation(gameOverCanvasGroup, false);
        nextSceneName = SceneManager.GetActiveScene().name;
        sceneController.FadeAndLoadScene(this);
    }

    public void NextLevel()
    {
        isShuttingDown = true;
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("currentScore", score);       
        CanvasGroupActivation(winCanvasGroup, false);
        sceneController.FadeAndLoadScene(this);
    }

    public void LevelCompleted()
    {
        Time.timeScale = 0f;
        if (PlayerPrefs.GetInt("highScore", 0) < score)
        {
            PlayerPrefs.SetInt("highScore", score);
            sceneController.SetHighScoreValue(score);
        }
        pause = true;
        CanvasGroupActivation(winCanvasGroup, true);
        CanvasGroupActivation(pauseButton, false);
    }

    public void MainMenu()
    {
        isShuttingDown = true;
        Time.timeScale = 1f;
        CanvasGroupActivation(pauseCanvasGroup, false);
        CanvasGroupActivation(gameOverCanvasGroup, false);
        nextSceneName = "persistent_scene";
        
        sceneController.FadeAndLoadScene(this);
    }

    public void OnPressPause()
    {
        if (pause == false)
        {
            Time.timeScale = 0f;
            CanvasGroupActivation(pauseCanvasGroup, true);
            pause = true;
        }
        else
        {
            Time.timeScale = 1f;
            CanvasGroupActivation(pauseCanvasGroup, false);
            pause = false;
        }
    }

    public void CanvasGroupActivation(CanvasGroup group, bool turnOn)
    {
        if (turnOn)
        {
            group.alpha = 1f;
            group.interactable = true;
            group.blocksRaycasts = true;
        }
        else
        {
            group.alpha = 0f;
            group.interactable = false;
            group.blocksRaycasts = false;
        }
    }

    public void ToggleSounds()
    {
        audioController.ToggleSounds();
    }

    public void ToggleMusic()
    {
        audioController.ToggleMusic();
    }

    public void SetSoundsToggle()
    {
        if (PlayerPrefs.GetInt("soundsKey", 1) == 1)
        {
            toggleSound.isOn = true;
        }
        else
        {
            toggleSound.isOn = false;
        }
    }

    public void SetMusicToggle()
    {
        if (PlayerPrefs.GetInt("musicKey", 1) == 1)
        {
            toggleMusic.isOn = true;
        }
        else
        {
            toggleMusic.isOn = false;
        }
    }
}