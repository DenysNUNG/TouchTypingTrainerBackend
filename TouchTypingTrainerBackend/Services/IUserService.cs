namespace TouchTypingTrainerBackend.Services
{
    /// <summary>
    /// HttpContext user service.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Gets current user identifier.
        /// </summary>
        string? GetUserId();

        string GetUserEmail();
    }
}
