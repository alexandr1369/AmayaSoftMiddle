using FluentAssertions;
using LoadingSystem.Loading.Operations.Home;
using NUnit.Framework;

namespace Editor.Tests
{
    public class AudioServiceTests
    {
        [Test]
        public void WhenDisablingMusicVolume_ThenIsSoundEnabledStateShouldBeFalse()
        {
            // Arrange
            var audioService = Setup.AudioService();

            // Act
            audioService.SetMusicVolume(false);

            // Assert
            audioService.IsMusicEnabled.Should().Be(false);
        }
        
        [Test]
        public void WhenEnablingSoundVolume_ThenIsMusicEnabledStateShouldBeTrue()
        {
            // Arrange
            var audioService = Setup.AudioService();

            // Act
            audioService.SetSoundVolume(true);

            // Assert
            audioService.IsSoundEnabled.Should().Be(true);
        }

        [Test]
        public void WhenPlayingLocalFx_AndSoundIsDisabled_ThenSourceClipShouldBeNull()
        {
            // Arrange
            var audioService = Setup.AudioService();
            var audioSource = Create.AudioSource();

            // Act
            audioService.SetSoundVolume(false);
            audioService.PlayLocalFx(audioSource, audioService.ClickClip);

            // Assert
            audioSource.clip.Should().Be(null);
        }

        [Test]
        public void WhenStoppingLocalFx_ThenSourceShouldNotBePlaying()
        {
            // Arrange
            var audioService = Setup.AudioService();
            var audioSource = Create.AudioSource();

            // Act
            audioService.PlayLocalFx(audioSource, audioService.ClickClip);
            audioService.StopLocalFx(audioSource);

            // Assert
            audioSource.isPlaying.Should().Be(false);
        }

        [Test]
        public void WhenPlayingGlobalFxWithEnabledSoundVolume_AndDisablingSoundVolume_ThenSoundAudioSourceIsPlayingShouldBeTrue()
        {
            // Arrange
            var audioService = Setup.AudioService();

            // Act
            audioService.SetSoundVolume(true);
            audioService.PlayGlobalFx(audioService.ClickClip);
            audioService.SetSoundVolume(false);

            // Assert
            audioService.SoundAudioSource.isPlaying.Should().Be(true);
        }

        [Test]
        public void WhenPlayingLocalFx_AndSoundIsEnabled_ThenSourceClipShouldNotBeNull()
        {
            // Arrange
            var audioService = Setup.AudioService();
            var audioSource = Create.AudioSource();

            // Act
            audioService.SetSoundVolume(true);
            audioService.PlayLocalFx(audioSource, audioService.ClickClip);

            // Assert
            audioSource.clip.Should().NotBe(null);
        }

        [Test]
        public void WhenPlayingHomeMusic_ThenMusicAudioSourceClipShouldNotBeNull()
        {
            // Arrange
            var audioService = Setup.AudioService();

            // Act
            audioService.PlayHomeMusic();

            // Assert
            audioService.MusicAudioSource.clip.Should().NotBe(null);
        }

        [Test]
        public void WhenInjectingDataIntoConstructMethod_ThenInjectableHomeSceneContextAudioServiceFieldShouldNotBeNull()
        {
            // Arrange
            var audioService = Setup.AudioService();
            var gameController = Setup.GameController();
            var homeSceneLoadingContext = new HomeSceneLoadingContext();
            
            // Act
            audioService.Construct(gameController, homeSceneLoadingContext);
            
            // Assert
            homeSceneLoadingContext.AudioService.Should().NotBe(null);
        }
    }
}