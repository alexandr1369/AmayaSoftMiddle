using FluentAssertions;
using NUnit.Framework;

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
    }
}