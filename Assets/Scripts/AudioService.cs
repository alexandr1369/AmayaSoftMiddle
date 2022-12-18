using System.Collections.Generic;
using LoadingSystem.Loading.Operations.Home;
using StateSystem;
using StateSystem.UserState;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class AudioService : MonoBehaviour
{
    [field: SerializeField] private AudioSource MusicAudioSource { get; set; }
    [field: SerializeField] private AudioSource SoundAudioSource { get; set; }

    [field: Header("Clips")]
    [field: SerializeField] public AudioClip HomeMusic1Clip { get; private set; }
    [field: SerializeField] public AudioClip HomeMusic2Clip { get; private set; }
    [field: SerializeField] public AudioClip ClickClip { get; private set; }
    [field: SerializeField] public AudioClip InteractionClip { get; private set; }

    public bool IsMusicEnabled { get; private set; }
    public bool IsSoundEnabled { get; private set; }

    private GameController _gameController;
    private GameSettings _gameSettings;
    
    [Inject]
    private void Construct(GameController gameController, HomeSceneLoadingContext context)
    {
        _gameController = gameController;
        _gameSettings = _gameController.State.UserState.GameSettings;
        IsSoundEnabled = _gameSettings.IsSoundEnabled;
        IsMusicEnabled = _gameSettings.IsMusicEnabled;
        context.AudioService = this;
    }

    public void PlayHomeMusic()
    {
        if ((MusicAudioSource.clip == HomeMusic1Clip || MusicAudioSource.clip == HomeMusic2Clip)
            && MusicAudioSource.isPlaying)
        {
            return;
        }
        
        var clips = new List<AudioClip> { HomeMusic1Clip, HomeMusic2Clip };
        var clipIndex = Random.Range(0, clips.Count);
        MusicAudioSource.clip = clips[clipIndex];

        if (!IsMusicEnabled) 
            return;
        
        MusicAudioSource.Play();
    }

    public void PlayLocalFx(AudioSource source, AudioClip clip)
    {
        if(!IsSoundEnabled)
            return;
        
        source.Stop();
        source.clip = clip;
        source.Play();
    }
    
    public void StopLocalFx(AudioSource source) => source.Stop();

    public void PlayGlobalFx(AudioClip clip)
    {
        if(!IsSoundEnabled)
            return;
        
        if(SoundAudioSource.isPlaying)
            SoundAudioSource.Stop();
        
        SoundAudioSource.clip = clip;
        SoundAudioSource.Play();
    }

    public void SetMusicVolume(bool state)
    {
        IsMusicEnabled = state;
        _gameSettings.IsMusicEnabled = IsMusicEnabled;
        _gameController.Save();

        if(!IsMusicEnabled && MusicAudioSource.isPlaying)
            MusicAudioSource.Stop();
        else if(IsMusicEnabled && MusicAudioSource.clip != null)
            MusicAudioSource.Play();
    }

    public void SetSoundVolume(bool state)
    {
        IsSoundEnabled = state;
        _gameSettings.IsSoundEnabled = IsSoundEnabled;
        _gameController.Save();
    }

    public void PreloadHomeAudio()
    {
        ClickClip.LoadAudioData();
    }
}