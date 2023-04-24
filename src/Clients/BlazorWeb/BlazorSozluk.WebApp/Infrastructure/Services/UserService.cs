using BlazorSozluk.Common.Events.User;
using BlazorSozluk.Common.Infrastructure.Exceptions;
using BlazorSozluk.Common.Infrastructure.Results;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.WebApp.Infrastructure.Services.Interfaces;
using System.ComponentModel;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorSozluk.WebApp.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserDetailViewModel> GetUserDetail(Guid? id)
        {
            var userDetail = await _httpClient.GetFromJsonAsync<UserDetailViewModel>($"api/user/{id}");
            return userDetail;
        }
        public async Task<UserDetailViewModel> GetUserDetail(string userName)
        {
            var userDetail = await _httpClient.GetFromJsonAsync<UserDetailViewModel>($"api/user/userName/{userName}");
            return userDetail;
        }

        public async Task<bool> UpdateUser(UserDetailViewModel user)
        {
            var res = await _httpClient.PostAsJsonAsync($"api/user/update", user);
            return res.IsSuccessStatusCode;
        }

        public async Task<bool> ChangeUserPassword(string oldPassword, string newPassword)
        {
            var command = new ChangeUserPasswordCommand(Guid.Empty, oldPassword, newPassword);
            var httpResponse = await _httpClient.PostAsJsonAsync($"api/user/ChangePassword", command);

            if (httpResponse is not null && !httpResponse.IsSuccessStatusCode)
            {
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var responseStr = await httpResponse.Content.ReadAsStringAsync();
                    var validation = JsonSerializer.Deserialize<ValidationResponseModel>(responseStr);
                    responseStr = validation.FlattenErrors;
                    throw new DatabaseValidationException(responseStr);
                }
                return false;
            }
            return httpResponse.IsSuccessStatusCode;
        }
    }
}
