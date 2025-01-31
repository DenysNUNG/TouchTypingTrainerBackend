using Moq;
using System.Data.Common;
using TouchTypingTrainerBackend.Entities;

namespace TouchTupingTrainerBackend.Tests.Entities
{
    public class TestingMaterialTest
    {
        [Fact]
        public void Map_ShouldReturnMappedTestingMaterial()
        {
            // Arrange
            var rm = new Mock<DbDataReader>();

            var expectedId = 1;
            var expectedText = "This text to will help you find out your accuracy and speed"; ;

            rm.Setup(r => r.GetOrdinal("TestingMaterial_UID"))
                .Returns(0);
            rm.Setup(r => r.GetInt32(0))
                .Returns(expectedId);

            rm.Setup(r => r.GetOrdinal("Text"))
                .Returns(1);
            rm.Setup(r => r.GetString(1))
                .Returns(expectedText);
            // Act
            var testingMaterial = TestingMaterial.Map(rm.Object);

            // Assert
            Assert.Equal(expectedId, testingMaterial.Id);
            Assert.Equal(expectedText, testingMaterial.Text);
        }
    }
}
