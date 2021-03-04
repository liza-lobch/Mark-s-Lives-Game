using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject loadingScreen;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Text progressText;

    [SerializeField]
    private GameObject optionsScreen;

    [SerializeField]
    private GameObject quitScreen;

    [SerializeField]
    private Slider soundSlider, musicSlider;

    private void Start()
    {
        soundSlider.value = PlayerPrefs.GetFloat("soundVolume");
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        PlayerPrefs.SetInt("soundAllowed", 1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (quitScreen.activeSelf == true)
            {
                quitScreen.SetActive(false);
            }
            else if (optionsScreen.activeSelf == true)
            {
                optionsScreen.SetActive(false);
            }
            else
            {
                quitScreen.SetActive(true);
            }
        }
    }

    private void LoadLevelMap()
    {
        SetPosToLastOpenedLevel();
        PlayerPrefs.SetInt("LastScene", 0);
        StartCoroutine(LoadAsynchronously("LevelsMap"));
        PlayerPrefs.SetInt("soundAllowed", 0);
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

    private void ResetProgress()
    {
        PlayerPrefs.SetInt("LastComplLv", 0);
    }

    private void GoToAuthorsSite()
    {
        Application.OpenURL("https://vk.com/id45428430/");
    }

    private void Quit()
    {
        PlayerPrefs.SetInt("soundAllowed", 0);
        Application.Quit();
    }

    private void SetPosToLastOpenedLevel()
    {
        int lastOpenedLevel = PlayerPrefs.GetInt("LastComplLv") + 1;
        PlayerPrefs.SetInt("CurrentLv", lastOpenedLevel);
    }

    private void UpdateSoundVolume()
    {
        Sound[] sounds = FindObjectOfType<AudioManager>().sounds;
        foreach (Sound s in sounds)
        {
            s.source.volume = soundSlider.value;
        }
        PlayerPrefs.SetFloat("soundVolume", soundSlider.value);

        if (PlayerPrefs.GetInt("soundAllowed") == 1)
            FindObjectOfType<AudioManager>().PlaySound("PlayerJump");
    }

    private void UpdateThemeVolume()
    {
        Sound[] themes = FindObjectOfType<AudioManager>().themes;
        foreach (Sound th in themes)
        {
            th.source.volume = musicSlider.value;
        }
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
    }
}
