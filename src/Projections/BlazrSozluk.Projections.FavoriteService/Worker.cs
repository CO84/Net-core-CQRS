using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.Entry;
using BlazorSozluk.Common.Infrastructure;

namespace BlazrSozluk.Projections.FavoriteService
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

            var favService = new Services.FavoriteService(connStr);

           QueueFactory.CreateBasicConsumer()
                .EnsureExchange(sozlukConstatns.FavExchangeName)
                .EnsureQueue(sozlukConstatns.CreateEntryFavQueueName, sozlukConstatns.FavExchangeName)
                .Recive<CreateEntryFavEvent>( fav => 
                {
                    //db insert 
                    favService.CreateEntryFav(fav).GetAwaiter().GetResult();

                    _logger.LogInformation($"Reveived EntryId {fav.EntryId}");
                })
                .StartConsuming(sozlukConstatns.CreateEntryFavQueueName);
        }
    }
}