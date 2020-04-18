using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public CanvasGroup faderCanvasGroup;
    public CanvasGroup mainMenuCanvasGroup;
    public CanvasGroup highScoreMenu;
    public CanvasGroup levelSelectionMenu;
    public GameObject confirmation;
    public Text highScoreValue;
    public float fadeDuration = 1f;
    public LevelButtonController[] levelButtonControllers;

    private string startingSceneName = "level_1";
    private bool isFading;
    

    private void Start()
    {
        Application.targetFrameRate = 60;
        GetComponent<AudioSource>().Play();
        SetHighScoreValue(PlayerPrefs.GetInt("highScore", 0));
        highScoreMenu.gameObject.SetActive(false);
        levelSelectionMenu.gameObject.SetActive(false);
        PlayerPrefs.SetInt("level_5", 1);
    }

    public void SetHighScoreValue(int score)
    {
        highScoreValue.text = score.ToString();
    }

    public void ResetHighScore()
    {
        PlayerPrefs.SetInt("highScore", 0);
        highScoreValue.text = "0";
        confirmation.SetActive(false);
    }

    public void OnClickResetLevels()
    {
        foreach (LevelButtonController i in levelButtonControllers)
        {
            PlayerPrefs.SetInt(i.levelName, 0);
            i.gameObject.GetComponent<Button>().interactable = false;
            i.openState.SetActive(false);
            i.lockState.SetActive(true);
        }
    }

    public void OnPressChooseLevel()
    {
        foreach(LevelButtonController i in levelButtonControllers)
        {
            if (PlayerPrefs.GetInt(i.levelName, 0) == 1)
            {
                i.gameObject.GetComponent<Button>().interactable = true;
                i.openState.SetActive(true);
                i.lockState.SetActive(false);
            }
        }
        CavasSetActive(levelSelectionMenu);
    }

    public void CavasSetActive(CanvasGroup canvasGroup)
    {
        canvasGroup.gameObject.SetActive(!canvasGroup.gameObject.activeInHierarchy);
    }

    public void OnPressStart(string sceneName)
    {
        startingSceneName = sceneName;
        levelSelectionMenu.gameObject.SetActive(false);
        GetComponent<AudioSource>().Stop();
        mainMenuCanvasGroup.alpha = 0f;
        mainMenuCanvasGroup.interactable = false;
        mainMenuCanvasGroup.blocksRaycasts = false;
        PlayerPrefs.SetInt("currentScore", 0);
        StartCoroutine(StartNewGame());
    }

    public void OnPressExit()
    {
        Application.Quit();
    }

    public void OnPressHighScores()
    {
        //highScoreMenu.gameObject.SetActive(!highScoreMenu.gameObject.activeInHierarchy);
        CavasSetActive(highScoreMenu);
        confirmation.SetActive(false);
    }

    public void OnPressReset()
    {
        confirmation.SetActive(!confirmation.activeInHierarchy);
    }

    //public void OnPressLevelButton(string sn)
    //{
    //    startingSceneName = sn;
    //    Debug.Log(startingSceneName);
    //}
    public IEnumerator StartNewGame()
    {
        faderCanvasGroup.alpha = 1f;
        yield return StartCoroutine(LoadSceneAndSetActive(startingSceneName));
        StartCoroutine(Fade(0f));
    }

    public void FadeAndLoadScene(GameController sceneReaction)
    {
        if (!isFading)
        {
            StartCoroutine(FadeAndSwitchScenes(sceneReaction.nextSceneName));
        }
    }
    private IEnumerator FadeAndSwitchScenes(string sceneName)
    {
        yield return StartCoroutine(Fade(1f));

        yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);

        // Если хотим вернуться в главное меню, то загружать новую сцену не нужно, делаем persisrent_scene активной
        if(sceneName == "persistent_scene")
        {
            yield return SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
            mainMenuCanvasGroup.alpha = 1f;
            mainMenuCanvasGroup.interactable = true;
            mainMenuCanvasGroup.blocksRaycasts = true;
            GetComponent<AudioSource>().Play();
        }
        else
        {
            yield return StartCoroutine(LoadSceneAndSetActive(sceneName));
        }

        yield return StartCoroutine(Fade(0f));
    }
    private IEnumerator LoadSceneAndSetActive(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        Scene newlyLoadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newlyLoadedScene);
    }
    private IEnumerator Fade(float finalAlpha)
    {
        isFading = true;
        faderCanvasGroup.blocksRaycasts = true;
        float fadeSpeed = Mathf.Abs(faderCanvasGroup.alpha - finalAlpha) / fadeDuration;
        while (!Mathf.Approximately(faderCanvasGroup.alpha, finalAlpha))
        {
            faderCanvasGroup.alpha = Mathf.MoveTowards(faderCanvasGroup.alpha, finalAlpha,
                fadeSpeed * Time.deltaTime);
            yield return null;
        }
        isFading = false;
        faderCanvasGroup.blocksRaycasts = false;
    }
}
