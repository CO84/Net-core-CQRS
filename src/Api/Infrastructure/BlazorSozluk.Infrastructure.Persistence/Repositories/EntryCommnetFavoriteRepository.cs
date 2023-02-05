using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.Infrastructure.Persistence.Context;

namespace BlazorSozluk.Infrastructure.Persistence.Repositories
{
    public class EntryCommnetFavoriteRepository : Repository<EntryCommentFavorite>, IEntryCommentFavoriteRepository
    {
        public EntryCommnetFavoriteRepository(BlazorSozlukContext context) : base(context)
        {
        }
    }
}
