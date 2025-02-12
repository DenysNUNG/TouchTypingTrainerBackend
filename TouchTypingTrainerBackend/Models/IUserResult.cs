namespace TouchTypingTrainerBackend.Models
{
    /// <summary>
    /// User result.
    /// </summary>
    public interface IUserResult
    {
        /// <summary>
        /// Result accuracy.
        /// </summary>
        public float Accuracy { get; set; }

        /// <summary>
        /// Result speed.
        /// </summary>
        public int Speed { get; set; }
    }
}
