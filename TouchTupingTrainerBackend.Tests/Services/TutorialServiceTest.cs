using Moq;
using TouchTypingTrainerBackend.Entities;
using TouchTypingTrainerBackend.Repositories;
using TouchTypingTrainerBackend.Services;
using FluentAssertions;
using FluentAssertions.Equivalency;

namespace TouchTupingTrainerBackend.Tests.Services
{
    public class TutorialServiceTest
    {
        private ITutorialService _tutorialService;
        private Mock<ICourseRepository> _courseRepoMock;
        private Mock<IUserResultRepository> _resultRepoMock;
        private Mock<IProgressRepository> _progressRepoMock;
        private Mock<ILayoutRepository> _layoutRepoMock;

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

        readonly private Exercise _expectedExercise = new Exercise()
        {
            Id = 1,
            Title = "Exercise 1, keys F and J",
            StudySet = "fj jf fj fj jf fj ffjf fjjf jfjj",
            LessonId = 1
        };

        public TutorialServiceTest()
        {
            _courseRepoMock = new Mock<ICourseRepository>();
            _resultRepoMock = new Mock<IUserResultRepository>();
            _progressRepoMock = new Mock<IProgressRepository>();
            _layoutRepoMock = new Mock<ILayoutRepository>();

            _tutorialService = new TutorialService(_courseRepoMock.Object,
                _resultRepoMock.Object,
                _progressRepoMock.Object,
                _layoutRepoMock.Object);
        }

        [Fact]
        public async Task GetCoursesAsync_ShouldReturnACollectionWithCourses()
        {
            // arrange
            _courseRepoMock.Setup(r => r.GetCoursesAsync())
                .ReturnsAsync(_expectedCourses);

            // act
            var result = await _tutorialService.GetCoursesAsync();

            // assert
            _courseRepoMock.Verify(r => r.GetCoursesAsync(), Times.Once());

            result.Should().BeEquivalentTo(_expectedCourses);
        }

        [Fact]
        public async Task GetCourseByIdAsync_WithoutIncludes_ShouldReturnCourseWithoutLessonsAndExercises()
        {
            // arrange
            var courseId = 1;
            var include = false;

            _courseRepoMock.Setup(r => r.GetCourseByIdAsync(courseId, include))
                .ReturnsAsync(_expectedCourse);

            // act
            var result = await _tutorialService.GetCourseByIdAsync(courseId, include);

            // assert
            _courseRepoMock.Verify(r => r.GetCourseByIdAsync(courseId, include), Times.Once);

            result.Should().BeEquivalentTo(_expectedCourse);
        }

        [Fact]
        public async Task GetCourseByIdAsync_WithIncludes_ShouldReturnCourseWithLessonsAndExercises()
        {
            // arrange
            var include = true;
            var courseId = 1;

            _courseRepoMock.Setup(r => r.GetCourseByIdAsync(courseId, include))
                .ReturnsAsync(_expectedCourseWithIncludes);

            // act
            var result = await _tutorialService.GetCourseByIdAsync(courseId, include);

            // assert
            _courseRepoMock.Verify(r => r.GetCourseByIdAsync(courseId, include), Times.Once);

            result.Should().BeEquivalentTo(_expectedCourseWithIncludes);
        }

        [Fact]
        public async Task GetUserLearningResultsAsync_ShouldReturnACollectionWithUserLearningResults()
        {
            // arrange
            var userId = "1";
            var courseId = 1;

            _resultRepoMock.Setup(r => r.GetUserLearningResultsAsync(userId, courseId))
                .ReturnsAsync(_expectedLearningResults);

            // act
            var result = await _tutorialService.GetUserLearningResultsAsync(userId, courseId);

            // assert
            _resultRepoMock.Verify(r => r.GetUserLearningResultsAsync(userId, courseId), Times.Once);

            result.Should().BeEquivalentTo(_expectedLearningResults);
        }

        [Fact]
        public async Task AddUserLearningResultAsync_ShouldAddNewUserLearningResult()
        {
            // arrange
            string userId = "1";

            var learningResult = new LearningResult
            {
                Id = 1,
                Accuracy = 98.50f,
                Speed = 210,
                ExerciseId = 1
            };

            // act
            await _tutorialService.AddUserLearningResultAsync(userId, learningResult);

            // assert
            _resultRepoMock.Verify(r => r.AddUserLearningResultAsync(userId, learningResult), Times.Once);
        }

        [Fact]
        public async Task GetCurrentExerciseAsync_ShouldReturnAnExercise()
        {
            // arrange
            var userId = "1";
            var courseId = 1;

            _progressRepoMock.Setup(r => r.GetCurrentExerciseAsync(userId, courseId))
                .ReturnsAsync(_expectedExercise);

            // act
            var result = await _tutorialService.GetCurrentExerciseAsync(userId, courseId);

            // assert
            _progressRepoMock.Verify(r => r.GetCurrentExerciseAsync(userId, courseId), Times.Once);

            result.Should().BeEquivalentTo(_expectedExercise);
        }

        [Fact]
        public async Task GetUserCoursesAsync_ShouldReturnACollectionWithCourses()
        {
            // arrange
            var userId = "1";

            _progressRepoMock.Setup(r => r.GetUserCoursesAsync(userId))
                .ReturnsAsync(_expectedCourses);

            // act
            var result = await _tutorialService.GetUserCoursesAsync(userId);

            // assert
            _progressRepoMock.Verify(r => r.GetUserCoursesAsync(userId), Times.Once());

            result.Should().BeEquivalentTo(_expectedCourses);
        }

        [Fact]
        public async Task UpsertUserCourseProgressAsync_ShouldUpsertUserCourseProgress()
        {
            // arrange
            var userId = "1";
            var courseId = 1;

            // act
            await _tutorialService.UpsertUserCourseProgressAsync(userId, courseId);

            // assert
            _progressRepoMock.Verify(r => r.UpsertUserCourseProgressAsync(userId, courseId), Times.Once);
        }
    }
}
