using TouchTypingTrainerBackend.Models;

namespace TouchTypingTrainerBackend.Services
{
    /// <summary>
    /// Calculation service.
    /// </summary>
    public class CalcService : ICalcService
    {
        /// <inheritdoc/>
        public T CalculatePerformance<T>(string resourse,
            int mistakesCount,
            int duration) 
            where T : IUserResult, new()
        {
            int setLength = resourse.Length;

            return new T
            {
                Accuracy = (setLength - mistakesCount) * 100 / setLength,
                Speed = setLength * 60 / duration
            };
        }
    }
}
