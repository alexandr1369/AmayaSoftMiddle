using Location.ConveyorTape.Item;
using NSubstitute;
using StateSystem;
using StateSystem.UserState;
using UnityEngine;

namespace Editor.Tests
{
    public static class Setup
    {
        public static ConveyorTapeItem TapeItem()
        {
            var conveyorTapeItem = new GameObject().AddComponent<ConveyorTapeItem>();
            var config = Create.ConveyorTapeItemConfig();
            conveyorTapeItem.Config = config;
            conveyorTapeItem.SpriteRenderer = conveyorTapeItem.gameObject.AddComponent<SpriteRenderer>();

            return conveyorTapeItem;
        }

        public static AudioService AudioService()
        {
            var audioService = new GameObject().AddComponent<AudioService>();
            var gameController = GameController();
            var gameSettings = Substitute.For<IGameSettings>();
            var audioClip = Create.AudioClip();
            var audioSource = Create.AudioSource();
            audioService.GameController = gameController; 
            audioService.GameSettings = gameSettings;
            audioService.MusicAudioSource = audioSource;
            audioService.SoundAudioSource = audioSource;
            audioService.HomeMusic1Clip = audioClip;
            audioService.HomeMusic2Clip = audioClip;
            audioService.ClickClip = audioClip;
            audioService.InteractionClip = audioClip;
            
            return audioService;
        }

        public static IGameController GameController()
        {
            var gameController = Substitute.For<IGameController>();
            var gameState = new GameState();
            // gameState.UserState.GameSettings.IsMusicEnabled = false;
            gameController.State.Returns(gameState);
            // gameController.State.UserState.Returns(new UserState());
            // gameController.State.UserState.GameSettings.Returns(new GameSettings());

            return gameController;
        }
    }
}