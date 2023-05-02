using BlazorSozluk.Common.Events.Entry;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BlazorSozluk.Projections.VoteService.Services
{
    public sealed class VoteService
    {
        private readonly string connectionString;

        public VoteService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task CreateEntryVote(CreateEntryVoteEvent vote)
        {
            await DeleteEntryVote(vote.EntryId, vote.CreatedBy);

            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(
                "INSERT INTO EntryVote (Id, CreatedDate, EntryId, VoteType, CreatedById) VALUES (@Id, GETDATE(), @EntryId, @VoteType, @CreatedBy)",
                new
                {
                    Id = Guid.NewGuid(),
                    EntryId = vote.EntryId,
                    VoteType = (int)vote.VoteType,
                    CreatedById = vote.CreatedBy
                });
        }

        public async Task DeleteEntryVote(Guid entryId, Guid userId)
        {
            using var connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync(
                "DELETE FROM EntryVote WHERE EntryId = @EntryId AND CreatedById = @UserId",
                new
                {
                    EntryId = entryId,
                    UserId = userId
                });
        }
    }
}
