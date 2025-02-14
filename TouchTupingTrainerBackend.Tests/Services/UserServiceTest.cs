using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;
using TouchTypingTrainerBackend.Services;

namespace TouchTupingTrainerBackend.Tests.Services
{
    public class UserServiceTest
    {
        readonly private IUserService _userService;
        readonly private Mock<IHttpContextAccessor> _contextAccessorMock;

        public UserServiceTest()
        {
            _contextAccessorMock = new Mock<IHttpContextAccessor>();
            _userService = new UserService(_contextAccessorMock.Object);
        }

        [Fact]
        public void GetUserId_WithAutorizedUser_ShouldReturnUserId()
        {
            // arrange
            var userId = "1";

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId)
            };

            var identity = new ClaimsIdentity(claims);

            var claimsPrincipal = new ClaimsPrincipal(identity);

            var httpContextMock = new DefaultHttpContext
            {
                User = claimsPrincipal
            };

            _contextAccessorMock.Setup(c => c.HttpContext)
                .Returns(httpContextMock);

            // act
            var result = _userService.GetUserId();

            // assert
            _contextAccessorMock.Verify(c => c.HttpContext, Times.Once());

            Assert.Equal(userId, result);
        }

        [Fact]
        public void GetUserId_WithoutAutorizedUser_ShouldReturnNull()
        {
            // arrange
            string userId = null;
            HttpContext? nullContext = null;

            _contextAccessorMock.Setup(c => c.HttpContext)
                .Returns(nullContext);

            // act
            var result = _userService.GetUserId();

            // assert
            _contextAccessorMock.Verify(c => c.HttpContext, Times.Once());

            Assert.Equal(userId, result);
        }
    }
}
