using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
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
            T result = new T();

            try
            {
                const int MINUTE_SECONDS = 60;
                const float MAX_PERCENT_VALUE = 100f;
                int resourseLength = resourse.Length;

                result.Speed = resourseLength * MINUTE_SECONDS / duration;

                if (!(mistakesCount > resourseLength))
                {
                    float accuracyResult = (resourseLength - mistakesCount) * MAX_PERCENT_VALUE / resourseLength;
                    result.Accuracy = MathF.Round(accuracyResult, 2);
                }
                else
                {
                    result.Accuracy = 0f;
                }
            }
            catch (DivideByZeroException)
            {
                result.Speed = 0;
                result.Accuracy = 0f;
            }
            return result;
        }
    }
}
