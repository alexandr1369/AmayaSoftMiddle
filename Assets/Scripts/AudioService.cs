using System;
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
    [field: SerializeField] public AudioClip BarnUnlockClip { get; private set; }
    [field: SerializeField] public AudioClip ClickClip { get; private set; }
    [field: SerializeField] public AudioClip UpgradeClip { get; private set; }
    [field: SerializeField] public AudioClip ChickenClip { get; private set; }
    [field: SerializeField] public AudioClip CowClip { get; private set; }
    [field: SerializeField] public AudioClip PlantingClip { get; private set; }
    [field: SerializeField] public AudioClip WateringClip { get; private set; }
    [field: SerializeField] public AudioClip HarvestingClip { get; private set; }
    [field: SerializeField] public AudioClip Swing1Clip { get; private set; }
    [field: SerializeField] public AudioClip Swing2Clip { get; private set; }
    [field: SerializeField] public AudioClip CoinCollectClip { get; private set; }
    [field: SerializeField] public AudioClip TickClip { get; private set; }
    [field: SerializeField] public AudioClip TravelClip { get; private set; }
    [field: SerializeField] public AudioClip TreeShakingClip { get; private set; }

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
        BarnUnlockClip.LoadAudioData();
        ClickClip.LoadAudioData();
        UpgradeClip.LoadAudioData();
        ChickenClip.LoadAudioData();
        CowClip.LoadAudioData();
        PlantingClip.LoadAudioData();
        WateringClip.LoadAudioData();
        HarvestingClip.LoadAudioData();
        Swing1Clip.LoadAudioData();
        Swing2Clip.LoadAudioData();
        CoinCollectClip.LoadAudioData();
        TickClip.LoadAudioData();
        TravelClip.LoadAudioData();
        TreeShakingClip.LoadAudioData();
    }
}