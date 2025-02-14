using TouchTypingTrainerBackend.Entities;

namespace TouchTypingTrainerBackend.Models
{
    /// <summary>
    /// Request model for completing test.
    /// </summary>
    public class TestCompleteRequest
    {
        /// <summary>
        /// Completed test.
        /// </summary>
        public TestingMaterial TestingMaterial { get; set; }

        /// <summary>
        /// The number of typo mistakes.
        /// </summary>
        public int MistakesCount { get; set; }

        /// <summary>
        /// The duration of typing in seconds.
        /// </summary>
        public int Duration { get; set; }
    }
}
