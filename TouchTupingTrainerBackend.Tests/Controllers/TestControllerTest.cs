using FluentAssertions;
using Moq;
using System.Runtime.CompilerServices;
using TouchTypingTrainerBackend.Controllers;
using TouchTypingTrainerBackend.Entities;
using TouchTypingTrainerBackend.Models;
using TouchTypingTrainerBackend.Services;

namespace TouchTupingTrainerBackend.Tests.Controllers
{
    public class TestControllerTest
    {
        readonly private TestController _controller;

        readonly private Mock<ITestService> _testService;
        readonly private Mock<ICalcService> _calcService;
        readonly private Mock<IUserService> _userService;

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

        readonly private TestingResult _expectedResult = new TestingResult
        {
            Id = 1,
            Accuracy = 97f,
            Speed = 180
        };

        public TestControllerTest()
        {
            _testService = new Mock<ITestService>();
            _calcService = new Mock<ICalcService>();
            _userService = new Mock<IUserService>();

            _controller = new TestController(_testService.Object,
                _calcService.Object,
                _userService.Object);
        }

        [Fact]
        public async Task GetRandomTestingMaterial_ShouldReturnARandomTestingMaterial()
        {
            // arrange
            _testService.Setup(s => s.GetRandomTestingMaterialAsync())
                .ReturnsAsync(_expectedTests.First);

            // act
            var result = await _controller.GetRandomTestingMaterial();

            // assert
            Assert.Contains(result, _expectedTests);

            _testService.Verify(s => s.GetRandomTestingMaterialAsync(), Times.Once);
        }

        [Fact]
        public async Task GetTestingResults_ShouldReturnUserTestingResults()
        {
            // arrange
            var userId = "1";

            _userService.Setup(s => s.GetUserId())
                .Returns(userId);
            _testService.Setup(s => s.GetUserTestingResultsAsync(It.IsAny<string>()))
                .ReturnsAsync(_expectedResults);

            // act
            var result = await _controller.GetTestingResults();

            // assert
            result.Should().BeEquivalentTo(_expectedResults);

            _userService.Verify(s => s.GetUserId(), Times.Once);
            _testService.Verify(s => s.GetUserTestingResultsAsync(It.IsAny<string>()));
        }

        [Fact]
        public async Task CompleteTest_WhenUserIsAuthorized_ShouldCompleteTestAndReturnResult()
        {
            // arrange
            var request = new TestCompleteRequest
            {
                TestingMaterial = new TestingMaterial
                {
                    Id = 1,
                    Text = "huonethu aneusa aoehtudant aosenitheo aonetudh"
                },
                Duration = 35,
                MistakesCount = 1
            };

            var userId = "1";

            _calcService.Setup(s => s.CalculatePerformance<TestingResult>(
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(_expectedResult);
            _userService.Setup(s => s.GetUserId())
                .Returns(userId);

            // act
            var result = await _controller.CompleteTest(request);

            // assert
            result.Should().BeEquivalentTo(_expectedResult);

            _calcService.Verify(s => s.CalculatePerformance<TestingResult>(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
            _userService.Verify(s => s.GetUserId(), Times.Once);
            _testService.Verify(s => s.AddUserTestingResultAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<TestingResult>()), Times.Once);
        }

        [Fact]
        public async Task CompleteTest_WhenUserIsUnauthorized_ShouldReturnResult()
        {
            // arrange
            var request = new TestCompleteRequest
            {
                TestingMaterial = new TestingMaterial
                {
                    Id = 1,
                    Text = "huonethu aneusa aoehtudant aosenitheo aonetudh"
                },
                Duration = 35,
                MistakesCount = 1
            };

            var userId = default(string);

            _calcService.Setup(s => s.CalculatePerformance<TestingResult>(
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(_expectedResult);
            _userService.Setup(s => s.GetUserId())
                .Returns(userId);

            // act
            var result = await _controller.CompleteTest(request);

            // assert
            result.Should().BeEquivalentTo(_expectedResult);

            _calcService.Verify(s => s.CalculatePerformance<TestingResult>(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
            _userService.Verify(s => s.GetUserId(), Times.Once);
            _testService.Verify(s => s.AddUserTestingResultAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<TestingResult>()), Times.Never);
        }
    }
}
