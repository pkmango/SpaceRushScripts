using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    //public event Action BeforeSceneUnload;
    //public event Action AfterSceneLoad;
    public CanvasGroup faderCanvasGroup;
    public CanvasGroup mainMenuCanvasGroup;
    public CanvasGroup highScoreMenu;
    public GameObject confirmation;
    public Text highScoreValue;
    public float fadeDuration = 1f;
    public string startingSceneName;
    //public string initialStartingPositionName = "DoorToMarket";
    //public SaveData playerSaveData;

    //public AudioMixer soundsMixer;
    //public AudioMixer musicMixer;
    //public Toggle toggleMusic;
    //public Toggle toggleSound;
    //public bool musicOn;
    //public bool soundsOn;

    private bool isFading;

    private void Start()
    {
        Application.targetFrameRate = 60;
        GetComponent<AudioSource>().Play();
        SetHighScoreValue(PlayerPrefs.GetInt("highScore", 0));
        //musicOn = true;
        //soundsOn = true;
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

    //public void ToggleSounds()
    //{
    //    soundsOn = !soundsOn;
    //    if (soundsOn == false)
    //    {
    //        soundsMixer.SetFloat("soundsVolume", -80f);  // Выключаем все звуки
    //    }
    //    else
    //    {
    //        soundsMixer.SetFloat("soundsVolume", 0f); // Включаем все звуки
    //    }
    //}

    //public void ToggleMusic()
    //{
    //    Debug.Log(GetComponent<Toggle>());
    //    musicOn = !musicOn;
    //    if (musicOn == false)
    //    {
    //        musicMixer.SetFloat("musicVolume", -80f);  // Выключаем все звуки
    //    }
    //    else
    //    {
    //        musicMixer.SetFloat("musicVolume", 0f); // Включаем все звуки
    //    }
    //}

    public void OnPressStart()
    {
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
        highScoreMenu.gameObject.SetActive(!highScoreMenu.gameObject.activeInHierarchy);
        confirmation.SetActive(false);
    }

    public void OnPressReset()
    {
        confirmation.SetActive(!confirmation.activeInHierarchy);
    }
    public IEnumerator StartNewGame()
    {
        faderCanvasGroup.alpha = 1f;
        //playerSaveData.Save(PlayerMovement.startingPositionKey, initialStartingPositionName);
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
        //if (BeforeSceneUnload != null)
        //    BeforeSceneUnload();
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

        //if (AfterSceneLoad != null)
        //    AfterSceneLoad();

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
