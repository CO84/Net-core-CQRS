using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.Infrastructure.Persistence.Context;

namespace BlazorSozluk.Infrastructure.Persistence.Repositories
{
    public class EntryCommnetRepository : Repository<EntryComment>, IEntryCommentRepository
    {
        public EntryCommnetRepository(BlazorSozlukContext context) : base(context)
        {
        }
    }
}
