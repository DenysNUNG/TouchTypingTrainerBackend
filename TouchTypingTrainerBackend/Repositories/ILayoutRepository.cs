using TouchTypingTrainerBackend.Entities;

namespace TouchTypingTrainerBackend.Repositories
{
    public interface ILayoutRepository
    {
        Task<List<LayoutKey>> GetCourseLayoutKeysAsync(int courseId);

        Task<List<Layout>> GetAllLayoutsAsync();
    }
}
