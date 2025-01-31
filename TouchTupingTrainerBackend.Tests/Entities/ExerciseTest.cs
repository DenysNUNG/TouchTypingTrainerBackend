using Moq;
using System.Data.Common;
using TouchTypingTrainerBackend.Entities;

namespace TouchTupingTrainerBackend.Tests.Entities
{
    public class ExerciseTest
    {
        [Fact]
        public void Map_ShouldReturnMappedExercise()
        {
            // Arrange
            var rm = new Mock<DbDataReader>();

            var expectedId = 1;
            var expectedTitle = "Exercise 1, keys F and J";
            var expectedStudySet = "fj jf fj fj jf fj ffjf fjjf jfjj";
            var expectedLessonId = 1;

            rm.Setup(r => r.GetOrdinal("Exercise_UID"))
                .Returns(0);
            rm.Setup(r => r.GetInt32(0))
                .Returns(expectedId);

            rm.Setup(r => r.GetOrdinal("Title"))
                .Returns(1);
            rm.Setup(r => r.GetString(1))
                .Returns(expectedTitle);

            rm.Setup(r => r.GetOrdinal("StudySet"))
                .Returns(2);
            rm.Setup(r => r.GetString(2))
                .Returns(expectedStudySet);

            rm.Setup(r => r.GetOrdinal("LessonFID"))
                .Returns(3);
            rm.Setup(r => r.GetInt32(3))
                .Returns(expectedLessonId);

            // Act
            var exercise = Exercise.Map(rm.Object);

            // Assert
            Assert.Equal(expectedId, exercise.Id);
            Assert.Equal(expectedTitle, exercise.Title);
            Assert.Equal(expectedStudySet, exercise.StudySet);
            Assert.Equal(expectedLessonId, exercise.LessonId);
        }
    }
}
