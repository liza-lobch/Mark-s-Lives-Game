using UnityEngine;
using System;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Slider soundSlider, musicSlider;

    public Sound[] themes;
    public Sound[] sounds;

    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        SetSoundsVolume(themes);
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SetSoundsVolume(sounds);
        soundSlider.value = PlayerPrefs.GetFloat("soundVolume");
    }

    void Start()
    {
        FindObjectOfType<AudioManager>().PlayTheme("MenuTheme");

        if (PlayerPrefs.GetInt("FirstLoad") == 0)
        {
            PlayerPrefs.SetFloat("soundValue", 0.8f);
            soundSlider.value = 0.8f;
            PlayerPrefs.SetFloat("musicValue", 0.5f);
            musicSlider.value = 0.5f;
            PlayerPrefs.SetInt("FirstLoad", 1);
        }
    }

    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void StopSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }
        s.source.Stop();
    }

    public void PlayTheme(string name)
    {
        Sound th = Array.Find(themes, theme => theme.name == name);
        if (th == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }
        th.source.Play();
    }

    public void StopTheme(string name)
    {
        Sound th = Array.Find(themes, theme => theme.name == name);
        if (th == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }
        th.source.Stop();
    }

    private void SetSoundsVolume(Sound[] sounds)
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
        }
    }
}
