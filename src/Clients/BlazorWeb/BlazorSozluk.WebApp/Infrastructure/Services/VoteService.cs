using BlazorSozluk.WebApp.Infrastructure.Services.Interfaces;
using static BlazorSozluk.Common.Models.Enums;

namespace BlazorSozluk.WebApp.Infrastructure.Services
{
    public class VoteService : IVoteService
    {
        private readonly HttpClient _httpClient;

        public VoteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DeleteEntryVote(Guid entryId)
        {
            var response = await _httpClient.PostAsync($"api/Vote/DeleteEntryVote/{entryId}", null);
            if (!response.IsSuccessStatusCode)
                throw new Exception("DeleteEntryVote error");
        }

        public async Task DeleteEntryCommentVote(Guid entryCommentId)
        {
            var response = await _httpClient.PostAsync($"api/Vote/DeleteEntryCommentVote/{entryCommentId}", null);
            if (!response.IsSuccessStatusCode)
                throw new Exception("DeleteEntryCommentVote error");
        }

        public async Task CreateEntryUpVote(Guid entryId)
        {
            await CreateEntryVote(entryId, VoteType.UpVote);
        }

        public async Task CreateEntryDownVote(Guid entryId)
        {
            await CreateEntryVote(entryId, VoteType.DownVote);
        }

        public async Task CreateEntryCommentUpVote(Guid entryCommentId)
        {
            await CreateEntryCommentVote(entryCommentId, VoteType.UpVote);
        }

        public async Task CreateEntryCommentDownVote(Guid entryCommentId)
        {
            await CreateEntryCommentVote(entryCommentId, VoteType.DownVote);
        }

        private async Task<HttpResponseMessage> CreateEntryVote(Guid entryId, VoteType voteType = VoteType.UpVote)
        {
            var result = await _httpClient.PostAsync($"api/Vote/entry/{entryId}?voteType={voteType}", null);

            return result;
        }

        private async Task<HttpResponseMessage> CreateEntryCommentVote(Guid entryCommentId, VoteType voteType = VoteType.UpVote)
        {
            var result = await _httpClient.PostAsync($"api/Vote/entryComment/{entryCommentId}?voteType={voteType}", null);

            return result;
        }
    }
}
