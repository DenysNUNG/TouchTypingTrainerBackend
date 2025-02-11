using System.Data.Common;
using Moq;
using TouchTypingTrainerBackend.Entities;

namespace TouchTupingTrainerBackend.Tests.Entities
{
    public class ResultTest
    {
        [Fact]
        public void Map_ShouldReturnMappedUserResult()
        {
            // Arrange
            var rm = new Mock<DbDataReader>();

            var expectedId = 1;
            var expectedAccuracy = 99.20f;
            var expectedSpeed = 250;

            rm.Setup(r => r.GetOrdinal("UserResult_UID"))
                .Returns(0);
            rm.Setup(r => r.GetInt32(0))
                .Returns(expectedId);

            rm.Setup(r => r.GetOrdinal("Accuracy"))
                .Returns(1);
            rm.Setup(r => r.GetFloat(1))
                .Returns(expectedAccuracy);

            rm.Setup(r => r.GetOrdinal("Speed"))
                .Returns(2);
            rm.Setup(r => r.GetInt32(2))
                .Returns(expectedSpeed);

            // Act
            var userResult = TestingResult.Map(rm.Object);

            // Assert
            Assert.Equal(expectedId, userResult.Id);
            Assert.Equal(expectedAccuracy, userResult.Accuracy);
            Assert.Equal(expectedSpeed, userResult.Speed);
        }
    }
}