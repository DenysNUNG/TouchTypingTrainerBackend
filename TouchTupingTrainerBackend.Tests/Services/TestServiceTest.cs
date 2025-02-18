using FluentAssertions;
using Moq;
using TouchTypingTrainerBackend.Entities;
using TouchTypingTrainerBackend.Repositories;
using TouchTypingTrainerBackend.Services;

namespace TouchTupingTrainerBackend.Tests.Services
{
    public class TestServiceTest
    {
        readonly private ITestService _testService;
        readonly private Mock<ITestRepository> _testRepoMock;
        readonly private Mock<IUserResultRepository> _userRepoMock;

        readonly private List<TestingMaterial> _expectedTests = new List<TestingMaterial>
        {
            new TestingMaterial
            {
                Id = 1,
                Text = "toeu grocg oneu oe8rg"
            },
            new TestingMaterial
            {
                Id = 2,
                Text = "euie euio a.p, eidue"
            },
            new TestingMaterial
            {
                Id = 3,
                Text = "gr,cg.p ;oqenuh 'g.p /'g.pusa r.cgp"
            }
        };

        readonly private List<TestingResult> _expectedResults = new List<TestingResult>
        {
            new TestingResult
            {
                Id = 1,
                Accuracy = 97f,
                Speed = 180
            },
            new TestingResult
            {
                Id = 2,
                Accuracy = 78.50f,
                Speed = 280
            },
            new TestingResult
            {
                Id = 3,
                Accuracy = 98.75f,
                Speed = 300
            }
        };

        public TestServiceTest()
        {
            _testRepoMock = new Mock<ITestRepository>();
            _userRepoMock = new Mock<IUserResultRepository>();

            _testService = new TestService(_testRepoMock.Object,
                _userRepoMock.Object);
        }

        [Fact]
        public async Task GetRandomTestingMaterialAsync_ShouldReturnARandomTestingMaterialSet()
        {
            // arrange
            var layoutId = 1;

            _testRepoMock.Setup(r => r.GetTestingMaterialsAsync(layoutId))
                .ReturnsAsync(_expectedTests);

            // act
            var result = await _testService.GetRandomTestingMaterialAsync(layoutId);

            // assert
            _testRepoMock.Verify(r => r.GetTestingMaterialsAsync(layoutId), Times.Once);

            Assert.Contains(result, _expectedTests);
        }

        [Fact]
        public async Task GetUserTestingResultsAsync_ShouldReturnUserTestingResults()
        {
            // arrange
            var userId = "1";

            _userRepoMock.Setup(r => r.GetUserTestingResultsAsync(userId))
                .ReturnsAsync(_expectedResults);

            // act
            var result = await _testService.GetUserTestingResultsAsync(userId);

            // assert
            _userRepoMock.Verify(r => r.GetUserTestingResultsAsync(userId), Times.Once);

            result.Should().BeEquivalentTo(_expectedResults);
        }

        [Fact]
        public async Task AddUserTestingResultAsync_ShouldAddUserTestingResult()
        {
            // arrange
            var userId = "1";
            var testId = 1;
            var testingResult = new TestingResult
            {
                Id = 1,
                Accuracy = 78.22f,
                Speed = 180
            };

            // act
            await _testService.AddUserTestingResultAsync(userId, testId, testingResult);

            // assert
            _userRepoMock.Verify(r => r.AddUserTestingResultAsync(userId, testId, testingResult), Times.Once);
        }
    }
}
