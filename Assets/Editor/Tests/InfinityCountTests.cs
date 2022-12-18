using FluentAssertions;
using NUnit.Framework;

namespace Editor.Tests
{
    public class InfinityCountTests
    {
        [Test]
        public void WhenCreatingInfinityCountWithRate0_0015K_ThenOutputShouldBe1()
        {
            // Arrange
            var count = new InfinityCount(.0015f, 3);

            // Assert
            count.ToString().Should().Be("1");
        }
        
        [Test]
        public void WhenCreatingInfinityCountWithRate999_9K_ThenOutputShouldBe999K()
        {
            // Arrange
            var count = new InfinityCount(999.9f, 3);

            // Assert
            count.ToString().Should().Be("999K");
        }

        [Test]
        public void WhenCreatingInfinityCountWithRate10M_AndRemovingFromItInfinityCountWithRate100KFor99Times_ThenOutputShouldBe100K()
        {
            // Arrange
            var count1 = new InfinityCount(10, 6);
            var count2 = new InfinityCount(100, 3);
            
            // Act
            for(var i = 0; i < 99; i++)
                count1 -= count2;
            
            // Assert
            count1.ToString().Should().Be("100K");
        }

        [Test]
        public void WhenCreatingInfinityCountWith10M_AndRemovingFromItInfinityCountWithRate100KFor99999Times_ThenOutputShouldBe100K()
        {
            // Arrange
            var count1 = new InfinityCount(100, 3);
            var count2 = new InfinityCount(10, 9);

            // Act
            for (var i = 0; i < 99999; i++)
                count2 -= count1;
            
            // Assert
            count2.ToString().Should().Be("100K");
        }

        [Test]
        public void WhenCreatingInfinityCountWithRate1250000000M_ThenOutputShouldBe1_25AA()
        {
            // Arrange
            var count = new InfinityCount(1250000000, 6);

            // Assert
            count.ToString().Should().Be("1.25AA");
        }

        [Test]
        public void WhenCreatingInfinityCountWithRate60B_AndDividingItBy6000_ThenOutputShouldBe10B()
        {
            // Arrange
            var count = new InfinityCount(60, 12);

            // Act
            count /= 6000;

            // Assert
            count.ToString().Should().Be("10B");
        }

        [Test]
        public void WhenCreatingInfinityCountWithRate10B_AndMultiplyingItBy100_ThenOutputShouldBe1T()
        {
            // Arrange
            var count = new InfinityCount(10, 9);

            // Act
            count *= 100;

            // Assert
            count.ToString().Should().Be("1T");
        }

        [Test]
        public void WhenCreatingInfinityCountWithRate50B_AndCheckingForLessOrEqualValueThanInfinityCountWithRate900M_ThenOutputShouldBeFalse()
        {
            // Arrange
            var count1 = new InfinityCount(50, 12);
            var count2 = new InfinityCount(900, 6);

            // Act
            var expression = count1 <= count2;

            // Assert
            expression.Should().Be(false);
        }

        [Test]
        public void WhenCreatingInfinityCountWithRate1_1K_AndRemovingFromIt10For11Times_ThenOutputShouldBe990()
        {
            // Arrange
            var count = new InfinityCount(1.1f, 3);

            // Act
            for(var i = 0; i < 11; i++)
                count -= 10;

            // Assert
            count.ToString().Should().Be("990");
        }

        [Test]
        public void WhenCreatingInfinityCountWithRate990_AndAddingToIt1For110010Times_ThenOutputShouldBe111K()
        {
            // Arrange
            var count = new InfinityCount(990);

            // Act
            for(var i = 0; i < 11001; i++)
                for(var j = 0; j < 10; j++)
                    count++;

            // Assert
            count.ToString().Should().Be("111K");
        }
    }
}
