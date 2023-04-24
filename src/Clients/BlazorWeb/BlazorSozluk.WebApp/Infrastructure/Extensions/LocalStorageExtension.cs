using Blazored.LocalStorage;

namespace BlazorSozluk.WebApp.Infrastructure.Extensions
{
    public static class LocalStorageExtension
    {
        public const string TokenName = "token";
        public const string UserName = "userName";
        public const string UserId = "userId";

        public static bool IsUserLoggedIn(this ISyncLocalStorageService syncLocalStorage)
        {
            return !string.IsNullOrEmpty(GetToken(syncLocalStorage));
        }

        public static string GetUserName(this ISyncLocalStorageService syncLocalStorage)
        {
            return syncLocalStorage.GetItem<string>(UserName);
        }

        public static async Task<string> GetUserName(this ILocalStorageService syncLocalStorage)
        {
            return await syncLocalStorage.GetItemAsync<string>(UserName);
        }

        public static void SetUserName(this ISyncLocalStorageService syncLocalStorage, string value)
        {
             syncLocalStorage.SetItem(UserName, value);
        }

        public static async Task SetUserName(this ILocalStorageService syncLocalStorage, string value)
        {
            await syncLocalStorage.SetItemAsync(UserName, value);
        }

        public static Guid GetUserId(this ISyncLocalStorageService syncLocalStorage)
        {
            return syncLocalStorage.GetItem<Guid>(UserId);
        }

        public static async Task<Guid> GetUserId(this ILocalStorageService syncLocalStorage)
        {
            return await syncLocalStorage.GetItemAsync<Guid>(UserId);
        }

        public static void SetUserId(this ISyncLocalStorageService syncLocalStorage, Guid id)
        {
             syncLocalStorage.SetItem(UserId, id);
        }

        public static async Task SetUserId(this ILocalStorageService syncLocalStorage, Guid id)
        {
           await syncLocalStorage.SetItemAsync(UserId, id);
        }

        public static string GetToken(this ISyncLocalStorageService syncLocalStorage)
        {
            var token = syncLocalStorage.GetItem<string>(TokenName);
            if (string.IsNullOrEmpty(token))
                token = "";

            return token;
        }

        public static async Task<string> GetToken(this ILocalStorageService syncLocalStorage)
        {
            var token = await syncLocalStorage.GetItemAsync<string>(TokenName);
            if (string.IsNullOrEmpty(token))
                token = ""; // TODO

            return token;
        }

        public static void SetToken(this ISyncLocalStorageService syncLocalStorage, string value)
        {
            syncLocalStorage.SetItem(TokenName, value);
        }

        public static async Task SetToken(this ILocalStorageService syncLocalStorage, string value)
        {
           await syncLocalStorage.SetItemAsync(TokenName, value);
        }
    }
}
