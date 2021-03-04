using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private int levelNumber;
    private bool isComplete;

    [SerializeField]
    private GameObject loadingScreen;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Text progressText;

    [SerializeField]
    private GameObject optionsMenuScreen;
    private bool isPaused;

    [SerializeField]
    private GameObject toLevelMapScreen, losingScreen, startScreen, endScreen;

    private void Start()
    {
        isPaused = false;
        Time.timeScale = 1;

        FindObjectOfType<AudioManager>().StopTheme("MenuTheme");
        FindObjectOfType<AudioManager>().PlayTheme("LevelTheme");

        if (PlayerPrefs.GetInt("RestartLevel") == 0)
        {
            startScreen.SetActive(true);
            PlayerPrefs.SetInt("RestartLevel", 1);
            GamePause();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (toLevelMapScreen.activeSelf == true)
            {
                toLevelMapScreen.SetActive(false);
            }
            else if (optionsMenuScreen.activeSelf == true)
            {
                toLevelMapScreen.SetActive(true);
            }
            else if (losingScreen.activeSelf == false)
            {
                optionsMenuScreen.SetActive(true);
                GamePause();
            }
        }
    }

    private void OnApplicationPause(bool focus)
    {
        if (!focus)
        {
            isPaused = true;
            Time.timeScale = 0;
            optionsMenuScreen.SetActive(true);
        }
    }

    public void GamePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0;
            CrossPlatformInputManager.SetAxis("Vertical", 0);
            CrossPlatformInputManager.SetAxis("Horizontal", 0);
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void RestartLevel()
    {
        LoadScene("Level" + levelNumber);
    }

    public void EndLevel(bool isCompl)
    {
        isComplete = isCompl;
        int LastComplLevel = PlayerPrefs.GetInt("LastComplLv");
        if (isComplete && levelNumber > LastComplLevel)
        {
            PlayerPrefs.SetInt("LastComplLv", levelNumber);
        }
        PlayerPrefs.SetInt("CurrentLv", levelNumber);
        PlayerPrefs.SetInt("LastScene", 1);
        LoadScene("LevelsMap");
        PlayerPrefs.SetInt("RestartLevel", 0);
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadAsynchronously(sceneName));
    }

    private IEnumerator LoadAsynchronously(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            progressText.text = (int)(progress * 100f) + "%";
            yield return null;
        }
    }

    public void Losing()
    {
        Invoke("LoadLosingScreen", 1.2f);
    }

    private void LoadLosingScreen()
    {
        Time.timeScale = 0;
        losingScreen.SetActive(true);
    }

    public void Ending()
    {
        Invoke("LoadEndScreen", 1.2f);
    }
    private void LoadEndScreen()
    {
        Time.timeScale = 0;
        endScreen.SetActive(true);
    }
}
