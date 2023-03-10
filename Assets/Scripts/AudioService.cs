using System.Collections.Generic;
using LoadingSystem.Loading.Operations.Home;
using StateSystem;
using StateSystem.UserState;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using Random = UnityEngine.Random;

public class AudioService : MonoBehaviour, IAudioService
{
    [field: SerializeField] public AudioSource MusicAudioSource { get; set; }
    [field: SerializeField] public AudioSource SoundAudioSource { get; set; }

    [field: Header("Clips")]
    [field: SerializeField] public AudioClip HomeMusic1Clip { get; set; }
    [field: SerializeField] public AudioClip HomeMusic2Clip { get; set; }
    [field: SerializeField] public AudioClip ClickClip { get; set; }
    [field: SerializeField] public AudioClip InteractionClip { get; set; }
    
    public IGameController GameController { get; set; }
    public IGameSettings GameSettings { get; set; }
    public int PlayedLocalFxCount { get; set; }
    public bool IsMusicEnabled { get; private set; }
    public bool IsSoundEnabled { get; private set; }
    
    [Inject]
    public void Construct(IGameController gameController, HomeSceneLoadingContext context)
    {
        GameController = gameController;
        GameSettings = GameController.State.UserState.GameSettings;
        IsSoundEnabled = GameSettings.IsSoundEnabled;
        IsMusicEnabled = GameSettings.IsMusicEnabled;
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
        
        PlayedLocalFxCount++;
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
        GameSettings.IsMusicEnabled = IsMusicEnabled;
        GameController.Save();

        if(!IsMusicEnabled && MusicAudioSource.isPlaying)
            MusicAudioSource.Stop();
        else if(IsMusicEnabled && MusicAudioSource.clip != null)
            MusicAudioSource.Play();
    }

    public void SetSoundVolume(bool state)
    {
        IsSoundEnabled = state;
        GameSettings.IsSoundEnabled = IsSoundEnabled;
        GameController.Save();
    }

    public void PreloadHomeAudio()
    {
        ClickClip.LoadAudioData();
        InteractionClip.LoadAudioData();
    }
}

public interface IAudioService
{
    AudioSource MusicAudioSource { get; }
    AudioSource SoundAudioSource { get; }
    AudioClip HomeMusic1Clip { get; }
    AudioClip HomeMusic2Clip { get; }
    AudioClip ClickClip { get; }
    AudioClip InteractionClip { get; }
    IGameController GameController { get; }
    IGameSettings GameSettings { get; }
    int PlayedLocalFxCount { get; }
    bool IsMusicEnabled { get; }
    bool IsSoundEnabled { get; }
    void PlayHomeMusic();
    void PlayLocalFx(AudioSource source, AudioClip clip);
    void StopLocalFx(AudioSource source);
    void PlayGlobalFx(AudioClip clip);
    void SetMusicVolume(bool state);
    void SetSoundVolume(bool state);
    void PreloadHomeAudio();
}