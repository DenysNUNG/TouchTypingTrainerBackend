using FluentAssertions;
using Microsoft.Data.SqlClient;
using Moq;
using System.Data.Common;
using TouchTypingTrainerBackend.Entities;
using TouchTypingTrainerBackend.Helpers;
using TouchTypingTrainerBackend.Repositories;

namespace TouchTupingTrainerBackend.Tests.Repositories
{
    public class CourseRepositoryTest
    {
        [Fact]
        public async Task GetCourses_ShouldReturnCollectionOfCoursesFromDb()
        {
            // arrange
            var sprocName = "dbo.SelectCourseById";
            var parameters = Array.Empty<SqlParameter>();
            var hm = new Mock<ISprocHelper>();
            var rm = new Mock<DbDataReader>();
            var index = -1;

            var expectedCourses = new List<Course>()
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

            var data = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    { "Course_UID", 1 },
                    { "Title", "qwerty" },
                    { "Description", "layout qwerty (english)" }
                },
                new Dictionary<string, object>
                {
                    { "Course_UID", 2 },
                    { "Title", "dvorak" },
                    { "Description", "layout qwerty (dvorak)" }
                },
                new Dictionary<string, object>
                {
                    { "Course_UID", 3 },
                    { "Title", "azerty" },
                    { "Description", "layout qwerty (azerty)" }
                }
            };

            //rm.Setup(r => r.ReadAsync(It.IsAny<CancellationToken>()))
            //    .ReturnsAsync(() => ++index < data.Count);

            //rm.Setup(r => r.HasRows)
            //    .Returns(() => data.Count > 0);

            //rm.Setup(r => r.GetOrdinal(It.IsAny<string>()))
            //    .Returns((string columnName) =>
            //    {
            //        return columnName switch
            //        {
            //            "Course_UID" => 0,
            //            "Title" => 1,
            //            "Description" => 2,
            //            _ => throw new IndexOutOfRangeException()
            //        };
            //    });

            //rm.Setup(r => r.GetInt32(It.IsAny<int>()))
            //    .Returns((int ordinal) => (int)data[index]["Course_UID"]);

            //rm.Setup(r => r.GetString(It.Is<int>(i => i == 1)))
            //    .Returns(() => (string)data[index]["Title"]);

            //rm.Setup(r => r.GetString(It.Is<int>(i => i == 2)))
            //    .Returns(() => (string)data[index]["Description"]);

            //hm.Setup(h => h.ExecuteReaderAsync(sprocName, parameters))
            //    .ReturnsAsync(rm.Object);

            //// act
            //var repo = new CourseRepository(hm.Object);
            //var result = await repo.GetCoursesAsync();

            //// assert
            //result.Should().BeEquivalentTo(expectedCourses);
        }

        [Fact]
        public async Task GetCourseByIdAsync_WithIncludes_ShouldReturnACourseWithLessonsAndExercises()
        {
            // arrange
            var sprocName = "dbo.SelectCourseById";
            var hm = new Mock<ISprocHelper>();
            var rm = new Mock<DbDataReader>();
            var index = -1;

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@CourseId", 1),
                new SqlParameter("@IncludeLessonsWithExercises", true)
            };

            var expectedCourse = new Course()
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

            var coursesSet = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    { "Course_UID", 1 },
                    { "Title", "qwerty" },
                    { "Description", "layout qwerty (english)" }
                }
            };

            var LessonsSet = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    { "Lesson_UID", 1 },
                    { "Lesson_Title", "Index keys" },
                    { "Description",  "This lesson will help you to master base ASDF JKL: keys" },
                    { "CourseFID", 1 },
                    { "Exercise_UID", 1 },
                    { "Exercise_Title", "Exercise 1, keys F and J" },
                    { "StudySet", "fj jf fj fj jf fj ffjf fjjf jfjj" },
                    { "LessonFID", 1 }
                }
            };                         

            // act

            // assert
        }

        [Fact]
        public async Task GetCourseByIdAsync_WithoutIncludes_ShouldReturnACourseWithLessonsAndExercises()
        {
            // arrange
            var expectedCourse = new Course()
            {
                Id = 1,
                Title = "qwerty",
                Description = "layout qwerty (english)"
            };

            // act

            // assert
        }
    }
}
