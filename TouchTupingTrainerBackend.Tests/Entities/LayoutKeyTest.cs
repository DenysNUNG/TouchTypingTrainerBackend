using FluentAssertions;
using Moq;
using System.Data.Common;
using TouchTypingTrainerBackend.Entities;

namespace TouchTupingTrainerBackend.Tests.Entities
{
    public class LayoutKeyTest
    {
        [Fact]
        public void Map_ShouldReturnAMappedLayoutKey()
        {
            // arrange
            var expectedIndex = 1;
            var expectedValue = 'c';
            var expectedShiftValue = 'C';

            var value = "c";
            var shiftValue = "C";

            var drMock = new Mock<DbDataReader>();

            drMock.Setup(r => r.GetOrdinal("KeyFID"))
                .Returns(0);
            drMock.Setup(r => r.GetInt32(0))
                .Returns(expectedIndex);

            drMock.Setup(r => r.GetOrdinal("Value"))
                .Returns(1);
            drMock.Setup(r => r.GetString(1))
                .Returns(value);

            drMock.Setup(r => r.GetOrdinal("ShiftValue"))
                .Returns(2);
            drMock.Setup(r => r.GetString(2))
                .Returns(shiftValue);

            // act
            var result = LayoutKey.Map(drMock.Object);

            // assert
            Assert.Equal(expectedIndex, result.Index);
            Assert.Equal(expectedValue, result.Value);
            Assert.Equal(expectedShiftValue, result.ShiftValue);
        }
    }
}
