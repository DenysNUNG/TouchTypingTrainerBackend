using Microsoft.AspNetCore.Identity;
using System.Data.Common;

namespace TouchTypingTrainerBackend.Entities
{
    /// <summary>
    /// Lnk group entity for advisors and participants.
    /// </summary>
    public class GroupMember
    {
        /// <summary>
        /// Lnk group advisor participant identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Lnk group advisor identifier.
        /// </summary>
        public string AdvisorId { get; set; }

        /// <summary>
        /// Lnk group advisor.
        /// </summary>
        public IdentityUser Advisor { get; set; }

        /// <summary>
        /// Lnk group participant identifier.
        /// </summary>
        public string ParticipantId { get; set; }

        /// <summary>
        /// Lnk group participant.
        /// </summary>
        public IdentityUser Participant { get; set; }

        /// <summary>
        /// Lnk group identifier.
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// Lnk group.
        /// </summary>
        public Group Group { get; set; }

        /// <summary>
        /// Maps a lnk group advisor participant.
        /// </summary>
        /// <param name="dr">A reader.</param>
        /// <returns>Mapped GroupAdvisorParticipant.</returns>
        public static GroupMember Map(DbDataReader dr)
        {
            return new GroupMember
            {
                Id = dr.GetInt32(dr.GetOrdinal("GroupAdvisorParticipant_UID")),
                AdvisorId = dr.GetString(dr.GetOrdinal("AdvisorUserFID")),
                ParticipantId = dr.GetString(dr.GetOrdinal("ParticipantUserFID")),
                GroupId = dr.GetInt32(dr.GetOrdinal("GroupFID"))
            };
        }
    }
}
