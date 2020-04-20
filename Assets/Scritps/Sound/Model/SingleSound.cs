using System;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class SingleSound
{
    [Header("Sound data")] 
    [SerializeField] private string name;
    [SerializeField] private AudioClip clip;

    [Header("Sound properties")]
    [SerializeField] [Range(0f, 1f)] private float volume = 1;
    [SerializeField] [Range(0.1f, 3f)]private float pitch = 0.1f;
    [SerializeField] private bool loop;
    
    
    private AudioSource source;

    public string Name => name;

    public AudioClip Clip => clip;

    public float Pitch => pitch;

    public float Volume => volume;

    public bool Loop => loop;

    public AudioSource Source
    {
        get => source;
        set => source = value;
    }
}
