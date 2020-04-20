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
        }
        
        foreach (SingleSound sound in soundMusic) {
            sound.Source = gameObject.AddComponent<AudioSource>();

            sound.Source.clip = sound.Clip;
            sound.Source.volume = sound.Volume;
            sound.Source.pitch = sound.Pitch;
            sound.Source.loop = sound.Loop;
        }
    }
}
