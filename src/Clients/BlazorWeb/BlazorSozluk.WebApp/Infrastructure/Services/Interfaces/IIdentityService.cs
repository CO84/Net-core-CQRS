using BlazorSozluk.Common.Models.RequestModels;

namespace BlazorSozluk.WebApp.Infrastructure.Services.Interfaces
{
    public interface IIdentityService
    {
        bool isLoggedIn { get; }

        Guid GetUserId();
        string GetUserName();
        string GetUserToken();
        Task<bool> Login(LoginUserCommand command);
        void Logout();
    }
}