using System.Data.Common;
using Moq;
using TouchTypingTrainerBackend.Entities;

namespace TouchTupingTrainerBackend.Tests.Entities
{
    public class UserCourseProgressTest
    {
        [Fact]
        public void Map_ShouldReturnMappedUserCourseProgress()
        {
            // Arrange
            var rm = new Mock<DbDataReader>();

            var expectedId = 1;
            var expetedIsCompleted = false;
            var expectedCurrentExerciseId = 1;
            var expectedCurrentLessonId = 1;
            var expectedCourseId = 1;
            var expectedUserId = "1";

            rm.Setup(r => r.GetOrdinal("UserCourseProgress_UID"))
                .Returns(0);
            rm.Setup(r => r.GetInt32(0))
                .Returns(expectedId);

            rm.Setup(r => r.GetOrdinal("IsCompleted"))
                .Returns(1);
            rm.Setup(r => r.GetBoolean(1))
                .Returns(expetedIsCompleted);

            rm.Setup(r => r.GetOrdinal("CurrentExerciseFID"))
                .Returns(2);
            rm.Setup(r => r.GetInt32(2))
                .Returns(expectedCurrentExerciseId);

            rm.Setup(r => r.GetOrdinal("CurrentLessonFID"))
                .Returns(3);
            rm.Setup(r => r.GetInt32(3))
                .Returns(expectedCurrentLessonId);

            rm.Setup(r => r.GetOrdinal("CourseFID"))
                .Returns(4);
            rm.Setup(r => r.GetInt32(4))
                .Returns(expectedCourseId);

            rm.Setup(r => r.GetOrdinal("UserFID"))
                .Returns(5);
            rm.Setup(r => r.GetString(5))
                .Returns(expectedUserId);

            // Act
            var progress = UserCourseProgress.Map(rm.Object);

            // Assert
            Assert.Equal(expectedId, progress.UserCourseProgress_UID);
            Assert.Equal(expetedIsCompleted, progress.IsCompleted);
            Assert.Equal(expectedCurrentExerciseId, progress.CurrentExerciseFID);
            Assert.Equal(expectedCurrentLessonId, progress.CurrentLessonFID);
            Assert.Equal(expectedCourseId, progress.CourseFID);
            Assert.Equal(expectedUserId, progress.UserFID);
        }
    }
}