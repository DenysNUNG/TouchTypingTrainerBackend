using Moq;
using TouchTypingTrainerBackend.Controllers;
using TouchTypingTrainerBackend.Entities;
using TouchTypingTrainerBackend.Services;
using FluentAssertions;
using TouchTypingTrainerBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace TouchTupingTrainerBackend.Tests.Controllers
{
    public class TutorialControllerTest
    {
        readonly private TutorialController _controller;

        readonly private Mock<ITutorialService> _tutorService;
        readonly private Mock<ICalcService> _calcService;
        readonly private Mock<IUserService> _userService;

        readonly private List<Course> _expectedCourses = new List<Course>()
        {
            new Course()
            {
                Id = 1,
                Title = "qwerty",
                Description = "layout qwerty (english)",
            },
            new Course()
            {
                Id = 2,
                Title = "dvorak",
                Description = "layout qwerty (dvorak)",
            },
            new Course()
            {
                Id = 3,
                Title = "azerty",
                Description = "layout qwerty (azerty)",
            }
        };

        readonly private Course _expectedCourseWithIncludes = new Course()
        {
            Id = 1,
            Title = "qwerty",
            Description = "layout qwerty (english)",
            Lessons = new List<Lesson>()
                {
                    new Lesson()
                    {
                        Id = 1,
                        Title = "Index keys",
                        Description = "This lesson will help you to master base ASDF JKL: keys",
                        CourseId = 1,
                        Exercises = new List<Exercise>()
                        {
                            new Exercise()
                            {
                                Id = 1,
                                Title = "Exercise 1, keys F and J",
                                StudySet = "fj jf fj fj jf fj ffjf fjjf jfjj",
                                LessonId = 1
                            }
                        }
                    }
                }
        };

        readonly private Course _expectedCourse = new Course()
        {
            Id = 1,
            Title = "qwerty",
            Description = "layout qwerty (english)"
        };

        readonly private Exercise _expectedExercise = new Exercise()
        {
            Id = 1,
            Title = "Exercise 1, keys F and J",
            StudySet = "fj jf fj fj jf fj ffjf fjjf jfjj",
            LessonId = 1
        };

        readonly private List<LearningResult> _expectedLearningResults = new List<LearningResult>()
        {
            new LearningResult
            {
                Id = 1,
                Accuracy = 98.50f,
                Speed = 210,
                ExerciseId = 1
            },
            new LearningResult
            {
                Id = 2,
                Accuracy = 85f,
                Speed = 179,
                ExerciseId = 2
            },
            new LearningResult
            {
                Id = 1,
                Accuracy = 100f,
                Speed = 330,
                ExerciseId = 3
            }
        };

        readonly private LearningResult _expectedLearningResult = new LearningResult
        {
            Id = 1,
            Accuracy = 98.50f,
            Speed = 210,
            ExerciseId = 1
        };

        public TutorialControllerTest()
        {
            _tutorService = new Mock<ITutorialService>();
            _calcService = new Mock<ICalcService>();
            _userService = new Mock<IUserService>();

            _controller = new TutorialController(_tutorService.Object,
                _calcService.Object,
                _userService.Object);
        }

        [Fact]
        public async Task GetCourses_SholdReturnAListWithCourses()
        {
            // arrange
            _tutorService.Setup(s => s.GetCoursesAsync())
                .ReturnsAsync(_expectedCourses);

            // act
            var result = await _controller.GetCourses();

            // assert
            result.Should().BeEquivalentTo(_expectedCourses);

            _tutorService.Verify(s => s.GetCoursesAsync(), Times.Once);
        }

        [Fact]
        public async Task GetCourseWithIncludes_ShouldReturnCourseWithLessonsAndExercisesById()
        {
            // arrange
            var courseId = 1;
            var include = true;

            _tutorService.Setup(s => s.GetCourseByIdAsync(courseId, include))
                .ReturnsAsync(_expectedCourseWithIncludes);

            // act
            var result = await _controller.GetCourseWithIncludes(courseId);

            // assert
            result.Should().BeEquivalentTo(_expectedCourseWithIncludes);

            _tutorService.Verify(s => s.GetCourseByIdAsync(courseId, include), Times.Once);
        }

        [Fact]
        public async Task GetCourse_ShouldReturnACourseById()
        {
            // arrange
            var courseId = 1;
            var include = false;

            _tutorService.Setup(s => s.GetCourseByIdAsync(courseId, include))
                .ReturnsAsync(_expectedCourse);

            // act
            var result = await _controller.GetCourse(courseId);

            // assert
            result.Should().BeEquivalentTo(_expectedCourse);

            _tutorService.Verify(s => s.GetCourseByIdAsync(courseId, include), Times.Once);
        }

        [Fact]
        public async Task GetLearningResults_ShouldReturnAListWithUserLearningResults()
        {
            // arrange
            var courseId = 1;
            var userId = "1";

            _userService.Setup(s => s.GetUserId())
                .Returns(userId);

            _tutorService.Setup(s => s.GetUserLearningResultsAsync(It.IsAny<string>(), courseId))
                .ReturnsAsync(_expectedLearningResults);

            // act
            var result = await _controller.GetLearningResults(courseId);

            // assert
            result.Should().BeEquivalentTo(_expectedLearningResults);

            _userService.Verify(s => s.GetUserId(), Times.Once);
            _tutorService.Verify(s => s.GetUserLearningResultsAsync(It.IsAny<string>(), courseId), Times.Once);
        }

        [Fact]
        public async Task GetCurrentExercise_ShouldReturnCurrentUserExercise()
        {
            // arrange
            var courseId = 1;
            var userId = "1";

            _userService.Setup(s => s.GetUserId())
                .Returns(userId);
            _tutorService.Setup(s => s.GetCurrentExerciseAsync(It.IsAny<string>(), courseId))
                .ReturnsAsync(_expectedExercise);

            // act
            var result = await _controller.GetCurrentUserExercise(courseId);

            // assert
            result.Should().BeEquivalentTo(_expectedExercise);

            _userService.Verify(s => s.GetUserId(), Times.Once);
            _tutorService.Verify(s => s.GetCurrentExerciseAsync(It.IsAny<string>(), courseId), Times.Once);
        }

        [Fact]
        public async Task CompleleExercise_ShouldCompleteAnExerciseAndReturnResult()
        {
            // arrange
            var request = new ExerciseCompleteRequest
            {
                Exercise = new Exercise
                {
                    Id = 1,
                    StudySet = "oethu oetuif naosencu",
                    Title = "Title",
                    LessonId = 1
                },
                Duration = 35,
                MistakesCount = 1,
                CourseId = 1
            };

            _calcService.Setup(s => s.CalculatePerformance<LearningResult>(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(_expectedLearningResult);

            // act
            var resourse = await _controller.CompleteExercise(request);

            // assert
            resourse.Should().BeEquivalentTo(_expectedLearningResult);

            _calcService.Verify(s => s.CalculatePerformance<LearningResult>(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
            _userService.Verify(s => s.GetUserId(), Times.Once);
            _tutorService.Verify(s => s.AddUserLearningResultAsync(It.IsAny<string>(), It.IsAny<LearningResult>()), Times.Once);
            _tutorService.Verify(s => s.UpsertUserCourseProgressAsync(It.IsAny<string>(), It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task RegisterUserCourse_ShouldRegisterUserCourseAndReturnOkResult()
        {
            // arrange
            var courseId = 1;

            // act
            var result = await _controller.RegisterUserCourse(courseId);

            // assert
            result.Should().BeOfType<OkResult>();

            _userService.Verify(s => s.GetUserId(), Times.Once);
            _tutorService.Verify(s => s.UpsertUserCourseProgressAsync(It.IsAny<string>(), It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task GetUserCourses_ShouldGetAllUserCourses()
        {
            // arrange
            var userId = "1";

            _userService.Setup(s => s.GetUserId())
                .Returns(userId);
            _tutorService.Setup(s => s.GetUserCoursesAsync(It.IsAny<string>()))
                .ReturnsAsync(_expectedCourses);

            // act
            var result = await _controller.GetUserCourses();

            // assert
            result.Should().BeEquivalentTo(_expectedCourses);

            _userService.Verify(s => s.GetUserId(), Times.Once);
            _tutorService.Verify(s => s.GetUserCoursesAsync(It.IsAny<string>()), Times.Once);
        }
    }
}
