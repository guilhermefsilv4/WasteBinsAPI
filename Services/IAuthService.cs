using WasteBinsAPI.ViewModel;

namespace WasteBinsAPI.Services
{
    public interface IAuthService
    {
        Task<string?> Authenticate(UserLoginViewModel user);
        Task<bool> ValidateUserCredentialsAsync(string username, string password);
    }
}
