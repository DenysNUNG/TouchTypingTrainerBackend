using Moq;
using System.Data.Common;
using TouchTypingTrainerBackend.Entities;

namespace TouchTupingTrainerBackend.Tests.Entities
{
    public class CourseTest
    {
        [Fact]
        public void Map_ShouldReturnMappedCourse()
        {
            // arrange
            var rm = new Mock<DbDataReader>();

            var expectedId = 1;
            var expectedTitle = "qwerty";
            var expectedDescription = "layout qwerty (english)";
            var expectedLayoutId = 1;

            rm.Setup(r => r.GetOrdinal("Course_UID"))
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

            rm.Setup(r => r.GetOrdinal("LayoutFID"))
                .Returns(3);
            rm.Setup(r => r.GetInt32(3))
                .Returns(expectedLayoutId);

            // act
            var course = Course.Map(rm.Object);

            // assert
            Assert.Equal(expectedId, course.Id);
            Assert.Equal(expectedTitle, course.Title);
            Assert.Equal(expectedDescription, course.Description);
            Assert.Equal(expectedLayoutId, course.LayoutId);
        }
    }
}
