using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume = 0.5f;
    [Range (0.1f, 3.0f)]
    public float pitch = 1.0f;

    public bool loop = false;

    [HideInInspector]
    public AudioSource source;
}
