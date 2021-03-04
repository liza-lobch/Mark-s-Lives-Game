using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMapMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject loadingScreen;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Text progressText;

    private void Start()
    {
        if (PlayerPrefs.GetInt("LastScene") == 1)
        {
            FindObjectOfType<AudioManager>().StopTheme("LevelTheme");
            FindObjectOfType<AudioManager>().PlayTheme("MenuTheme");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadMainMenu();
        }
    }

    private void LoadScene(string sceneName)
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

    private void LoadMainMenu()
    {
        LoadScene("Menu");
    }

    private void LoadLevel(int levelNumber)
    {
        int lastComplLevel = PlayerPrefs.GetInt("LastComplLv");

        if (levelNumber <= lastComplLevel + 1)
            LoadScene("Level" + levelNumber);
    }
}
