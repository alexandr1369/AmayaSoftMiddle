using FluentAssertions;
using Location.Character;
using NUnit.Framework;
using UnityEngine;

namespace Editor.Tests
{
    public class CharactersInteractServiceTests
    {
        [Test]
        public void CharactersInteractServiceSimplePasses()
        {
            // Act
            var service = new GameObject().AddComponent<CharactersInteractService>();

            // Arrange
            service.Add(new GameObject().AddComponent<Character>());
            service.Add(new GameObject().AddComponent<Character>());
            
            // Assert
            service.CharactersCount.Should().Be(2);
        }
    }
}
