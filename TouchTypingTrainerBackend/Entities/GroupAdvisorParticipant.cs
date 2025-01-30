using Microsoft.AspNetCore.Identity;
using System.Data.Common;

namespace TouchTypingTrainerBackend.Entities
{
    /// <summary>
    /// Lnk group entity for advisor and participants.
    /// </summary>
    public class GroupAdvisorParticipant
    {
        /// <summary>
        /// Lnk group advisor participant identifier.
        /// </summary>
        public int GroupAdvisorParticipant_UID { get; set; }

        /// <summary>
        /// Lnk group advisor identifier.
        /// </summary>
        public string AdvisorUserFID { get; set; }

        /// <summary>
        /// Lnk group advisor.
        /// </summary>
        public IdentityUser Advisor { get; set; }

        /// <summary>
        /// Lnk group participant identifier.
        /// </summary>
        public string ParticipantUserFID { get; set; }

        /// <summary>
        /// Lnk group participant.
        /// </summary>
        public IdentityUser Participant { get; set; }

        /// <summary>
        /// Lnk group identifier.
        /// </summary>
        public int GroupFID { get; set; }

        /// <summary>
        /// Lnk group.
        /// </summary>
        public Group Group { get; set; }

        /// <summary>
        /// Maps a lnk group advisor participant.
        /// </summary>
        /// <param name="dr">A reader.</param>
        /// <returns>Mapped GroupAdvisorParticipant.</returns>
        public static GroupAdvisorParticipant Map(DbDataReader dr)
        {
            return new GroupAdvisorParticipant
            {
                GroupAdvisorParticipant_UID = dr.GetInt32(dr.GetOrdinal("GroupAdvisorParticipant_UID")),
                AdvisorUserFID = dr.GetString(dr.GetOrdinal("AdvisorUserFID")),
                ParticipantUserFID = dr.GetString(dr.GetOrdinal("ParticipantUserFID")),
                GroupFID = dr.GetInt32(dr.GetOrdinal("GroupFID"))
            };
        }
    }
}
