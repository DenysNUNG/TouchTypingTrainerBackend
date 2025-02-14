using TouchTypingTrainerBackend.Entities;

namespace TouchTypingTrainerBackend.Models
{
    /// <summary>
    /// Request model for completing exercise
    /// </summary>
    public class ExerciseCompleteRequest
    {
        /// <summary>
        /// Completed exercise.
        /// </summary>
        public Exercise Exercise { get; set; }

        /// <summary>
        /// Related course identifier to the exercise.
        /// </summary>
        public int CourseId { get; set; }

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
