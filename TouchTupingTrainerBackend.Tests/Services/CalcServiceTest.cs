using TouchTupingTrainerBackend.Tests.Models;
using TouchTypingTrainerBackend.Services;
using FluentAssertions;

namespace TouchTupingTrainerBackend.Tests.Services
{
    public class CalcServiceTest
    {
        readonly private ICalcService _calcService;

        public CalcServiceTest()
        {
            _calcService = new CalcService();
        }

        [Fact]
        public void CalculatePerformance_ShouldReturnUserPerformanceResult()
        {
            // arrange
            var resourse = "tnhaoeu hadeu niaoeu naehu eahodut naoetshu anehu";
            var mistakesCount = 2;
            var duration = 25;

            var expectedResult = new TestResult
            {
                Speed = 117,
                Accuracy = 95.92f
            };

            // act
            TestResult result = _calcService.CalculatePerformance<TestResult>(resourse,
                mistakesCount,
                duration);

            // assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void CalculatePerformance_WithoutMistakes_AccuracyResultShouldBeAThousand()
        {
            // arrange
            var resourse = "tnhaoeu hadeu niaoeu naehu eahodut naoetshu anehu";
            var mistakesCount = 0;
            var duration = 25;

            var expectedAccuracy = 100f;

            // act
            TestResult result = _calcService.CalculatePerformance<TestResult>(resourse,
                mistakesCount,
                duration);

            // assert
            Assert.Equal(expectedAccuracy, result.Accuracy);
        }

        [Fact]
        public void CalculatePerformance_WhenMistakesCountIsMoreThanTheResourseLength_ShouldReturnZeroAccuracyPercent()
        {
            // arrange
            var resourse = "tnhaoeu hadeu niaoeu naehu eahodut naoetshu anehu";
            var mistakesCount = resourse.Length + 1;
            var duration = 25;

            var expectedAccuracy = 0f;

            // act
            TestResult result = _calcService.CalculatePerformance<TestResult>(resourse,
                mistakesCount,
                duration);

            // assert
            Assert.Equal(expectedAccuracy, result.Accuracy);
        }

        [Fact]
        public void CalculatePerformance_WhenLengthIsEmptyOrDurationEqualZero_ShouldReturnEmptyResult()
        {
            // arrange
            var resourse = "";
            var mistakesCount = 2;
            var duration = 0;

            var expectedResult = new TestResult
            {
                Speed = 0,
                Accuracy = 0f
            };

            // act
            TestResult result = _calcService.CalculatePerformance<TestResult>(resourse,
                mistakesCount,
                duration);

            // assert
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
