using System.Security.Claims;

namespace TouchTypingTrainerBackend.Services
{
    /// <summary>
    /// HttpContext user service.
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// HttpContext accessor.
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// DI constructor.
        /// </summary>
        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <inheritdoc/>
        public string? GetUserId()
        {
            return _httpContextAccessor
                .HttpContext
                ?.User
                .FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public string GetUserEmail()
        {
            return _httpContextAccessor
                .HttpContext
                .User
                .FindFirstValue(ClaimTypes.Email);
        }
    }
}
