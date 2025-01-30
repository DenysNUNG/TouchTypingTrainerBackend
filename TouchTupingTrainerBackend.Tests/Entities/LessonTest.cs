using Moq;
using System.Data.Common;
using TouchTypingTrainerBackend.Entities;

namespace TouchTupingTrainerBackend.Tests.Entities
{
    public class LessonTest
    {
        [Fact]
        public void Map_ShouldReturnMappedLesson()
        {
            // Arrange
            var rm = new Mock<DbDataReader>();

            var expectedId = 1;
            var expectedTitle = "Index keys";
            var expectedDescription = "This lesson will help you to master base ASDF JKL: keys";
            var expectedCourseId = 1;

            rm.Setup(r => r.GetOrdinal("Lesson_UID"))
                .Returns(0);
            rm.Setup(r => r.GetInt32(0))
                .Returns(expectedId);

            rm.Setup(r => r.GetOrdinal("Title"))
                .Returns(1);
            rm.Setup(r => r.GetString(1))
                .Returns(expectedTitle);

            rm.Setup(r => r.GetOrdinal("Description"))
                .Returns(2);
            rm.Setup(r => r.GetString(2))
                .Returns(expectedDescription);

            rm.Setup(r => r.GetOrdinal("CourseFID"))
                .Returns(3);
            rm.Setup(r => r.GetInt32(3))
                .Returns(expectedCourseId);

            // Act
            var lesson = Lesson.Map(rm.Object);

            // Assert
            Assert.Equal(expectedId, lesson.Lesson_UID);
            Assert.Equal(expectedTitle, lesson.Title);
            Assert.Equal(expectedDescription, lesson.Description);
            Assert.Equal(expectedCourseId, lesson.CourseFID);
        }
    }
}
