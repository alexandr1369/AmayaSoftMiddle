using FluentAssertions;
using LoadingSystem.Loading.Operations.Home;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace Editor.Tests
{
    public class ConveyorTapeTests
    {
        [Test]
        public void WhenCreatingConveyorTapeItem_AndInitializingItWithBonusInit_ThenIsBonusStateShouldBeTrue()
        {
            // Arrange
            var conveyorTapeItem = Setup.TapeItem();

            // Act
            conveyorTapeItem.InitBonus(conveyorTapeItem.Config.BonusSprite);
            
            // Assert
            conveyorTapeItem.IsBonus.Should().Be(true);
        }

        [Test]
        public void WhenStartingConveyorTape_ThenItShouldBeActive()
        {
            // Arrange
            var conveyorTape = Create.Tape();

            // Act
            conveyorTape.StartConveyorTape();

            // Assert
            conveyorTape.IsActive.Should().Be(true);
        }

        [Test]
        public void WhenDraggingConveyorTapeItemDraggable_AndContextIsNull_ThenPointerPositionShouldBeNull()
        {
            // Arrange
            var itemDraggable = Create.ItemDraggable();

            // Act
            itemDraggable.Context = null;
            itemDraggable.OnDrag(null);

            // Assert
            itemDraggable.PointerPosition.Should().Be(null);
        }

        [Test]
        public void WhenPlayingConveyorTapeItemInteractionAnimationFor3Times_ThenAudioServicePlayLocalFxReceivedCallsShouldBe3()
        {
            // Arrange
            var conveyorTapeItem = Setup.TapeItem();
            var audioService = Setup.AudioServiceInterface();
            var gameItems = Create.GameItems();
            var homeSceneContext = new HomeSceneLoadingContext();
            conveyorTapeItem.Construct(
                gameItems,
                audioService, 
                homeSceneContext);

            // Act
            conveyorTapeItem.PlayInteractionAnimation(default);
            conveyorTapeItem.PlayInteractionAnimation(default);
            conveyorTapeItem.PlayInteractionAnimation(default);
            
            // Assert
            audioService.Received(3).PlayLocalFx(Arg.Any<AudioSource>(), Arg.Any<AudioClip>());
        }
        
        [Test]
        public void WhenPlayingConveyorTapeItemDraggingAnimationFor4Times_ThenAudioServicePlayLocalFxReceivedCallsShouldBe0()
        {
            // Arrange
            var conveyorTapeItem = Setup.TapeItem();
            var audioService = Setup.AudioServiceInterface();
            var gameItems = Create.GameItems();
            var homeSceneContext = new HomeSceneLoadingContext();
            conveyorTapeItem.Construct(
                gameItems,
                audioService, 
                homeSceneContext);

            // Act
            conveyorTapeItem.PlayDraggingAnimation(default);
            conveyorTapeItem.PlayDraggingAnimation(default);
            conveyorTapeItem.PlayDraggingAnimation(default);
            conveyorTapeItem.PlayDraggingAnimation(default);
            
            // Assert
            audioService.Received(0).PlayLocalFx(Arg.Any<AudioSource>(), Arg.Any<AudioClip>());
        }
    }
}