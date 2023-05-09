using BlazorSozluk.Common.Events.Entry;
using BlazorSozluk.Common.Infrastructure;
using BlazorSozluk.Common;
using Microsoft.Extensions.Configuration;
using BlazorSozluk.Common.Events.EntryComment;

namespace BlazorSozluk.Projections.VoteService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var connStr = _configuration.GetConnectionString("SqlServer");
            var voteService = new Services.VoteService(connStr);

            QueueFactory.CreateBasicConsumer()
               .EnsureExchange(sozlukConstatns.VoteExchangeName)
               .EnsureQueue(sozlukConstatns.CreateEntryVoteQueueName, sozlukConstatns.VoteExchangeName)
               .Recive<CreateEntryVoteEvent>(vote =>
               {
                   voteService.CreateEntryVote(vote).GetAwaiter().GetResult();

                   _logger.LogInformation("Create Entry Received EntryId: {0}, VoteType: {1}", vote.EntryId, vote.VoteType);
               })
               .StartConsuming(sozlukConstatns.CreateEntryVoteQueueName);

            QueueFactory.CreateBasicConsumer()
              .EnsureExchange(sozlukConstatns.VoteExchangeName)
              .EnsureQueue(sozlukConstatns.DeleteEntryVoteQueueName, sozlukConstatns.VoteExchangeName)
              .Recive<DeleteEntryVoteEvent>(vote =>
              {
                  voteService.DeleteEntryVote(vote.EntryId, vote.CreatedBy).GetAwaiter().GetResult();

                  _logger.LogInformation("Delete Entry Received EntryId: {0}", vote.EntryId);
              })
              .StartConsuming(sozlukConstatns.DeleteEntryVoteQueueName);

            QueueFactory.CreateBasicConsumer()
             .EnsureExchange(sozlukConstatns.VoteExchangeName)
             .EnsureQueue(sozlukConstatns.CreateEntryCommentVoteQueueName, sozlukConstatns.VoteExchangeName)
             .Recive<CreateEntryCommentVoteEvent>(vote =>
             {
                 voteService.CreateEntryCommentVote(vote).GetAwaiter().GetResult();

                 _logger.LogInformation("Create Entry Vote Received EntryId: {0}, VoteType: {1}", vote.EntryCommentId, vote.VoteType);
             })
             .StartConsuming(sozlukConstatns.CreateEntryCommentVoteQueueName);

            QueueFactory.CreateBasicConsumer()
            .EnsureExchange(sozlukConstatns.VoteExchangeName)
            .EnsureQueue(sozlukConstatns.DeleteEntryCommentVoteQueueName, sozlukConstatns.VoteExchangeName)
            .Recive<DeleteEntryCommentVoteEvent>(vote =>
            {
                voteService.DeleteEntryCommentVote(vote.EntryCommentId, vote.CreatedBy).GetAwaiter().GetResult();

                _logger.LogInformation("Delete Entry Vote Received EntryId: {0}", vote.EntryCommentId);
            })
            .StartConsuming(sozlukConstatns.DeleteEntryCommentVoteQueueName);
        }
    }
}