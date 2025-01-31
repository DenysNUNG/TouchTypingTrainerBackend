using Moq;
using System.Data.Common;
using TouchTypingTrainerBackend.Entities;

namespace TouchTupingTrainerBackend.Tests.Entities
{
    public class GroupTest
    {
        [Fact]
        public void Map_ShouldReturnMappedGroup()
        {
            // Arrange
            var rm = new Mock<DbDataReader>();

            var expectedId = 1;
            var expectedName = "Test group";

            rm.Setup(r => r.GetOrdinal("Group_UID"))
                .Returns(0);
            rm.Setup(r => r.GetInt32(0))
                .Returns(expectedId);

            rm.Setup(r => r.GetOrdinal("Name"))
                .Returns(1);
            rm.Setup(r => r.GetString(1))
                .Returns(expectedName);

            // Act
            var group = Group.Map(rm.Object);

            // Assert
            Assert.Equal(expectedId, group.Id);
            Assert.Equal(expectedName, group.Name);
        }
    }
}
