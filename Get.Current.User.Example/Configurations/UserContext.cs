using Get.Current.User.Example.Configurations.Abstractions;
using Get.Current.User.Example.Extensions;

namespace Get.Current.User.Example.Configurations
{
    public sealed class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
    {
        public bool IsAuthenticated =>
            httpContextAccessor
            .HttpContext?
            .User
            .Identity?
            .IsAuthenticated ?? false;

        public Guid UserId =>
            httpContextAccessor
            .HttpContext?
            .User
            .GetUserId() ?? throw new ApplicationException();

        public string Username =>
            httpContextAccessor
            .HttpContext?
            .User
            .GetUsername() ?? throw new ApplicationException();
    }
}
