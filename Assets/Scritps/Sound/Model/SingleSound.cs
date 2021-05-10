using System;
using UnityEngine;

[Serializable]
public class SingleSound
{
    [Header("Sound data")] 
    [SerializeField] private string name = null;
    [SerializeField] private AudioClip clip = null;

    [Header("Sound properties")]
    [SerializeField] [Range(0f, 1f)] private float volume = 1f;
    [SerializeField] [Range(-3f, 3f)]private float pitch = 1f;
    [SerializeField] private bool loop = false;
    
    
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
