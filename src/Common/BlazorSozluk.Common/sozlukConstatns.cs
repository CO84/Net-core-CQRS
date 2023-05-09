using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Common
{
    public class sozlukConstatns
    {
#if DEBUG
        public const string RabbitMQHost = "localhost";
#else
        public const string RabbitMQHost = "rabbitmq";
#endif

        public const string DefaultExchangetype = "direct";


        public const string UserExchangeName = "UserExchange";
        public const string UserEmailChangedQueueName = "UserEmailChangedQueue";

        //FavExchange
        public const string FavExchangeName = "FavExchange";
        public const string CreateEntryFavQueueName = "CreateEntryFavQueue";
        public const string CreateEntryCommentFavQueueName = "CreateEntryCommentFavQueue";

        public const string DeleteEntryFavQueueName = "DeleteEntryFavQueue";
        public const string DeleteEntryVoteQueueName = "DeleteEntryVoteQueue";
        public const string DeleteEntryCommentFavQueueName = "DeleteEntryCommentFavQueue";
        public const string DeleteEntryCommentVoteQueueName = "DeleteEntryCommentVoteQueue";


        public const string VoteExchangeName = "VoteExchange";
        public const string CreateEntryVoteQueueName = "CreateEntryVoteQueue";
        public const string CreateEntryCommentVoteQueueName = "CreateEntryCommentVoteQueue";
    }
}
