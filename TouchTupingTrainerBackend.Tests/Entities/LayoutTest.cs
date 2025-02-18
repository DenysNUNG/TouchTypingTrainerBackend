using FluentAssertions;
using Moq;
using System.Data.Common;
using TouchTypingTrainerBackend.Entities;

namespace TouchTupingTrainerBackend.Tests.Entities
{
    public class LayoutTest
    {
        [Fact]
        public void Map_ShouldReturnAMappedLayout()
        {
            // arrange
            var expectedId = 1;
            var expectedName = "qwerty";

            var drMock = new Mock<DbDataReader>();

            drMock.Setup(r => r.GetOrdinal("Layout_UID"))
                .Returns(0);
            drMock.Setup(r => r.GetInt32(0))
                .Returns(expectedId);

            drMock.Setup(r => r.GetOrdinal("Name"))
                .Returns(1);
            drMock.Setup(r => r.GetString(1))
                .Returns(expectedName);

            // act
            var result = Layout.Map(drMock.Object);

            // assert
            Assert.Equal(expectedId, result.LayoutId);
            Assert.Equal(expectedName, result.Name);
        }
    }
}
