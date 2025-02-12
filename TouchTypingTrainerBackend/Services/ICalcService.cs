using TouchTypingTrainerBackend.Models;

namespace TouchTypingTrainerBackend.Services
{
    /// <summary>
    /// Calculation service.
    /// </summary>
    public interface ICalcService
    {
        /// <summary>
        /// Calculates user typing performance.
        /// </summary>
        /// <typeparam name="T">Typing type.</typeparam>
        /// <param name="resourse">Text set resourse.</param>
        /// <param name="mistakesCount">Count of mistakes.</param>
        /// <param name="duration">Typing duration.</param>
        public T CalculatePerformance<T>(string resourse,
            int mistakesCount,
            int duration)
            where T : IUserResult, new();
    }
}
