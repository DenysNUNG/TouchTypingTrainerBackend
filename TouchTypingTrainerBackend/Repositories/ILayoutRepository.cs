using TouchTypingTrainerBackend.Entities;

namespace TouchTypingTrainerBackend.Repositories
{
    /// <summary>
    /// Layout entity repository.
    /// </summary>
    public interface ILayoutRepository
    {
        /// <summary>
        /// Gets layouts keys related to course.
        /// </summary>
        /// <param name="courseId">Course identifier.</param>
        Task<List<LayoutKey>> GetCourseLayoutKeysAsync(int courseId);

        /// <summary>
        /// Gets all layouts.
        /// </summary>
        Task<List<Layout>> GetAllLayoutsAsync();
    }
}
