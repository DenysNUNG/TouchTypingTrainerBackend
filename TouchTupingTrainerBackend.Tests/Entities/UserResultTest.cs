using System.Data.Common;
using Moq;
using TouchTypingTrainerBackend.Entities;

namespace TouchTupingTrainerBackend.Tests.Entities
{
    public class UserResultTest
    {
        [Fact]
        public void Map_ShouldReturnMappedUserResult()
        {
            // Arrange
            var rm = new Mock<DbDataReader>();

            var expectedId = 1;
            var expectedAccuracy = 99.20f;
            var expectedSpeed = 250;
            var expectedResultType = false;
            var expectedUserId = "1";
            var expectedTestingMaterialId = 0;
            var expectedExerciseId = 1;

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

            rm.Setup(r => r.GetOrdinal("ResultType"))
                .Returns(3);
            rm.Setup(r => r.GetBoolean(3))
                .Returns(expectedResultType);

            rm.Setup(r => r.GetOrdinal("UserFID"))
                .Returns(4);
            rm.Setup(r => r.GetString(4))
                .Returns(expectedUserId);

            rm.Setup(r => r.GetOrdinal("TestingMaterialFID"))
                .Returns(5);
            rm.Setup(r => r.GetInt32(5))
                .Returns(expectedTestingMaterialId);

            rm.Setup(r => r.GetOrdinal("ExerciseFID"))
                .Returns(6);
            rm.Setup(r => r.GetInt32(6))
                .Returns(expectedExerciseId);

            // Act
            var userResult = UserResult.Map(rm.Object);

            // Assert
            Assert.Equal(expectedId, userResult.UserResult_UID);
            Assert.Equal(expectedAccuracy, userResult.Accuracy);
            Assert.Equal(expectedSpeed, userResult.Speed);
            Assert.Equal(expectedResultType, userResult.ResultType);
            Assert.Equal(expectedUserId, userResult.UserFID);
            Assert.Equal(expectedTestingMaterialId, userResult.TestingMaterialFID);
            Assert.Equal(expectedExerciseId, userResult.ExerciseFID);
        }
    }
}