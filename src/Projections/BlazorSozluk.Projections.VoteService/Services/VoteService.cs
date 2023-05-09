using BlazorSozluk.Common.Events.Entry;
using BlazorSozluk.Common.Events.EntryComment;
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
                "INSERT INTO entry_vote (Id, CreateDate, EntryId, VoteType, CreatedById) VALUES (@Id, GETDATE(), @EntryId, @VoteType, @CreatedById)",
                new
                {
                    Id = Guid.NewGuid(),
                    EntryId = vote.EntryId,
                    VoteType = (int)vote.VoteType,
                    CreatedById = vote.CreatedBy
                });
        }

        public async Task CreateEntryCommentVote(CreateEntryCommentVoteEvent vote)
        {
            await DeleteEntryVote(vote.EntryCommentId, vote.CreatedBy);

            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(
                "INSERT INTO entry_comment_vote (Id, CreateDate, EntryCommentId, VoteType, CreatedById) VALUES (@Id, GETDATE(), @EntryCommentId, @VoteType, @CreatedById)",
                new
                {
                    Id = Guid.NewGuid(),
                    EntryCommentId = vote.EntryCommentId,
                    VoteType = (int)vote.VoteType,
                    CreatedById = vote.CreatedBy
                });
        }

        public async Task DeleteEntryVote(Guid entryId, Guid userId)
        {
            using var connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync(
                "DELETE FROM entry_vote WHERE EntryId = @EntryId AND CreatedById = @UserId",
                new
                {
                    EntryId = entryId,
                    UserId = userId
                });
        }

        public async Task DeleteEntryCommentVote(Guid entryCommentId, Guid userId)
        {
            using var connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync(
                "DELETE FROM entry_comment_vote WHERE EntryCommentId = @EntryCommentId AND CreatedById = @UserId",
                new
                {
                    EntryCommentId = entryCommentId,
                    CreatedById = userId
                });
        }
    }
}
