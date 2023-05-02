using BlazorSozluk.Common.Events.Entry;
using BlazorSozluk.Common.Infrastructure;
using BlazorSozluk.Common;
using Microsoft.Extensions.Configuration;

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
                   //db insert 
                   voteService.CreateEntryVote(vote).GetAwaiter().GetResult();

                   _logger.LogInformation("Create Entry Received EntryId: {0}, VoteType: {1}", vote.EntryId, vote.VoteType);
               })
               .StartConsuming(sozlukConstatns.CreateEntryVoteQueueName);
        }
    }
}