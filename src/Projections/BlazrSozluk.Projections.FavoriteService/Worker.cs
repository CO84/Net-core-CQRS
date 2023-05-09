using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.Entry;
using BlazorSozluk.Common.Events.EntryComment;
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
                    
                    favService.CreateEntryFav(fav).GetAwaiter().GetResult();

                    _logger.LogInformation($"Reveived EntryId {fav.EntryId}");
                })
                .StartConsuming(sozlukConstatns.CreateEntryFavQueueName);

            QueueFactory.CreateBasicConsumer()
              .EnsureExchange(sozlukConstatns.FavExchangeName)
              .EnsureQueue(sozlukConstatns.DeleteEntryFavQueueName, sozlukConstatns.FavExchangeName)
              .Recive<DeleteEntryFavEvent>(fav => 
              {

                 favService.DeleteEntryFav(fav).GetAwaiter().GetResult();

                  _logger.LogInformation($"Deleted Reveived EntryId {fav.EntryId}");
              })
              .StartConsuming(sozlukConstatns.DeleteEntryFavQueueName);

            QueueFactory.CreateBasicConsumer()
               .EnsureExchange(sozlukConstatns.FavExchangeName)
               .EnsureQueue(sozlukConstatns.DeleteEntryCommentFavQueueName, sozlukConstatns.FavExchangeName)
               .Recive<DeleteEntryCommentFavEvent>(fav =>
               {

                   favService.DeleteEntryCommentFav(fav).GetAwaiter().GetResult();

                   _logger.LogInformation($" Delete Entry Comment Reveived EntryId {fav.EntryCommentId}");
               })
               .StartConsuming(sozlukConstatns.DeleteEntryCommentFavQueueName);

            QueueFactory.CreateBasicConsumer()
              .EnsureExchange(sozlukConstatns.FavExchangeName)
              .EnsureQueue(sozlukConstatns.CreateEntryCommentFavQueueName, sozlukConstatns.FavExchangeName)
              .Recive<CreateEntryCommentFavEvent>(fav =>
              {

                  favService.CreateEntryCommentFav(fav).GetAwaiter().GetResult();

                  _logger.LogInformation($"Create Entry CommentReveived EntryId {fav.EntryCommentId}");
              })
              .StartConsuming(sozlukConstatns.CreateEntryCommentFavQueueName);
        }
    }
}