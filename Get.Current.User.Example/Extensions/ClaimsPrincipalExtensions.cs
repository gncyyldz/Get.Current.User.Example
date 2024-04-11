using System.Security.Claims;

namespace Get.Current.User.Example.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal? claimsPrincipal)
        {
            string? userId = claimsPrincipal?.FindFirstValue("UserId");
            return Guid.TryParse(userId, out Guid parsedUserId) ? parsedUserId : throw new ApplicationException();
        }

        public static string GetUsername(this ClaimsPrincipal? claimsPrincipal)
        {
            string? username = claimsPrincipal?.FindFirstValue(ClaimTypes.NameIdentifier);
            return username;
        }
    }
}
