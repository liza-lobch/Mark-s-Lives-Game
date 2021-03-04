using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SocialPlatforms;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    public bool loop;

    [HideInInspector]
    public float volume;

    [HideInInspector]
    public AudioSource source;
}
