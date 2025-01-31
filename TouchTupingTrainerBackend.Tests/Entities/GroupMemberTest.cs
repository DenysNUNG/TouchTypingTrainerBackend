using System.Data.Common;
using Moq;
using TouchTypingTrainerBackend.Entities;

namespace TouchTupingTrainerBackend.Tests.Entities
{
    public class GroupMemberTest
    {
        [Fact]
        public void Map_ShouldReturnMappedGroupAdvisorParticipant()
        {
            // Arrange
            var rm = new Mock<DbDataReader>();

            var expectedId = 1;
            var expectedAdvizorId = "1";
            var expectedParticipantId = "2";
            var expectedGroupId = 1;

            rm.Setup(r => r.GetOrdinal("GroupAdvisorParticipant_UID"))
                .Returns(0);
            rm.Setup(r => r.GetInt32(0))
                .Returns(expectedId);

            rm.Setup(r => r.GetOrdinal("AdvisorUserFID"))
                .Returns(1);
            rm.Setup(r => r.GetString(1))
                .Returns(expectedAdvizorId);

            rm.Setup(r => r.GetOrdinal("ParticipantUserFID"))
                .Returns(2);
            rm.Setup(r => r.GetString(2))
                .Returns(expectedParticipantId);

            rm.Setup(r => r.GetOrdinal("GroupFID"))
                .Returns(3);
            rm.Setup(r => r.GetInt32(3))
                .Returns(expectedGroupId);

            // Act
            var groupAP = GroupMember.Map(rm.Object);

            // Assert
            Assert.Equal(expectedId, groupAP.Id);
            Assert.Equal(expectedAdvizorId, groupAP.AdvisorId);
            Assert.Equal(expectedParticipantId, groupAP.ParticipantId);
            Assert.Equal(expectedGroupId, groupAP.GroupId);
        }
    }
}