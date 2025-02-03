using Moq;
using TouchTypingTrainerBackend.Entities;
using TouchTypingTrainerBackend.Helpers;
using TouchTypingTrainerBackend.Repositories;

namespace TouchTupingTrainerBackend.Tests.Repositories
{
    public class CourseRepositoryTest
    {
        readonly private Mock<SprocHelper> _hm;

        readonly private Course _targetCourseWithoutInclude;
        readonly private Course _targetCourseWithInclude;
        readonly private List<Course> _targetCourses;

        public CourseRepositoryTest()
        {
            _hm = new Mock<SprocHelper>();

            _targetCourseWithoutInclude = new Course()
            {
                Id = 1,
                Title = "qwerty",
                Description = "layout qwerty (english)"
            };

            _targetCourseWithInclude = new Course()
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

            _targetCourses = new List<Course>()
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
        }

        [Fact]
        public async Task GetCourses_ShouldReturnCollectionOfCoursesFromDb()
        {
            // arrange

            // act

            // assert
        }
    }
}
