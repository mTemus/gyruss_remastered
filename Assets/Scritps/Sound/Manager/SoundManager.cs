using System;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private SingleSound[] soundEffects = null;
    [SerializeField] private SingleSound[] soundMusic = null;


    private SingleSound currentPlayingBGM;
    
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
        GyrussEventManager.CurrentPlayingBGMSilencingInitiated += SilencePlayingBGM;
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
            currentPlayingBGM = Array.Find(soundMusic, sound => sound.Name == BGMName);
            currentPlayingBGM.Source.Play();
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
    
    private void SilencePlayingBGM()
    {
        currentPlayingBGM.Source.volume -= 0.05f;

        if (currentPlayingBGM.Source.volume <= 0) {
            currentPlayingBGM.Source.Stop();
            currentPlayingBGM = null;
        }
        else {
            GyrussGameManager.Instance.SetConditionInTimer("BGMSilencing", true);
        }
    }
    
    
    private void StopCurrentPlayingBGM()
    {
        currentPlayingBGM.Source.Stop();
        currentPlayingBGM = null;
    }

    private bool IsBGMPlaying()
    {
        return currentPlayingBGM != null && currentPlayingBGM.Source.isPlaying;
    }
}
