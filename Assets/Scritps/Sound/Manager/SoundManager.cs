using System;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private SingleSound[] soundEffects;
    [SerializeField] private SingleSound[] soundMusic;

    private void Awake()
    {
        foreach (SingleSound sound in soundEffects) {
            sound.Source = gameObject.AddComponent<AudioSource>();

            sound.Source.clip = sound.Clip;
            sound.Source.volume = sound.Volume;
            sound.Source.pitch = sound.Pitch;
            sound.Source.loop = sound.Loop;
            sound.Source.playOnAwake = false;
        }
        
        foreach (SingleSound sound in soundMusic) {
            sound.Source = gameObject.AddComponent<AudioSource>();

            sound.Source.clip = sound.Clip;
            sound.Source.volume = sound.Volume;
            sound.Source.pitch = sound.Pitch;
            sound.Source.loop = sound.Loop;
            sound.Source.playOnAwake = false;
        }
    }

    private void Start()
    {
        SetDelegates();
    }

    private void SetDelegates()
    {
        GyrussEventManager.SoundEffectPlayInitiated += PlaySoundEffect;
        GyrussEventManager.SoundBGMPlayInitiated += PlayBGM;
        GyrussEventManager.SoundBGMStopInitiated += StopBGM;
        GyrussEventManager.CurrentSoundBGMStopInitiated += StopCurrentPlayingBGM;
        GyrussEventManager.IsBGMPlayingInitiated += IsBGMPlaying;
    }

    private void PlaySoundEffect(string effectName)
    {
        if (soundEffects.Any(s => s.Name == effectName)) {
            Array.Find(soundEffects, sound => sound.Name == effectName).Source.Play();
        }
        else {
            Debug.LogError("No effect with name: " + effectName + " to play!");
        }
    }

    private void PlayBGM(string BGMName)
    {
        if (soundMusic.Any(s => s.Name == BGMName)) {
            Array.Find(soundMusic, sound => sound.Name == BGMName).Source.Play();
        }
        else {
            Debug.LogError("No music with name: " + BGMName + " to play!");
        }
    }

    private void StopBGM(string BGMName)
    {
        if (soundMusic.Any(s => s.Name == BGMName)) {
            Array.Find(soundMusic, sound => sound.Name == BGMName).Source.Stop();
        }
        else {
            Debug.LogError("No music with name: " + BGMName + " to stop!");
        }
    }
    
    //TODO: add silencing method to 0 to silence before warping

    private void StopCurrentPlayingBGM()
    {
        foreach (SingleSound sound in soundMusic) {
            if (sound.Source.isPlaying) {
                sound.Source.Stop();
            }
        }
    }

    private bool IsBGMPlaying()
    {
        return soundMusic.Any(sound => sound.Source.isPlaying);
    }
    
    
}
