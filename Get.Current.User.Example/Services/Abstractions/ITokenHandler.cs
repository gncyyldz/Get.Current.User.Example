using Get.Current.User.Example.Modals;

namespace Get.Current.User.Example.Services.Abstractions
{
    public interface ITokenHandler
    {
        Token CreateAccessToken();
    }
}
