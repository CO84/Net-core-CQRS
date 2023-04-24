namespace BlazorSozluk.WebApp.Infrastructure.Services
{
    public class FavService : IFavService
    {
        private readonly HttpClient _httpClient;

        public FavService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateEntryFav(Guid entryId)
        {
            await _httpClient.PostAsync($"api/favorite/Entry/{entryId}", null);
        }

        public async Task CreateEntryCommentFav(Guid entryCommentId)
        {
            await _httpClient.PostAsync($"api/favorite/EntryComment/{entryCommentId}", null);
        }

        public async Task DeleteEntryFav(Guid entryId)
        {
            await _httpClient.PostAsync($"api/favorite/DeleteEntryFav/{entryId}", null);
        }

        public async Task DeleteEntryCommentFav(Guid entryCommentId)
        {
            await _httpClient.PostAsync($"api/favorite/DeleteEntryCommentFav/{entryCommentId}", null);
        }
    }
}
